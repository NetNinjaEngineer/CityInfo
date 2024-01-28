using CityInfo.API.Helpers;
using Microsoft.AspNetCore.Identity;

namespace CityInfo.API.Services;

public class SeedRolesService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedRolesService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedRoles()
    {
        await SeedRoles([Roles.Admin, Roles.User]);
    }

    private async Task SeedRoles(IEnumerable<string> roles)
    {
        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var identityRole = new IdentityRole { Name = role };
                await _roleManager.CreateAsync(identityRole);
            }
        }
    }
}
