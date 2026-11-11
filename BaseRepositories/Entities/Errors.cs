using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRepositories.Entities
{
    public class IncorrectNameError(string text) : Exception(text);
}
