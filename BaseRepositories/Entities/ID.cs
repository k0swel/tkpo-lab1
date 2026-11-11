using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRepositories.Entities
{
    public record ID: BaseRepositories.IDInterface<ID>
    {
        public object Value => _value;
        protected ulong _value;
        public ID(ulong Value)
        {
            this._value = Value;
        }
        public ID()
        {
            this._value = 0;
        }
        public static ID operator +(ID Left, ID Right)
        {
            return new ID(Left._value + Right._value);
        }
        public static ID operator +(ID lvalue, ulong rvalue) {
            return new ID(lvalue._value + rvalue);
        }
        public static ID operator ++(ID lvalue) {
            return lvalue._value + 1;
        }
        public static implicit operator ID(ulong value) => new(value);
    }
}
