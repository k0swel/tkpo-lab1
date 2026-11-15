using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;


namespace Course.Repositories
{
    public class InMemoryCourseRepository : BaseRepositories.InMemoryRepository<Course.Entities.Course, Course.Entities.CourseID>, Interfaces.ICourseRepository { };
    //public class InMemoryCourseRepository:  Interfaces.ICourseRepository
    //{
    //    private Dictionary<Entities.CourseID, Entities.Course> database = new Dictionary<Entities.CourseID, Entities.Course>();
    //    private System.Threading.Lock mutex = new Lock();
    //    private Entities.CourseID _id = new Entities.CourseID(0);
    //    private Entities.CourseID _NextID() {
    //        _id = new Entities.CourseID(this._id.Value + 1);
    //        return _id;
    //    }
    //    public Task UpdateAsync(Entities.Course course_object) {
    //        lock (mutex) {
    //            Entities.CourseID course_id = course_object.Id;
    //            if (!database.ContainsKey(course_id)) {
    //                throw new Repositories.Course.CourseDoesNotExists($"Course {course_id} does not exist in the repository");
    //            }
    //            database[course_id] = course_object;
    //        }
    //        return Task.CompletedTask;

    //    }
    //    public Task DeleteAsync(Entities.CourseID course_id) {
    //        lock (mutex)
    //        {
    //            bool result = database.Remove(course_id);
    //            if (!result)
    //            {
    //                throw new Repositories.Course.CourseDoesNotExists($"Course {course_id} does not exist in the repository");
    //            }
    //        }
    //        return Task.CompletedTask;
    //    }        public Task <Entities.Course?> FindByIDAsync(Entities.CourseID course_id) {
    //        lock (mutex)
    //        {
    //            Entities.Course? course_object;
    //            database.TryGetValue(course_id, out course_object);
    //            return Task.FromResult(course_object);
    //        }
    //    }
    //    public async Task<UInt64> CountAsync() {
    //        UInt64 count;
    //        lock (mutex)
    //        {
    //           count = Convert.ToUInt64(this.database.Count);
    //        }
    //        return count;
    //    }
    //    public Task<Entities.Course> InsertAsync(Entities.Course course_object) {
    //        lock (mutex)
    //        {
    //            Entities.CourseID id = _NextID();
    //            course_object = course_object with { Id = id };
    //            database.Add(id, course_object);
    //        }
    //        return Task.FromResult(course_object);
    //    }
    //    public Task<bool> IsExistsAsync(Entities.CourseID course_id) {
    //        lock (mutex)
    //        {
    //            return Task.FromResult(this.database.ContainsKey(course_id));
    //        }
    //    }
    //}
}
