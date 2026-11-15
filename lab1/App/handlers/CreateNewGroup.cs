using Group.Entities;
using Student.Entities;
using Student.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    internal partial class Application
    {
        private async void _create_new_group_handler()
        {
            Group.Services.GroupService group_service = new(this._group_repository);
            GroupType group_type_prompt = _prompt_group_type();
            Group.Entities.Group group = new(Group.Entities.GroupState.ACTIVE, group_type_prompt);
            try
            {
                Group.Entities.GroupID groupID = await group_service.CreateGroup(group);
                _print_payload_text($"A group was created . Group ID is {groupID}");
            }
            catch (GroupAlreadyExistsError err) {
                _print_error(err.Message);
            }
        }
        private GroupType _prompt_group_type() {

            _print_text("Please choose a type of your group");
            for (int i = 0; i < Enum.GetValues(typeof(GroupType)).Length; i++)
            {
                var prev = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\t[{i + 1}] ");
                Console.ForegroundColor = prev;
                Console.WriteLine(Enum.GetValues(typeof(GroupType)).GetValue(i));
            }
            GroupType group_type = (GroupType)Convert.ToInt32(_get_prompt());
            return group_type;
        }
    }
}
