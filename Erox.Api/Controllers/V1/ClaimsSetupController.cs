using Erox.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Erox.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class ClaimsSetupController : BaseController
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthorizationController> _logger;
        public ClaimsSetupController(DataContext ctx, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AuthorizationController> logger)
        {
            _ctx = ctx;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllClaims(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"The user with the {email} does not exist");
                return BadRequest(new { error = "User does not exist" });
            }
            var userClaims=await _userManager.GetClaimsAsync(user);
            return Ok(userClaims);
        }

        [HttpPost]
        [Route("AddClaimsToUser")]
        public async Task<IActionResult> AddClaimsToUser(string email,string claimName,string claimValue)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"The user with the {email} does not exist");
                return BadRequest(new { error = "User does not exist" });
            }
            var userClaim = new Claim(claimName, claimValue);
            var result=await _userManager.AddClaimAsync(user, userClaim);   
            if(result.Succeeded)
            {
                return Ok(new
                {
                    result = $"User {user.Email} has a claim {claimName} added to them"
                });
            }
            return BadRequest(new
            {
                error = $"Unable to add claim {claimName} to the user {user.Email}"
            });
        }
    }
}
