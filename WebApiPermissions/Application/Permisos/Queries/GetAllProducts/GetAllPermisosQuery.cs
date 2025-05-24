using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Permisos.Queries.GetAllProducts
{
    public class GetAllPermisosQuery : IRequest<IEnumerable<PermisosDto>>
    {
    }
}
