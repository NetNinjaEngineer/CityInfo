namespace CityInfo.API.Specifications
{
    public abstract class Specification<T> : ISpecification<T> where T : class
    {
        public abstract bool IsSatisfied(T entity);

        public Specification<T> And(Specification<T> specification) =>
            new AndSpecification<T>(this, specification);

        public Specification<T> Or(Specification<T> specification) =>
            new OrSpecification<T>(this, specification);

        public Specification<T> Not(Specification<T> specification) =>
            new NotSpecification<T>(specification);

    }
}
