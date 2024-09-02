using System;
using System.Threading;
using System.Threading.Tasks;

namespace FicharApi.Services
{
    public interface IStandardServiceBase<TModel>
    {
        Task<int> CreateAsync(TModel model, CancellationToken cancellationToken);
    }
} 