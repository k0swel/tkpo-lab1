using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRepositories.Interfaces
{
    public interface Entity<ID> where ID: BaseRepositories.IDInterface<ID>, new()
    {
        public ID Id { get; set; }
    }
}
