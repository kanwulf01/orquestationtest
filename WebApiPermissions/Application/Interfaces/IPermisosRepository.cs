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
        Task<Permiso?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Permiso>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(Permiso permisos, CancellationToken cancellationToken = default);

        void Update(Permiso permisos); // Aca EF puede seguir el cambio en la DB


    }
}
