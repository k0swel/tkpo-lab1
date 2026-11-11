using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRepositories
{
    class ObjectDoesNotExistsError : Exception {
        public ObjectDoesNotExistsError(string text) : base(text) { }
    }
    class ObjectExistsError : Exception {
        public ObjectExistsError(string text) : base(text) { }
    }
}
