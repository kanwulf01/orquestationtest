using Domain.Entities;


namespace Application.Interfaces
{
    public interface ITipoPermisosRepository
    {
        Task<TipoPermiso?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<TipoPermiso>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(TipoPermiso permisos, CancellationToken cancellationToken = default);

        void Update(TipoPermiso permisos); // Aca EF puede seguir el cambio en la DB
    }
}
