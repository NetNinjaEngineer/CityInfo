using CityInfo.API.Contracts;
using CityInfo.API.Data;

namespace CityInfo.API.Repository.Implementors;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Lazy<ICityRepository> _cityRepository;
    private readonly Lazy<IPointOfInterestRepository> _pointOfInterestRepository;
    private Dictionary<Type, object> repositories = [];

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        _cityRepository = new Lazy<ICityRepository>(
            () => new CityRepository(_context));

        _pointOfInterestRepository = new Lazy<IPointOfInterestRepository>(
            () => new PointOfInterestRepository(_context));
    }

    public ICityRepository CityRepository => _cityRepository.Value;
    public IPointOfInterestRepository PointOfInterestRepository => _pointOfInterestRepository.Value;

    public IGenericRepository<T> GetRepository<T>() where T : class
    {
        if (!repositories.ContainsKey(typeof(T)))
            repositories[typeof(T)] = new GenericRepository<T>(_context);
        return (IGenericRepository<T>)repositories[typeof(T)];
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
