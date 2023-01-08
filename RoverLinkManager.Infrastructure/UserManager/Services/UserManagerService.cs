using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Auth;
using Stream;

namespace RoverLinkManager.Infrastructure.UserManager.Services;
public class UserManagerService
{
	private IAuthRepositoryAsync _authRepo;
	private IManageRolesAsync _roleManager;

    public UserManagerService(IAuthRepositoryAsync authRepo)
    {
	    _authRepo = authRepo;
        _roleManager = (IManageRolesAsync)authRepo;

        var users = _authRepo.GetUserAuthsAsync().Result;
        
    }
}
