using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRepositories
{
    public class InMemoryRepository<Entity, ID> : BaseRepositories.Interfaces.IRepository<Entity, ID>
        where Entity : class, BaseRepositories.Interfaces.Entity<ID>
        where ID : BaseRepositories.IDInterface<ID>, new()
        
    {
        protected Dictionary<ID, Entity> _database;
        protected ID _id;
        protected Lock _mutex;
        public ID _NextID()
        {
            this._id = ++_id;
            return this._id;
        }

        public InMemoryRepository()
        {
            this._database = new Dictionary<ID, Entity>();
            this._mutex = new Lock();
            this._id = new ID();
        }

        public virtual async Task UpdateAsync(Entity some_object)
        {
            if (!await IsExistsAsync(some_object.Id))
            {
                throw new BaseRepositories.ObjectDoesNotExistsError($"{typeof(Entity).Name} with specified id ({some_object.Id}) does not exists in the repository");
            }
            lock (_mutex)
            {
                _database[some_object.Id] = some_object;
            }
            return;
        }

        public virtual Task<ID> InsertAsync(Entity some_object)
        {
            lock (_mutex)
            {
                ID newID = _NextID();
                some_object.Id = newID;
                _database.Add(newID, some_object);

                return Task.FromResult(newID);
            }
        }
        public virtual Task<bool> IsExistsAsync(ID Id)
        {
            lock (_mutex)
            {
                return Task.FromResult(this._database.ContainsKey(Id));
            }
        }
        public virtual async Task DeleteAsync(ID Id)
        {
            bool is_exists = await IsExistsAsync(Id);
            if (is_exists)
            {
                lock (_mutex)
                {
                    _database.Remove(Id);
                }
                return;
            }
            throw new BaseRepositories.ObjectDoesNotExistsError($"{typeof(Entity).Name} with specified id ({Id}) does not exists in the repository");
        }
        public virtual async Task<Entity?> FindByIDAsync(ID Id)
        {
            bool is_exists = await IsExistsAsync(Id);
            if (is_exists)
            {
                lock (_mutex)
                {
                    return _database[Id];
                }
            }
            return null;
        }

        public virtual Task<UInt64> CountAsync()
        {
            lock (_mutex)
            {
                return Task.FromResult(Convert.ToUInt64(_database.Count));
            }
        }
        public virtual async Task<List<Entity>> GetAll() {
            lock (_mutex) {
                return _database.Values.ToList();
            }
        }

    }
    
    
}
