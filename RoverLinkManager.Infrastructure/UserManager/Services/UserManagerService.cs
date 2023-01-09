using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        //_authRepo.
        
    }

    public async Task<List<AppUser>> FindUsersAsync(string query = "", int pageNumber = 1, int pageSize = 25)
    {
        using var db = await _dbConnectionFactory.OpenDbConnectionAsync();

        // Limit to page numbers of 1 or higher
        pageNumber = pageNumber > 0 ? pageNumber : 1;

        // Limit to 100 records per page
        pageSize = pageSize > 100 ? pageSize : 25;

        // See http://www.albahari.com/nutshell/predicatebuilder.aspx
        var predicate = PredicateBuilder.True<AppUser>();

        if (!string.IsNullOrEmpty(query))
        {
            // append query predicate onto the existing predicate
            predicate = predicate.And(user => user.DisplayName.StartsWith(query));
        }

        var users = db.WhereLazy<AppUser>(predicate)
                                       .Skip((pageNumber-1)*pageSize)
                                       .Take(pageSize);

        db.Close();

        return users.ToList();
    }
}
