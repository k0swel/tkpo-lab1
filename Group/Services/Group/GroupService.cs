using Group.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Services
{
    public class GroupService
    {
        private readonly Interfaces.GroupRepositoryInterface _repository;
        public GroupService(Interfaces.GroupRepositoryInterface repository)
        {
            this._repository = repository;
        }
        public async Task<GroupID> CreateGroup(Entities.Group group_object) {
            GroupID groupID = await _repository.InsertAsync(group_object);
            return groupID;
        }
        public async Task<Entities.Group> GetGroup(Entities.GroupID group_id) {
            Entities.Group? group = await _repository.FindByIDAsync(group_id);
            if (group is null)
                throw new Entities.GroupDoesNotExistsError($"Group with the specified group_id ({group_id}) does not exist in the repo");
            return group;
        }
    }
}
