using BaseRepositories.Interfaces;
using Group.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Group.Repositories
{
    public class InMemoryGroupInRepository: BaseRepositories.InMemoryRepository<Entities.Group, Entities.GroupID>, Group.Interfaces.GroupRepositoryInterface
    {
        private Dictionary<GroupType, GroupID> _ids = new Dictionary<GroupType, GroupID>();
        public InMemoryGroupInRepository()
        {
            this._database = new Dictionary<GroupID, Group.Entities.Group>();
            this._mutex = new Lock();
            foreach (var grouptype in Enum.GetValues(typeof(Group.Entities.GroupType))) {
                this._ids.Add((GroupType)grouptype, new GroupID((GroupType)grouptype));
            }
            
        }
        private GroupID _NextID(GroupType group_type) {
            _ids[group_type] = ++_ids[group_type];
            return _ids[group_type];
        }
        public async override Task<Entities.GroupID> InsertAsync(Entities.Group group_object) {
            group_object.Id = _NextID(group_object.Type);
            if (!await this.IsExistsAsync(group_object.Id))
            {
                _database.Add(group_object.Id, group_object);
                return group_object.Id;
            }
            throw new Entities.GroupAlreadyExistsError($"A group with id ({group_object.Id}) already exists");
        }

        public async Task<List<Entities.Group>> FindAsync(
            GroupType? type,
            GroupState? state,
            string? regexPattern,
            bool decreaseOrder)
        {
            IEnumerable<Entities.Group> q = _database.Values;

            if (type is { } t)
                q = q.Where(g => g.Type == t);

            if (state is { } s)
                q = q.Where(g => g.State == s);

            Regex? rx = null;
            if (regexPattern is not null)
            {
                rx = new Regex(regexPattern, RegexOptions.Compiled);
                q = q.Where(g => rx.IsMatch(g.Id.ToString()));
            }

            Func<Entities.Group, int> keySelector = g =>
                rx is null ? -1 : FetchKeyForOrdering(rx.Match(g.Id.ToString()));

            q = decreaseOrder ? q.OrderByDescending(keySelector) : q.OrderBy(keySelector);

            return await Task.FromResult(q.ToList());
        }

        private static int FetchKeyForOrdering(Match match)
        {
            if (!match.Success) return -1;
            var group = match.Groups[1];
            return group.Success && int.TryParse(group.Value, out var v) ? v : -1;
        }
    }
}
