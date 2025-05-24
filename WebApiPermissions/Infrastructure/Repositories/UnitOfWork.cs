using Application.Interfaces;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private  IPermisosRepository? _permisosRepository; // => throw new NotImplementedException();

        public UnitOfWork(ApplicationDbContext context) { _context = context; }
        IPermisosRepository IUnitOfWork.Permisos => _permisosRepository ??= new PermisoRepository(_context);

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            //Uso del Garbage Collector

            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
