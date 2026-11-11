using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Entities
{
    public class IncrementDecrimentNotPossibleError : Exception {
        public IncrementDecrimentNotPossibleError(string text) : base(text) { }
    }
    public class IncorrectGroupTypeError : Exception {
        public IncorrectGroupTypeError(string text) : base(text) { }
    }
    public class GroupDoesNotExistsError : Exception
    {
        public GroupDoesNotExistsError(string text) : base(text) { }
    }
    public class IncorrectGroupStateError : Exception
    {
        public IncorrectGroupStateError(string text) : base(text) { }
    }

    public class GroupAlreadyExistsError : Exception
    {
        public GroupAlreadyExistsError(string text) : base(text) { }
    }
}
