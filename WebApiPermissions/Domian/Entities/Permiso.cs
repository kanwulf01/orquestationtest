using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    public class Permiso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        public string NombreEmpleado { get; private set; } = string.Empty;

        [Required]
        public string ApellidoEmpleado { get; private set; } = string.Empty;

        [Required]
        public int TipoPermiso { get; private set; }

        [Required]
        public DateTime FechaPermiso { get; private set; }

        private Permiso() { }

        public static Permiso Create(
            int id,
            string nombreEmpleado,
            string apellidoEmpleado,
            int TipoPermiso,
            DateTime fechaPermiso
            )
        {
            if (string.IsNullOrEmpty(nombreEmpleado)) throw new ArgumentException("El NombreEmpleado no puede ser vacio");
            if (string.IsNullOrEmpty(apellidoEmpleado)) throw new ArgumentException("El ApellidoEmpleado no puede ser vacio");
            if (TipoPermiso == 0) throw new ArgumentException("Necesita un tipo de permiso de forma obligatoria");

            return new Permiso
            {
                //Id = id,
                NombreEmpleado = nombreEmpleado,
                ApellidoEmpleado = apellidoEmpleado,
                TipoPermiso = TipoPermiso,
                FechaPermiso = fechaPermiso
            };
        }

        public void Update(
            string nombreEmpleado,
            string apellidoEmpleado,
            int tipoPermiso,
            DateTime fechaPermiso
        )
        {
            //if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));

            if (string.IsNullOrEmpty(nombreEmpleado)) throw new ArgumentException("El nombre del empleado no puede ser vacio");
            if (string.IsNullOrEmpty(apellidoEmpleado)) throw new ArgumentException("El apellido del empleado no puede ser vacio");
            if (TipoPermiso == 0) throw new ArgumentException("Necesita un tipo de permiso de forma obligatoria");

            //Id = id;
            NombreEmpleado = nombreEmpleado;
            ApellidoEmpleado = apellidoEmpleado;
            TipoPermiso = tipoPermiso;
            FechaPermiso = fechaPermiso;

        }



    }
}
