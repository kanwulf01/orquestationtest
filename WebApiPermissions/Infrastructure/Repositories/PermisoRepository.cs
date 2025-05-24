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
    public class PermisoRepository : IPermisosRepository
    {
        private readonly ApplicationDbContext _context;
        public PermisoRepository(ApplicationDbContext applicationDbContext) { _context = applicationDbContext; }

        public async Task AddAsync(Permiso permisos, CancellationToken cancellationToken = default)
        {
            await _context.Permisos.AddAsync(permisos, cancellationToken);
        }

        public async Task<IEnumerable<Permiso>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            // Aca devolver la relacion con tipo de permisos - pendiente
            return await _context.Permisos.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Permiso?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Permisos.FindAsync([id], cancellationToken);
        }

        public void Update(Permiso permisos)
        {
            _context.Permisos.Update(permisos);
        }
    }
}
