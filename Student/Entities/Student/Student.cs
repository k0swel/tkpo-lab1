using BaseRepositories.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Student.Entities
{
    public record Student : BaseRepositories.Interfaces.Entity<StudentID>
    {
        public StudentStatus Status { get; private set; }
        public StudentID Id { get; set; }
        public StudentName Name { get; private set; }
        public Student(
            StudentName Name,
            StudentStatus Status
            )
        {
            this.Id = 0;
            this.Name = Name;
            //this.Capacity = Capacity;
            this.Status = Status;

        }
        public void Rename(StudentName Name)
        {
            // Не придумал пока никакой бизнес-логики по которой можно было бы менять имя.
            this.Name = Name;
        }
        public void Expel() {
            if (this.Status == StudentStatus.ACTIVE || this.Status == StudentStatus.VACATION) {
                this.Status = StudentStatus.EXPELLED;
                return;
            }
            throw new IncorrectStudentState($"Incorrect student state (StudentId = {this.Id}) during expelling. Current StudentState is {nameof(this.Status)}");
        }
        public void GoOnVacation() {
            if (this.Status == StudentStatus.ACTIVE) {
                this.Status = StudentStatus.VACATION;
                return;
            }
            throw new IncorrectStudentState($"Incorrect student state (StudentId = {this.Id}) during vacationing. Current state is {nameof(this.Status)}");
        }
        public void Restore() {
            this.Status = StudentStatus.ACTIVE;
        }
        public void LeaveFromVacation()
        {
            if (this.Status == StudentStatus.VACATION)
            {
                this.Status = StudentStatus.ACTIVE;
                return;
            }
            throw new IncorrectStudentState($"Incorrect student state (StudentId = {this.Id}) during leaving from vacation. Current state is {nameof(this.Status)}");
        }
    };
    public sealed record StudentID : ID, BaseRepositories.IDInterface<StudentID>
    {
        public new object Value => _value;
        public StudentID(ulong Value)
        {
            this._value = Value;
        }
        public StudentID(string string_value) {
            ulong value;
            try
            {
                value = Convert.ToUInt64(string_value);
                this._value = value;
            }
            catch (FormatException ) {
                throw new StudentIDIncorrectError($"Incorrect format for StudentID. Just pass digital identificator for student. E.g: 2");
            }
        }
        public StudentID()
        {
            this._value = 0;
        }
        public static StudentID operator +(StudentID left, StudentID right)
        {
            return new StudentID(left._value + right._value);
        }
        public static StudentID operator ++(StudentID Value)
        {
            return new StudentID(Value._value + 1);
        }
        public static implicit operator StudentID(ulong value) {
            return new StudentID(value);
        }
        public static implicit operator StudentID(string value) {
            return new(value);
        }
        public override string ToString()
        {
            return $"{this._value}";
        }

    }
    public record StudentName(string firstName, string lastName, string? surName) : BaseRepositories.Entities.Name(firstName, lastName, surName) {
        public static StudentName TryParse(string name)
        {
            string[] splitted_text = name.Split(" ", 3);
            if (splitted_text.Length < 2)
                throw new IncorrectNameError("Incorrect name. \"<LastName> <FirstName> (SurName)\" format");
            if (splitted_text.Length == 2)
            {
                string lastName = splitted_text[0];
                string firstName = splitted_text[1];
                return new StudentName(firstName, lastName, null);
            }
            {
                string lastName = splitted_text[0];
                string firstName = splitted_text[1];
                string SurName = splitted_text[2];
                return new StudentName(firstName, lastName, SurName);
            }
        }
        public static implicit operator StudentName(string text) {
            return StudentName.TryParse(text);
        }
        public override string ToString()
        {
            return $"{LastName} {FirstName} {SurName}";
        }
    }
    public enum StudentStatus
    {
        ACTIVE,
        EXPELLED,
        VACATION,
    }
}
