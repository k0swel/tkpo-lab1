using Course.Entities;
using Course.Interfaces;
using Student.Entities;
using Student.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Services
{
    public class CourseTranscriptService
    {
        private readonly CourseTranscriptRepositoryInterface transcripts_repository;
        private readonly StudentRepositoryInterface student_repository;
        private readonly ICourseRepository course_repository;
        public CourseTranscriptService(CourseTranscriptRepositoryInterface transcripts_repository, StudentRepositoryInterface student_repository, ICourseRepository course_repository)
        {
            this.transcripts_repository = transcripts_repository;
            this.student_repository = student_repository;
            this.course_repository = course_repository;
        }

        public async Task<CourseTranscriptID> AppendGrade(StudentID student_id, Entities.CourseID course_id, Grade grade_object) {
            bool student_is_exists = await student_repository.IsExistsAsync(student_id);
            if (!student_is_exists) throw new Student.Entities.StudentDoesNotExistsError($"A student with id {student_id} does not exists in the repo");
            bool course_is_exists = await course_repository.IsExistsAsync(course_id);
            if (!course_is_exists) throw new Course.Entities.CourseDoesNotExistsError($"A course with id {course_id} does not exists in the repo");
            List<CourseTranscript> current_transcripts_with_same_course = await transcripts_repository.FindByStudentIDAndCourseID(student_id, course_id);

            CourseTranscript transcript_object = new CourseTranscript(student_id, course_id, grade_object);

            if (current_transcripts_with_same_course.Count == 0)
            {
                return await transcripts_repository.InsertAsync(transcript_object);
            }
            else {
                foreach (CourseTranscript transcript in current_transcripts_with_same_course) {
                    if (transcript.getState() == CourseTranscriptState.ACTUAL) {
                        throw new CourseTranscriptImpossibleSetGrade($"It's not possible set grade, because a student has an actual grades. Set all previous grades to INHERITED if u want to set actual grade");
                    }
                }
                return await transcripts_repository.InsertAsync(transcript_object);
            }
        }
        public async Task RevokeGrade(CourseTranscriptID transcript_id) {
            CourseTranscript? transcript_object = await transcripts_repository.FindByIDAsync(transcript_id);
            if (transcript_object is null) {
                throw new CourseTranscriptDoesNotExists($"A course transcript with id {transcript_id} does not exists");
            }
            transcript_object.setState(CourseTranscriptState.INHERITED);
            await transcripts_repository.UpdateAsync(transcript_object);
        }
        public async Task<List<CourseTranscript>> GetTranscriptsByStudentID(StudentID student_id) {
            return await transcripts_repository.FindByStudentID(student_id);
        }
    }
}
