using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TipoPermiso
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public string Descripcion { get; private set; } = string.Empty;

        public ICollection<Permiso> Permisos { get; private set; } = new List<Permiso>();



        private TipoPermiso() { }

        public static TipoPermiso Create(
            int id,
            string descripcion
            )
        {
            if (string.IsNullOrEmpty(descripcion)) throw new ArgumentException("La descripción no puede ser vacio");

            return new TipoPermiso
            {
                Id = id,
                Descripcion = descripcion
            };
        }

        public void Update(
            string descripcion
        )
        {
            if (string.IsNullOrEmpty(descripcion)) throw new ArgumentException("La descripción no puede ser vacio");
            Descripcion = descripcion;

        }
    }
}
