using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        private async void _disband_existing_group() {
            Group.Services.GroupService group_service = new(this._group_repository);
            Group.Services.GroupLifecycleService group_lifecycle_service = new(group_service, this._group_repository);
            Group.Entities.GroupID group_id = _get_prompt("Please put existing group ID");
            try
            {
                await group_lifecycle_service.Disband(group_id);
                _print_payload_text($"A group with id {group_id} was disbanded");
            }
            catch (Group.Entities.GroupDoesNotExistsError err) {
                _print_error(err.Message);
            }

        }
    }
}
