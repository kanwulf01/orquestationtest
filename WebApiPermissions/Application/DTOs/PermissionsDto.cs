using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PermissionsDto
    {
        public int Id { get; set; }

        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }

        //[ForeignKey]
        public int TipoPermiso { get; set; }

        public DateTime FechaPermiso { get; set; }


    }
}
