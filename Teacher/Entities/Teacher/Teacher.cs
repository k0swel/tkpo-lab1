using BaseRepositories.Entities;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Teacher.Entities
{
    public class Teacher: BaseRepositories.Interfaces.Entity<TeacherID>
    {
        public TeacherStatus Status { get; private set; }
        public TeacherID Id { get; set; }
        public TeacherName Name { get; private set; }

        public Teacher(
        TeacherName Name,
        TeacherStatus Status)
        {
            this.Id = 0;
            this.Name = Name;
            this.Status = Status;

        }
        public void Rename(TeacherName name) {
            if (name.FirstName is not null && name.LastName is not null)
            {
                this.Name = name;
                return;
            }
            throw new Errors.TeacherNameIsNotCorrect($"New Teacher Name {name} is not correct. It's need to have minimum FirstName and LastName");
        }
        public void GoOnVacation() {
            if (this.Status == TeacherStatus.WORKING) {
                this.Status = TeacherStatus.VACATION;
                return;
            }
            throw new Errors.TeacherStatusIsNotCorrect($"Teacher {this.Name} cant go to the vacation, because his status is {nameof(this.Status)}, not {nameof(TeacherStatus.WORKING)}");
        }
        public void LeaveFromVacation() {
            if (this.Status == TeacherStatus.VACATION)
            {
                this.Status = TeacherStatus.WORKING;
                return;
            }
            throw new Errors.TeacherStatusIsNotCorrect($"Teacher {this.Name} cant leave from vacation, because his status is {nameof(this.Status)}, not {nameof(TeacherStatus.VACATION)}");
        }
        public void Fire()
        {
            this.Status = TeacherStatus.FIRED;
        }
    };
    public record TeacherID : BaseRepositories.Entities.ID, BaseRepositories.IDInterface<TeacherID> {
        public TeacherID(ulong Value) {
            this._value = Value;
        }
        public TeacherID() {
            this._value = 0;
        }
        public static TeacherID operator +(TeacherID Left, TeacherID Right) {
            return new TeacherID(Left._value + Right._value);
        }
        public static TeacherID operator ++(TeacherID Value)
        {
            return new TeacherID(Value._value + 1);
        }
        public static implicit operator TeacherID(ulong value) => new(value);
        public override string ToString()
        {
            return $"{this._value}";
        }
    };
    public enum TeacherStatus
    {
        WORKING,
        VACATION,
        FIRED
    }

    public sealed record TeacherName(string firstName, string lastName, string? SurName) : BaseRepositories.Entities.Name(firstName, lastName, SurName) {
        private static TeacherName TryParse(string name)
        {
            string[] splitted_text = name.Split(" ", 3);
            if (splitted_text.Length < 2)
                throw new IncorrectNameError("Incorrect name. \"<LastName> <FirstName> (SurName)\" format");
            if (splitted_text.Length == 2)
            {
                string lastName = splitted_text[0];
                string firstName = splitted_text[1];
                return new TeacherName(firstName, lastName, null);
            }
            {
                string lastName = splitted_text[0];
                string firstName = splitted_text[1];
                string SurName = splitted_text[2];
                return new TeacherName(firstName, lastName, SurName);
            }
        }
        public static implicit operator TeacherName(string text)
        {
            return TeacherName.TryParse(text);
        }
    }
}
