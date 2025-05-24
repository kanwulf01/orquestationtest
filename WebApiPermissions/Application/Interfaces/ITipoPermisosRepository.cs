using Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITipoPermisosRepository
    {
        Task<Permisos?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Permisos>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(Permisos permisos, CancellationToken cancellationToken = default);

        void Update(Permisos permisos); // Aca EF puede seguir el cambio en la DB
    }
}
