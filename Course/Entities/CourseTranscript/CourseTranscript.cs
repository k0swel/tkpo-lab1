using BaseRepositories;
using Student.Entities;
using System;
using System.Collections.Generic;
using System.Text;

using CourseID = Course.Entities.CourseID;

namespace Course.Entities
{
    public record CourseTranscript: BaseRepositories.Interfaces.Entity<CourseTranscriptID>
    {
        public CourseTranscriptID Id { get; set; }
        public StudentID student_id;
        public CourseID course_id;
        public Grade grade { get; private set; }
        private CourseTranscriptState State { get; set; }
        public CourseTranscript(StudentID student_id, CourseID course_id, Grade grade) {
            this.Id = 0UL;
            this.student_id = student_id;
            this.course_id = course_id;
            this.grade = grade;
            this.State = CourseTranscriptState.ACTUAL;
        }
        public void setGrade(Grade new_grade) {
            this.grade = new_grade;
        }
        public CourseTranscriptState getState() {
            return this.State;
        }
        public void setState(CourseTranscriptState state)
        {
            if (this.State == CourseTranscriptState.INHERITED && state == CourseTranscriptState.ACTUAL) {
                throw new ImpossibleChangeCourseTranscriptStateError($"A currect state of courseTranscript is {Enum.GetName(typeof(CourseTranscriptState), this.State)}, but u trying set it to ACTUAL. It is not possible. Try to create new CourseTranscript");
            }
            this.State = state;
        }
        public override string ToString()
        {
            return $"CourseID: {course_id}, StudentID: {student_id}, Grade: {grade.ToString()}";
        }

    }

    public enum CourseTranscriptState {
        ACTUAL,
        INHERITED
    }

    public record CourseTranscriptID : BaseRepositories.Entities.ID, IDInterface<CourseTranscriptID> {
        public override string ToString() {
            return $"{this.Value}";
        }
        public CourseTranscriptID(ulong Value)
        {
            this._value = Value;
        }
        public static implicit operator CourseTranscriptID(ulong value) {
            return new CourseTranscriptID(value);
        }
        public CourseTranscriptID()
        {
            this._value = 0;
        }
        public static CourseTranscriptID operator ++(CourseTranscriptID courseidtranscript_object) {
            return new(courseidtranscript_object._value + 1);
        }
    }
}
