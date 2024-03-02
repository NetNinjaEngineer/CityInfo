namespace CityInfo.API.Specifications
{
    public class NotSpecification<T> : Specification<T> where T : class
    {
        private readonly ISpecification<T> _specification;

        public NotSpecification(ISpecification<T> specification)
            => _specification = specification;

        public override bool IsSatisfied(T entity)
            => !_specification.IsSatisfied(entity);
    }
}
