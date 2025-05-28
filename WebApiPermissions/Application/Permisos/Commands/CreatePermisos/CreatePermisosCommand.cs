using Application.DTOs;
using MediatR;


namespace Application.Permisos.Commands.CreatePermisos
{
    public class CreatePermisosCommand : IRequest<PermisosDto>
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; } = string.Empty;
        public string ApellidoEmpleado { get; set; } = string.Empty;
        public int TipoPermisoId { get; set; }
        public DateTime FechaPermiso { get; set; }


    }
}
