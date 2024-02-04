using CityInfo.API.Models;

namespace CityInfo.API.Services;

public class CityValidationService : ValidationService<City>
{
    public override bool Validate(City entity)
    {
        return base.Validate(entity);
    }
}
