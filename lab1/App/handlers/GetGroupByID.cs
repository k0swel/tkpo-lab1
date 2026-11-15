using Group.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        private async void _get_group_by_id() {
            Group.Services.GroupService group_service = new(this._group_repository);
            Group.Entities.GroupID group_id = _get_prompt("Please put a group identifier");
            try
            {
                _print_payload_text((await group_service.GetGroup(group_id)).ToString());
            }
            catch (GroupDoesNotExistsError err) {
                _print_error(err.Message);
            }
        }
    }
}
