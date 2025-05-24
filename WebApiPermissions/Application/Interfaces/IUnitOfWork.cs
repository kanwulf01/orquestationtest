using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IPermisosRepository Permisos { get; }

        ITipoPermisosRepository TipoPermisos { get; }

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}
