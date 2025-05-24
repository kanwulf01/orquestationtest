using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Permisos.Queries.GetPermisosById
{
   

    public class GetPermisosByIdQuery : IRequest<PermisosDto?>
    {
        public int Id { get; set; }

        public GetPermisosByIdQuery(int id)
        {
            Id = id;
        }
    }
}
