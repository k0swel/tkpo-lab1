using Group.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Interfaces
{
    public interface GroupIDGeneratorInterface
    {
        public Task<List<Entities.Group>> FindAsync(GroupType? type, GroupState? state, GroupIDPattern? pattern);
    }
}
