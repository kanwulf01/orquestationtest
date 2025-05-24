using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPermisosRepository
    {
        Task<Domain.Entities.Permiso> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Domain.Entities.Permiso>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(Domain.Entities.Permiso permisos, CancellationToken cancellationToken = default);

        void Update(Domain.Entities.Permiso permisos); // Aca EF puede seguir el cambio en la DB


    }
}
