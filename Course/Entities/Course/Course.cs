using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Course.Entities
{
    public record Course: BaseRepositories.Interfaces.Entity<CourseID>
    {
        public CourseStatus Status { get; private set; }
        public CourseID Id { get;  set; }
        public CourseName Name { get; private set; }
        public CourseDescription Description { get; private set; }
        //public CourseCapacity Capacity { get; private set; }

        public Course(
        CourseName Name,
        CourseDescription Description,
        //CourseCapacity Capacity,
        CourseStatus Status)
        {
            this.Id = 0;
            this.Name = Name;
            this.Description = Description;
            //this.Capacity = Capacity;
            this.Status = Status;

        }

        public void Enable()
        {
            if (Status == CourseStatus.ENABLED)
                throw new Entities.CourseAlreadyEnabledError($"Course {Id} is already enabled");
            else if (Status == CourseStatus.DISABLED)
            {
                this.Status = CourseStatus.ENABLED;
            }
        }

        public void Disable()
        {
            if (Status == CourseStatus.DISABLED)
                throw new Entities.CourseAlreadyEnabledError($"Course {Id} is already disabled");
            else if (Status == CourseStatus.ENABLED)
            {
                this.Status = CourseStatus.DISABLED;
            }
        }

        public void Rename(CourseName text) {
            // Не придумал пока никакой бизнес-логики по которой можно было бы менять имя.
            this.Name = text;
        }
        public void ChangeDescription(CourseDescription description)
        {
            // Не придумал пока никакой бизнес-логики по которой можно было бы менять описание.
            this.Description = description;
        }
    };
    public sealed record CourseID: BaseRepositories.Entities.ID, BaseRepositories.IDInterface<CourseID>  {
        public new object Value => _value;
        public CourseID(ulong Value) {
            this._value = Value;
        }
        public CourseID(string _value) {
            ulong value;
            try
            {
                value = Convert.ToUInt64(_value);
                this._value = value;
            }
            catch (FormatException ) {
                throw new CourseIDIncorrectFormat($"Incorrect format for CourseID. Just pass a digital identificator. For example: 2");
            }
        }
        public CourseID() {
            this._value = 0;
        }
        public static CourseID operator +(CourseID left, CourseID right) {
            return new CourseID(left._value + right._value);
        }
        public static CourseID operator ++(CourseID Value)
        {
            return new CourseID(Value._value + 1);
        }
        public static implicit operator CourseID(ulong value) {
            return new CourseID(value);
        }
        public static implicit operator CourseID(string value)
        {
            return new CourseID(value);
        }
        public override string ToString() {
            return $"{this._value}";
        }
        public object GetID() {
            return this.Value;
        }
    }
    public sealed record CourseName(string Value) {
        public static implicit operator CourseName(string text)
        {
            return new CourseName(text);
        }
        public static implicit operator string(CourseName coursename_object)
        {
            return coursename_object.Value;
        }
        public override string ToString() {
            return this.Value;
        }
    }
    public sealed record CourseDescription(string Value) {
        public static implicit operator CourseDescription(string text)
        {
            return new CourseDescription(text);
        }
        public override string ToString()
        {
            return this.Value;
        }
    }
    public enum CourseStatus
    {
        DISABLED,
        ENABLED
    }
    // legacy-code
    //public record CourseCapacity(UInt32 Value) : IComparable<CourseCapacity> {
    //    public int CompareTo(CourseCapacity? other) {
    //        return this.Value.CompareTo(other?.Value);
    //    }
    //    public static bool operator <(CourseCapacity left, CourseCapacity right) {
    //        return left.CompareTo(right) < 0;
    //    }
    //    public static bool operator >(CourseCapacity left, CourseCapacity right)
    //    {
    //        return left.CompareTo(right) > 0;
    //    }

    //    public static bool operator <=(CourseCapacity left, CourseCapacity right)
    //    {
    //        return left.CompareTo(right) < 0 || left.CompareTo(right) == 0;
    //    }
    //    public static bool operator >=(CourseCapacity left, CourseCapacity right)
    //    {
    //        return left.CompareTo(right) > 0 || left.CompareTo(right) == 0;
    //    }
    //}
}
