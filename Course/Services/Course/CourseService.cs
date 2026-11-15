using Course.Entities;
using System.Runtime.InteropServices;

namespace Course.Services
{
    public class CourseService
    {
        private Interfaces.ICourseRepository _repository;
        private Course.Services.CourseEnrollmentService _course_enrollment_service;
        public CourseService(Interfaces.ICourseRepository repository) {
            _repository = repository;
        }
        public CourseService(Interfaces.ICourseRepository repository, Course.Services.CourseEnrollmentService course_enrollment_service) {
            _repository = repository;
            _course_enrollment_service = course_enrollment_service;
        }

        public async Task<Entities.Course> GetCourse(Entities.CourseID id) {
            Entities.Course course_object = (await _repository.FindByIDAsync(id)) ?? throw new Entities.CourseDoesNotExistsError($"Course with id {id} does not exists");
            return course_object;
        }
        public async Task<Entities.CourseID> CreateCourse(Entities.Course course) {
            Entities.CourseID course_id = await _repository.InsertAsync(course);
            return course_id;
        }
        public async Task DeleteCourse(Entities.CourseID course_id)
        {
            Entities.Course course_object = await _repository.FindByIDAsync(course_id) ?? throw new Entities.CourseDoesNotExistsError($"Course with id {course_id} does not exists");
            LinkedList<Enrollment> enrollments_to_course = await _course_enrollment_service.FindByCourseID(course_id);
            foreach (var enrollment in enrollments_to_course) {
                if (enrollment.Group is { } g) await _course_enrollment_service.UnenrollGroup(course_id, g);
                if (enrollment.Teacher is { } t) await _course_enrollment_service.UnenrollTeacher(course_id, t);
                if (enrollment.Student is { } s) await _course_enrollment_service.UnenrollStudent(course_id, s);
            }
            await _repository.DeleteAsync(course_id);
            return;
        }
    }
}
