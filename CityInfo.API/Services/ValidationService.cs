using CityInfo.API.Contracts;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Services;

public class ValidationService<T> : IValidationService<T> where T : class
{
    public virtual bool Validate(T entity)
    {
        var validationContext = new ValidationContext(entity);
        var validationResults = new List<ValidationResult>();
        return Validator.TryValidateObject(entity, validationContext, validationResults);
    }
}
