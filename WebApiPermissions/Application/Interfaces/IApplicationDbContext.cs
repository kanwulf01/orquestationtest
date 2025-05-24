using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Permiso> Permisos {  get; }  
        DbSet<TipoPermiso> TipoPermisos {  get; }  

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
