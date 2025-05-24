using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Permisos.Commands.UpdatePermisos
{
    public class UpdatePermisosCommand : IRequest<PermisosDto?>
    {
        public int Id { get; set; }

        public string NombreEmpleado { get; set; } = string.Empty;
        public string ApellidoEmpleado { get; set; } = string.Empty;
        public int TipoPermiso { get; set; }

        public DateTime FechaPermiso { get; set; }
    }
}
