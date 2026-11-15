using Course.Entities;
using Student.Entities;
using Student.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        private async Task _getStudentByItsIDHandler()
        {
            StudentService student_service = new(this._student_repository);
            Student.Entities.StudentID student_id = Convert.ToUInt64(_get_prompt("Please put student id (integer) to the terminal"));
            Student.Entities.Student student_object;
            Course.Services.CourseTranscriptService course_transcript_service = new(this._course_transcript_repository, this._student_repository, this._course_repository);
            try
            {
                student_object = await student_service.GetStudent(student_id);
                _print_payload_text(student_object.ToString());
            }
            catch (Student.Entities.StudentDoesNotExistsError err) {
                _print_error(err.Message);
                return;
            }

            List<CourseTranscript> student_transcripts =  await course_transcript_service.GetTranscriptsByStudentID(student_id);
            if (student_transcripts.Count > 0)
            {
                foreach (var transcript in student_transcripts)
                {
                    _print_payload_text(transcript.ToString());
                }
                return;
            }
            _print_error($"A student does not have any transcriptions yet");
            
        }
    }
}
