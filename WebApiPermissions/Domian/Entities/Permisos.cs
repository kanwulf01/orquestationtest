using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Entities
{
    public class Permisos
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public string NombreEmpleado { get; private set; } = string.Empty;

        [Required]
        public string ApellidoEmpleado { get; private set; } = string.Empty;

        [Required]
        public int TipoPermiso {  get; private set; }

        [Required]
        public DateTime FechaPermiso { get; private set; }

        private Permisos() { }

        public static Permisos Create(
            int id,
            string nombreEmpleado,
            string apellidoEmpleado,
            int TipoPermiso,
            DateTime fechaPermiso
            )
        {
            if (string.IsNullOrEmpty( nombreEmpleado) ) throw new ArgumentException( "El NombreEmpleado no puede ser vacio" );
            if (string.IsNullOrEmpty(apellidoEmpleado)) throw new ArgumentException("El ApellidoEmpleado no puede ser vacio");
            if (TipoPermiso == 0) throw new ArgumentException("Necesita un tipo de permiso de forma obligatoria");

            return new Permisos
            {
                Id = id,
                NombreEmpleado = nombreEmpleado,
                ApellidoEmpleado = apellidoEmpleado,
                TipoPermiso = TipoPermiso,
                FechaPermiso = fechaPermiso
            };
        }

        public void UpdatePermisos(
            int id,
            string nombreEmpleado,
            string apellidoEmpleado,
            int tipoPermiso,
            DateTime fechaPermiso
        )
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));

            if (string.IsNullOrEmpty(nombreEmpleado)) throw new ArgumentException("El nombre del empleado no puede ser vacio");
            if (string.IsNullOrEmpty(apellidoEmpleado)) throw new ArgumentException("El apellido del empleado no puede ser vacio");
            if (TipoPermiso == 0) throw new ArgumentException("Necesita un tipo de permiso de forma obligatoria");

            Id = id;
            NombreEmpleado = nombreEmpleado;
            ApellidoEmpleado = apellidoEmpleado;
            TipoPermiso = tipoPermiso;
            FechaPermiso = fechaPermiso;

        }



    }
}
