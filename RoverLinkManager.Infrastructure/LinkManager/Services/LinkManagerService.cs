using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RoverLinkManager.Domain.Entities.Link;
using RoverLinkManager.Infrastructure.Common.IdGenerator.Services;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace RoverLinkManager.Infrastructure.LinkManager.Services;

public class LinkManagerService
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly IdGeneratorService _idGenerator;

    private readonly string _urlRegex = @"((http|https)://)(www.)?" +
                                        "[a-zA-Z0-9@:%._\\+~#?&//=]" +
                                        "{2,256}\\.[a-z]" +
                                        "{2,6}\\b([-a-zA-Z0-9@:%" +
                                        "._\\+~#?&//=]*)";

    public LinkManagerService(IDbConnectionFactory dbConnectionFactory, IdGeneratorService idGenerator)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _idGenerator = idGenerator;
    }

    /// <summary>
    /// Finds the url for the link identified by shortId, returns null if unable to find link
    /// </summary>
    /// <param name="shortId"></param>
    /// <param name="incrementVisitCount"></param>
    /// <returns></returns>
    public async Task<Link?> GetShortLinkAsync(string shortId, bool incrementVisitCount = false)
    {
        if (string.IsNullOrEmpty(shortId))
            return null;

        var snowflakeId = _idGenerator.ToSnowflakeId(shortId);

        if (snowflakeId == null)
            return null;

        // Open a database connection
        using var db = await _dbConnectionFactory.OpenDbConnectionAsync();

        // Lookup link in database
        var link = (await db.SingleAsync<Link>(link => link.Id == snowflakeId.Id));

        // Check to see if this request will function as a visit
        if (link != null && incrementVisitCount)
        {
            // Only update the visits count
            await db.UpdateAsync<Link>(new { Visits = link.Visits + 1 }, x => x.Id == link.Id);
        }

        return link;
    }

    /// <summary>
    /// Creates a short version of a link
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<Link?> CreateShortLinkAsync (string url)
    {
        var re = new Regex(_urlRegex);

        // Check to see if the url is valid
        if (string.IsNullOrEmpty(url) || !re.IsMatch(url))
            return null;

        // Open a database connection
        using var db = await _dbConnectionFactory.OpenDbConnectionAsync();

        // Generate a new snowflake id
        var snowflakeId = _idGenerator.CreateId();

        var link = new Link
        {
            Id = snowflakeId.Id,
            Url = url,
            ShortId = snowflakeId.ShortId
        };

        await db.InsertAsync(link);

        db.Close();
        
        return link;
    }
}
