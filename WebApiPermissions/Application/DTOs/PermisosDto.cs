using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PermisosDto
    {
        public int Id { get; set; }

        public string NombreEmpleado { get; set; } = string.Empty;
        public string ApellidoEmpleado { get; set; } = string.Empty;

        public int TipoPermiso { get; set; }

        public DateTime FechaPermiso { get; set; }


    }
}
