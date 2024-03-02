namespace CityInfo.API.Specifications
{
    public class AndSpecification<T> : Specification<T> where T : class
    {
        private readonly ISpecification<T> _leftSpecification;
        private readonly ISpecification<T> _rightSpecification;

        public AndSpecification(ISpecification<T> leftSpecification, ISpecification<T> rightSpecification)
        {
            _leftSpecification = leftSpecification;
            _rightSpecification = rightSpecification;
        }

        public override bool IsSatisfied(T entity)
            => _leftSpecification.IsSatisfied(entity) && _rightSpecification.IsSatisfied(entity);
    }
}
