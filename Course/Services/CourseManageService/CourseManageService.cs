using BaseRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Services.CourseManageService
{
    public class CourseManageService
    {
        private Interfaces.ICourseRepository _repository;
        private CourseService course_service;
        public CourseManageService(Interfaces.ICourseRepository repository, CourseService course_service) {
            this._repository = repository;
            this.course_service = course_service;
        }
        public async Task RenameCourse(Entities.CourseID id, Entities.CourseName name)
        {
            Entities.Course course_object = await course_service.GetCourse(id);
            course_object.Rename(name);
            await _repository.UpdateAsync(course_object);
            return;
        }
        public async Task ChangeDescription(Entities.CourseID id, Entities.CourseDescription description)
        {
            Entities.Course course_object = await course_service.GetCourse(id);
            course_object.ChangeDescription(description);
            await _repository.UpdateAsync(course_object);
            return;
        }
        public async Task OpenCourse(Entities.CourseID id)
        {
            Entities.Course course_object = await course_service.GetCourse(id);
            course_object.Enable();
            await _repository.UpdateAsync(course_object);
            return;
        }
        public async Task CloseCourse(Entities.CourseID id)
        {
            Entities.Course course_object = await course_service.GetCourse(id);
            course_object.Disable();
            await _repository.UpdateAsync(course_object);
            return;
        }
    }
}
