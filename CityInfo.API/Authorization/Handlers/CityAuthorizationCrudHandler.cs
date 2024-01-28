using CityInfo.API.Authorization.Requirements;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CityInfo.API.Authorization.Handlers;

public class CityAuthorizationCrudHandler : AuthorizationHandler<
    OperationAuthorizationRequirement, City>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        City resource)
    {
        if (requirement.Name == Operations.Read.Name)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}