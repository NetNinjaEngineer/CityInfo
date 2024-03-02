namespace CityInfo.API.Specifications
{
    public class OrSpecification<T> : Specification<T> where T : class
    {
        private readonly ISpecification<T> _specificationLeft;
        private readonly ISpecification<T> _specificationRight;

        public OrSpecification(ISpecification<T> specificationLeft, ISpecification<T> specificationRight)
        {
            _specificationLeft = specificationLeft;
            _specificationRight = specificationRight;
        }

        public override bool IsSatisfied(T entity)
            => _specificationLeft.IsSatisfied(entity) || _specificationRight.IsSatisfied(entity);
    }
}
