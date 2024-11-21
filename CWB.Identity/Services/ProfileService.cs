using CWB.Identity.Domain;
using CWB.Logging;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CWB.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<CwbUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<CwbUser> _userClaimsPrincipalFactory;
        private readonly ILoggerManager _logger;

        public ProfileService(UserManager<CwbUser> userManager, RoleManager<IdentityRole> roleManager,
            IUserClaimsPrincipalFactory<CwbUser> userClaimsPrincipalFactory, ILoggerManager logger)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

            var user = await _userManager.GetUserAsync(context.Subject);
            //var claims = await _userManager.GetClaimsAsync(user);
            var userClaims = await _userClaimsPrincipalFactory.CreateAsync(user); //
            List<Claim> claims = userClaims.Claims.ToList();
            if (_userManager.SupportsUserRole)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                    if (_roleManager.SupportsRoleClaims)
                    {
                        IdentityRole role = await _roleManager.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            claims.AddRange(await _roleManager.GetClaimsAsync(role));
                        }
                    }

                }

            }
            context.IssuedClaims = claims;//.AddRange(claims);


        }

        // this method allows to check if the user is still "enabled" per token request
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = (user != null) && user.LockoutEnabled;
        }
    }
}
