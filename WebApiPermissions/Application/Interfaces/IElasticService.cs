using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nest;


namespace Application.Interfaces
{
    public interface IElasticService
    {
        Task EnsureIndexExistAsync();

        Task<IndexResponse?> IndexMessageCreatePermisosAsync(PermisosPostDtos permiso);
    }
}
