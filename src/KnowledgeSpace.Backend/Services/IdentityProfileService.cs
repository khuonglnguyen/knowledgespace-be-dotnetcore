using IdentityServer4.Models;
using IdentityServer4.Services;
using KnowledgeSpace.Backend.Constants;
using KnowledgeSpace.Backend.Data.Entities;
using KnowledgeSpace.Backend.Data;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using IdentityServer4.Extensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.Backend.Services
{
    public class IdentityProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityProfileService(IUserClaimsPrincipalFactory<User> claimsFactory,
            UserManager<User> userManager,
            ApplicationDBContext context,
           RoleManager<IdentityRole> roleManager)
        {
            _claimsFactory = claimsFactory;
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                throw new ArgumentException("");
            }

            var principal = await _claimsFactory.CreateAsync(user);
            var claims = principal.Claims.ToList();
            var roles = await _userManager.GetRolesAsync(user);

            var query = from p in _context.Permissions
                        join c in _context.Commands
                        on p.CommandId equals c.Id
                        join f in _context.Functions
                        on p.FunctionId equals f.Id
                        join r in _roleManager.Roles on p.RoleId equals r.Id
                        where roles.Contains(r.Name)
                        select f.Id + "_" + c.Id;
            var permissions = await query.Distinct().ToListAsync();

            //Add more claims like this
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Role, string.Join(";", roles)));
            claims.Add(new Claim(SystemConstants.Claims.Permissions, JsonConvert.SerializeObject(permissions)));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
