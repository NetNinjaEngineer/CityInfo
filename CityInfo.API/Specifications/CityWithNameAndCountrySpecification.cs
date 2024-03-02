using CityInfo.API.Models;

namespace CityInfo.API.Specifications
{
    public class CityWithNameAndCountrySpecification : Specification<City>
    {
        private readonly string _searchTerm;

        public CityWithNameAndCountrySpecification(string searchTerm)
            => _searchTerm = searchTerm;

        public override bool IsSatisfied(City city)
        {
            return city.Name!.Contains(_searchTerm!, StringComparison.CurrentCultureIgnoreCase) ||
                city.Country!.Contains(_searchTerm!, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}