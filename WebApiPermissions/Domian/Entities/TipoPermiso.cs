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
            int id,
            string descripcion
        )
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));

            if (string.IsNullOrEmpty(descripcion)) throw new ArgumentException("La descripción no puede ser vacio");

            Id = id;
            Descripcion = descripcion;

        }
    }
}
