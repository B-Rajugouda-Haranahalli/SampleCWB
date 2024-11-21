using CWB.Constants.UserIdentity;
using CWB.Identity.DbContext;
using CWB.Identity.Domain;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;

namespace CWB.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly CwbIdentityDbContext _cwbIdentityDbContext;

        private readonly UserManager<CwbUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(CwbIdentityDbContext cwbIdentityDbContext, UserManager<CwbUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _cwbIdentityDbContext = cwbIdentityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if (_roleManager.FindByNameAsync(Roles.ADMIN).Result == null)
                {
                    _roleManager.CreateAsync(new IdentityRole(Roles.ADMIN)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(Roles.SUPERADMIN)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(Roles.USER)).GetAwaiter().GetResult();
                }
                else
                {
                    return;
                }
                //Create default super admin
                var superAdmin = new CwbUser()
                {
                    UserName = "superadmin",
                    Email = "superadmin@cwb.com",
                    PhoneNumber = "0000000000",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = "Super",
                    LastName = "Admin",
                    TenantId = 0

                };
                var result = _userManager.CreateAsync(superAdmin, "SAdmin@123").GetAwaiter().GetResult();

                _userManager.AddToRoleAsync(superAdmin, Roles.SUPERADMIN).GetAwaiter().GetResult();
                var userClaim = _userManager.AddClaimsAsync(superAdmin, new Claim[]
                {
                new Claim(JwtClaimTypes.Name, superAdmin.FirstName +" "+ superAdmin.LastName),
                new Claim(JwtClaimTypes.GivenName, superAdmin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, superAdmin.LastName),
                new Claim(JwtClaimTypes.Role, Roles.SUPERADMIN),
                new Claim("TenantId","0")
                }).Result;

                //Create default client Admin
                var clientAdmin1 = new CwbUser()
                {
                    UserName = "kgk1admin",
                    Email = "admin1@kgk.com",
                    PhoneNumber = "0000000000",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = "Kgk1",
                    LastName = "Admin",
                    TenantId = 0
                };
                var kgk1Result = _userManager.CreateAsync(clientAdmin1, "KAdmin@123").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(clientAdmin1, Roles.ADMIN).GetAwaiter().GetResult();
                var user1Claim = _userManager.AddClaimsAsync(clientAdmin1, new Claim[]
                {
                new Claim(JwtClaimTypes.Name, clientAdmin1.FirstName +" "+ clientAdmin1.LastName),
                new Claim(JwtClaimTypes.GivenName, clientAdmin1.FirstName),
                new Claim(JwtClaimTypes.FamilyName, clientAdmin1.LastName),
                new Claim(JwtClaimTypes.Role, Roles.ADMIN),
                new Claim("TenantId","1")
                }).Result;


                var clientAdmin2 = new CwbUser()
                {
                    UserName = "kgk2admin",
                    Email = "admin2@kgk.com",
                    PhoneNumber = "0000000000",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = "Kgk2",
                    LastName = "Admin",
                    TenantId = 0
                };
                var kgk2Result = _userManager.CreateAsync(clientAdmin2, "KAdmin@123").GetAwaiter().GetResult();

                _userManager.AddToRoleAsync(clientAdmin2, Roles.ADMIN).GetAwaiter().GetResult();
                var user2Claim = _userManager.AddClaimsAsync(clientAdmin2, new Claim[]
                {
                new Claim(JwtClaimTypes.Name, clientAdmin2.FirstName +" "+ clientAdmin2.LastName),
                new Claim(JwtClaimTypes.GivenName, clientAdmin2.FirstName),
                new Claim(JwtClaimTypes.FamilyName, clientAdmin2.LastName),
                new Claim(JwtClaimTypes.Role, Roles.ADMIN),
                new Claim("TenantId","2")
                }).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
