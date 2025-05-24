using Domain.Entities;


namespace Application.Interfaces
{
    public interface ITipoPermisosRepository
    {
        Task<Permiso?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Permiso>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(Permiso permisos, CancellationToken cancellationToken = default);

        void Update(Permiso permisos); // Aca EF puede seguir el cambio en la DB
    }
}
