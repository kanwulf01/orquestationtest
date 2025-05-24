using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    public class TipoPermisoRepository : ITipoPermisosRepository
    {
        private readonly ApplicationDbContext _context;
        public TipoPermisoRepository(ApplicationDbContext applicationDbContext) { _context = applicationDbContext; }

        public async Task AddAsync(TipoPermiso permisos, CancellationToken cancellationToken = default)
        {
            await _context.TipoPermisos.AddAsync(permisos, cancellationToken);
        }

        public async Task<IEnumerable<TipoPermiso>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            
            return await _context.TipoPermisos.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TipoPermiso?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.TipoPermisos.FindAsync([id], cancellationToken);
        }

        public void Update(TipoPermiso permisos)
        {
            _context.TipoPermisos.Update(permisos);
        }
    }
}
