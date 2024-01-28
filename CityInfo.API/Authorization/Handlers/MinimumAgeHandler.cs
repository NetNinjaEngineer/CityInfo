using CityInfo.API.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CityInfo.API.Authorization.Handlers;

public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var userClaims = context.User.Claims;
        var dateOfBirthClaim = userClaims.FirstOrDefault(c =>
            c.Type == ClaimTypes.DateOfBirth);

        if (!DateTime.TryParse(dateOfBirthClaim.Value, out var dateOfBirth))
            return Task.CompletedTask;

        var calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
        if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            calculatedAge--;
        if (calculatedAge >= requirement.Age)
            context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
