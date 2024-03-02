using CityInfo.API.Repository;

namespace CityInfo.API.Contracts;

public interface IUnitOfWork
{
    ICityRepository CityRepository { get; }
    IPointOfInterestRepository PointOfInterestRepository { get; }

    IGenericRepository<T> GetRepository<T>() where T : class;

    Task SaveAsync();
}
