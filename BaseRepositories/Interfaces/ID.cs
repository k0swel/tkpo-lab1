using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRepositories {
    public interface IDInterface<T> where T: IDInterface<T>, new() {
        public object Value { get; }
        public static abstract T operator ++(T lvalue);
        public string ToString();
    }
}
