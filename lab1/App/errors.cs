using System;
using System.Collections.Generic;
using System.Text;

namespace lab1.App
{
    public class IncorrectKindOfRepostiryError(string text) : Exception(text);
}
