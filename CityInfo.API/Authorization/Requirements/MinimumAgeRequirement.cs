using Microsoft.AspNetCore.Authorization;

namespace CityInfo.API.Authorization.Requirements;

public class MinimumAgeRequirement : IAuthorizationRequirement
{
    public MinimumAgeRequirement(int age)
    {
        Age = age;
    }

    public int Age { get; private set; }
}
