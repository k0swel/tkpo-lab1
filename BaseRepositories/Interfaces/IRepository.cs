using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRepositories.Interfaces
{
    public interface IRepository<TEntity, ID> where TEntity: Entity<ID> where ID : IDInterface<ID>, new()
    {
        public abstract ID _NextID();

        public  Task UpdateAsync(TEntity some_object);

        public  Task<ID> InsertAsync(TEntity some_object);
        public  Task<bool> IsExistsAsync(ID Id);
        public Task DeleteAsync(ID Id);
        public  Task<TEntity?> FindByIDAsync(ID Id);

        public Task<UInt64> CountAsync();
        public Task<List<TEntity>> GetAll();
    }
}
