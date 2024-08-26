using Erox.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Erox.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class AuthorizationController : BaseController
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthorizationController> _logger;
        public AuthorizationController(DataContext ctx, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,ILogger<AuthorizationController> logger)
        {
                _ctx = ctx;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles =_roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            var roleExist=await _roleManager.RoleExistsAsync(name);

            if(!roleExist)
            {
                var roleResult=await _roleManager.CreateAsync(new IdentityRole(name));
                if(roleResult.Succeeded)
                {
                    _logger.LogInformation($"The role {name} has been added successfuly");
                    return Ok(new
                    {
                        result = $"The role {name} has been added successfuly"
                    });
                }
                else
                {
                    _logger.LogInformation($"The role {name} has not  been added");
                    return Ok(new
                    {
                        error = $"The role {name} has not been added"
                    });
                }
            }

            return BadRequest(new { error = "Role already exist" });
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers2()
        {
            var users=await _userManager.Users.ToListAsync();
            return Ok(users);   
        }


        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"The user with the {email} does not exist");
                return BadRequest(new { error = "User does not exist" });
            }
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                _logger.LogInformation($"The role {roleName} does not exist");
                return BadRequest(new { error = "Role does not exist" });
            }
            var result=await _userManager.AddToRoleAsync(user, roleName);
            if(result.Succeeded)
            {
                return Ok(new
                {
                    result = "Success,user has been added to the role"
                });
            }
            else
            {
                _logger.LogInformation($"The user was not abel to be added to the role  ");
                return BadRequest(new { error = $"The user was not abel to be added to the role" });
            }
        }

        [HttpDelete]
        [Route("RemoveUserFromRoole")]
        public async Task<IActionResult> RemoveUserFromRole(string email,string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"The user with the {email} does not exist");
                return BadRequest(new { error = "User does not exist" });
            }
            var roleExist = await _roleManager.RoleExistsAsync(role);

            if (!roleExist)
            {
                _logger.LogInformation($"The role {role} does not exist");
                return BadRequest(new { error = "Role does not exist" });
            }
            var result=await _userManager.RemoveFromRoleAsync(user, role);
            if(result.Succeeded)
            {
                return Ok(new
                {
                    result = "User has been remove from role"
                });
            }
            return BadRequest(new { error = $"Unable to remove User {email} from role {role}" });
        }

       
    }
}
