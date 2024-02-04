namespace CityInfo.API.Contracts;

public interface IValidationService<TEntity> where TEntity : class
{
    bool Validate(TEntity entity);
}
