using System.Threading;
using System.Threading.Tasks;
using SnapObjects.Data;
using DWNet.Data;
using System;

namespace FicharApi.Services.Impl
{
    public abstract class StandardServiceBase<TModel> : IStandardServiceBase<TModel>
    {
        protected readonly DataContext _dataContext;

        public StandardServiceBase(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<int> CreateAsync(TModel model, CancellationToken cancellationToken)
        {
            var dataStore = new DataStore<TModel>(_dataContext);
            
            dataStore.Add(model);

            return await dataStore.UpdateAsync(cancellationToken);
        }      
    }
}