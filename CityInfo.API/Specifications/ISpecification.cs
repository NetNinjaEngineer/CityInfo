namespace CityInfo.API.Specifications
{
    public interface ISpecification<T> where T : class
    {
        bool IsSatisfied(T entity);
    }
}
