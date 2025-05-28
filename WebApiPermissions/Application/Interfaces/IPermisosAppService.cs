using Application.DTOs;
using Application.Permisos.Commands.CreatePermisos;
using Application.Permisos.Commands.UpdatePermisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPermisosAppService
    {
        Task<IEnumerable<PermisosDto>> GetlllPermisos(CancellationToken cancellationToken);

        Task<PermisosDto> CreatePermiso(CreatePermisosCommand command, CancellationToken cancellationToken);

        Task<PermisosDto?> UpdatePermiso(UpdatePermisosCommand command, CancellationToken cancellationToken);

        Task<PermisosDto?> GetPermisoById(int id, CancellationToken cancellationToken);
    }
}
