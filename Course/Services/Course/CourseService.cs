using Course.Entities;
using System.Runtime.InteropServices;

namespace Course.Services
{
    public class CourseService
    {
        private Interfaces.ICourseRepository _repository;
        public CourseService(Interfaces.ICourseRepository repository) {
            _repository = repository;
        }

        public async Task<Entities.Course> GetCourse(Entities.CourseID id) {
            Entities.Course course_object = await _repository.FindByIDAsync(id) ?? throw new Entities.CourseDoesNotExistsError($"Course with id {id} does not exists");
            return course_object;
        }
        public async Task<Entities.CourseID> CreateCourse(Entities.Course course) {
            Entities.CourseID course_id = await _repository.InsertAsync(course);
            return course_id;
        }
        public async Task DeleteCourse(Entities.CourseID course_id)
        {
            Entities.Course course_object = await _repository.FindByIDAsync(course_id) ?? throw new Entities.CourseDoesNotExistsError($"Course with id {course_id} does not exists");
            await _repository.DeleteAsync(course_id);
            return;
        }
    }
}
