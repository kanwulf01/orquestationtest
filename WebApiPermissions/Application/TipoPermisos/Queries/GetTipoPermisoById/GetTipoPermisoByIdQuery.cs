using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TipoPermisos.Queries.GetTipoPermisoById
{
    public class GetTipoPermisoByIdQuery: IRequest<PermisosTypeDto?>
    {
        public int Id { get; set; }

        public GetTipoPermisoByIdQuery(int id) {
            Id = id;
        }
    }
}
