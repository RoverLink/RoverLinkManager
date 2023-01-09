using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RoverLinkManager.Domain.Entities;
using RoverLinkManager.Domain.Entities.Identity;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Stream;
using RoverLinkManager.Domain.Extensions;

namespace RoverLinkManager.Infrastructure.UserManager.Services;
public class UserManagerService
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly IAuthRepositoryAsync _authRepo;
    private readonly IManageRolesAsync _roleManager;

    public UserManagerService(IAuthRepositoryAsync authRepo, IDbConnectionFactory dbConnectionFactory)
    {
	    _authRepo = authRepo;
        _roleManager = (IManageRolesAsync)authRepo;
        _dbConnectionFactory = dbConnectionFactory;

        //var users = _authRepo.GetUserAuthsAsync().Result;
    }

    public Expression<Func<AppUser, bool>> DisplayNameStartsWith(string name) => x => x.DisplayName.StartsWith(name);
    public Expression<Func<AppUser, bool>> DisplayNameContains(string name) => x => x.DisplayName.Contains(name);

    /// <summary>
    /// To build the where predicate it is possible to use predicate builder extensions
    /// See http://www.albahari.com/nutshell/predicatebuilder.aspx
    ///     var predicate = PredicateBuilder.True<AppUser>();  // Start the query chain with a true value to do a search for all terms
    ///     predicate = predicate.And(user => user.DisplayName.StartsWith(query));  // Chain along additional terms with And
    ///
    /// Example call:
    /// var users = await FindUsersAsync(x => x.DisplayName.Contains("Matt"), 1, 25);
    /// </summary>
    /// <param name="where"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<List<AppUser>> FindUsersAsync(Expression<Func<AppUser, bool>>? where, int pageNumber = 1, int pageSize = 25)
    {
        using var db = await _dbConnectionFactory.OpenDbConnectionAsync();

        // Limit to page numbers of 1 or higher
        pageNumber = pageNumber > 0 ? pageNumber : 1;

        // Limit to 100 records per page
        pageSize = pageSize > 100 ? pageSize : 25;

        // Create a dummy predicate that is always true
        var predicate = PredicateBuilder.True<AppUser>();

        // If where is not null add that expression to the predicate
        if (where != null)
	        predicate = predicate.And(where);

        var users = db.WhereLazy<AppUser>(predicate)
										.Skip((pageNumber-1)*pageSize)
										.Take(pageSize);

        db.Close();

        return users.ToList();
    }
}
