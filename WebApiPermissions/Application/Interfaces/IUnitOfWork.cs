using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IPermisosRepository PermisosRepository { get; }

        ITipoPermisosRepository TipoPermisosRepository { get; }

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}
