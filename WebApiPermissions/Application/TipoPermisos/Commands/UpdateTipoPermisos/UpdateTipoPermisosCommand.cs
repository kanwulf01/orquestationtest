using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TipoPermisos.Commands.UpdateTipoPermisos
{


    public class UpdateTipoPermisosCommand : IRequest<PermisosTypeDto>
    {
        public int Id { get; set; }

        public string Descripcion { get; set; } = string.Empty;

    }
}
