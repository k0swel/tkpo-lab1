using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Teacher.Entities;

namespace Teacher.Repositories
{
    public class InMemoryTeacherRepository : BaseRepositories.InMemoryRepository<Entities.Teacher, Entities.TeacherID> , Interfaces.ITeacherRepository { 
    
    }
    //public class InMemoryTeacherRepository: Teacher.Interfaces.ITeacherRepository
    //{
    //    private Dictionary<Entities.TeacherID, Entities.Teacher> database;
    //    private Lock mutex;
    //    private Entities.TeacherID _id;

    //    private Entities.TeacherID _NextID() {
    //        this._id = this._id + 1;
    //        return this._id;
    //    }

    //    public InMemoryTeacherRepository() {
    //        this.database = new Dictionary<Entities.TeacherID, Entities.Teacher>();
    //        this.mutex = new Lock();
    //        this._id = new Entities.TeacherID(0);
    //    }

    //    public async Task UpdateAsync(Entities.Teacher teacher_object) {
    //        if (!  await IsExists(teacher_object.Id) )
    //        {
    //            throw new TeacherDoesNotExistsError($"Teacher with specified id {teacher_object.Id} does not exists in the repository");
    //        }
    //        lock (mutex)
    //        {
    //            database[teacher_object.Id] = teacher_object;
    //        }
    //        return;
    //    }

    //    public Task<Entities.TeacherID> InsertAsync(Entities.Teacher teacher_object) {
    //        lock (mutex)
    //        {
    //            Entities.TeacherID newID = _NextID();
    //            database.Add(newID, teacher_object);

    //            return Task.FromResult(newID);
    //        }
    //    }
    //    public Task<bool> IsExists(Entities.TeacherID teacher_id) {
    //        lock (mutex) {
    //            return Task.FromResult(this.database.ContainsKey(teacher_id));
    //        }
    //    }
    //    public async Task DeleteAsync(TeacherID teacher_id) {
    //        bool is_exists = await IsExists(teacher_id);
    //        if (is_exists)
    //        {
    //            lock (mutex)
    //            {
    //                database.Remove(teacher_id);
    //            }
    //            return;
    //        }
    //        throw new Repositories.TeacherDoesNotExistsError($"A teacher with specified ID {teacher_id} does not exists");
    //    }
    //    public async Task<Entities.Teacher?> FindByIDAsync(Entities.TeacherID teacher_id) {
    //        bool is_exists = await IsExists(teacher_id);
    //        if (is_exists) {
    //            lock (mutex) {
    //                return database[teacher_id];
    //            }
    //        }
    //        return null;
    //    }

    //    public Task<UInt64> Count() {
    //        lock (this.mutex) {
    //            return Task.FromResult(Convert.ToUInt64(database.Count));
    //        }
    //    }

    //}
}
