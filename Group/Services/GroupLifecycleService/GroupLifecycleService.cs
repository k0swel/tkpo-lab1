using Group.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Services
{
    public class GroupLifecycleService
    {
        GroupService group_service;
        Group.Interfaces.GroupRepositoryInterface group_repository;
        public GroupLifecycleService(GroupService group_service, Group.Interfaces.GroupRepositoryInterface group_repository) {
            this.group_service = group_service;
            this.group_repository = group_repository;
        }
        public async Task Disband(GroupID group_id)
        {
            Entities.Group group = await this.group_service.GetGroup(group_id);
            group.Disband();
            await group_repository.UpdateAsync(group);
            return;
        }
    }
}
