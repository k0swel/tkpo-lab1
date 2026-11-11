// See https://aka.ms/new-console-template for more information

using Teacher.Services;
using Teacher.Repositories;
using Teacher.Entities;
using Group.Repositories;
using Group.Services;
using Student.Entities;
using Student.Services;
using Course.Entities;

class Program {
    static async Task Main(string[] args) {
        Course.Repositories.InMemoryCourseEnrollmentRepository enrollment_repository = new();
        Teacher.Repositories.InMemoryTeacherRepository teacher_repository = new();
        Group.Repositories.InMemoryGroupInRepository group_repository = new();
        Student.Repositories.InMemoryStudentRepository student_repository = new();
        Course.Repositories.InMemoryCourseRepository course_repository = new();

        Course.Services.CourseEnrollmentService enrollment_service = new(enrollment_repository, student_repository, teacher_repository, group_repository, course_repository);
        Course.Services.CourseService course_service = new(course_repository);
        List<Task<EnrollmentID>> tasks = new();
        Course.Entities.Course networks = new Course.Entities.Course("Сети и системы передачи информации", "Хуета ебаная", Course.Entities.CourseStatus.ENABLED);
        await course_service.CreateCourse(networks);
        
        for (int i = 0; i < 200; i++) {
            Student.Entities.Student student = new Student.Entities.Student("Леоненко Роман", StudentStatus.ACTIVE);
            StudentID studentID_object = await student_repository.InsertAsync(student);
            Console.WriteLine($"Student with ID {studentID_object.ToString()} was created");
            tasks.Add(enrollment_service.EnrollStudent(networks.Id, student.Id));
            
        }
        List<EnrollmentID> task_list = (await Task.WhenAll(tasks.ToArray())).ToList();
        Course.Entities.CourseID course = (await enrollment_service.GetEnrollment(task_list[5])).Course;
        Console.WriteLine((await course_service.GetCourse(course)).Name);

    }
}