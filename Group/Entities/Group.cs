using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Group.Entities
{
    public record Group : BaseRepositories.Interfaces.Entity<GroupID> {
        public GroupID Id { get; set; }
        public GroupState State { get; private set; }
        public GroupType Type { get; private set; }

        public Group(GroupState State) {
            this.Id = 0;
            this.State = State;
        }
        public void Disband() {
            if (this.State == GroupState.DISBANDED) {
                throw new Entities.IncorrectGroupStateError($"Incorrect state of group (id = {this.Id}). Imposible disband because a group already disbanded");
            }
            this.State = GroupState.DISBANDED;
        }
    }

    public record GroupID: BaseRepositories.Entities.ID, BaseRepositories.IDInterface<GroupID> {
        public new object Value => _value;
        private new string _value;
        public GroupID() {
            this._value = "";
        }
        public GroupID(string value) {
            this._value = value;
        }
        static public implicit operator GroupID(string text) {
            return new GroupID(text);
        }
        static public int operator +(GroupID lvalue, ulong rvalue)
        {
            throw new NotImplementedException("operator + (GroupID, ulong) does not implemented");
        }
        public GroupID(GroupType type_group) {
            string year = Convert.ToString(DateTime.UtcNow.Year)[0..2];
            switch (type_group) {
                case GroupType.SPECIALITY:
                    this._value = $"{year}2-0";
                    break;
                case GroupType.BACHELOR:
                    this._value = $"{year}1-0";
                    break;
                case GroupType.MASTER:
                    this._value = $"{year}3-0";
                    break;
            }
            throw new IncorrectGroupTypeError($"Incorrect GroupType");
        }

        public static GroupID operator ++(GroupID groupId)
        {
            Match m = Regex.Match(groupId.ToString(), @"^(\d{3})-(\d+)$");
            if (!m.Success || !ulong.TryParse(m.Groups[2].Value, out ulong number))
                throw new Entities.IncrementDecrimentNotPossibleError(
                    $"Increment operation over GroupID ({groupId}) is not possible: unsuitable ID format.");

            if (number == ulong.MaxValue)
                throw new OverflowException($"GroupID ({groupId}) counter overflow.");

            string primaryPart = m.Groups[1].Value;
            string next = (number + 1).ToString(new string('0', m.Groups[2].Length)); // сохранить ширину
            return new GroupID($"{primaryPart}-{next}");
        }
        public static GroupID operator +(GroupID left, GroupID right) {
            throw new NotImplementedException($"GroupID не поддерживает operator+(GroupID, GroupID)");
        }
        public override string ToString() {
            return this._value;
        }
        public static implicit operator string(GroupID e) {
            return e.ToString();
        }
        public static implicit operator GroupID(ulong value) {
            throw new NotImplementedException($"Conversion from integer to GroupID is not impossible. Try use conversion from string");
        }

    }
    public record GroupIDPattern{
        private Regex Pattern { get; }
        private readonly GroupID _groupId;
        public Match Match { get; private set; }
        public GroupIDPattern(Regex pattern, GroupID group_id) {
            Pattern = pattern;
            _groupId = group_id;
            Match = Pattern.Match(group_id.ToString());
        }
        public bool IsMatch() {
            return this.Pattern.IsMatch(_groupId);
        }
        public bool HasCaptures() {
            return this.Match.Captures.Count > 0;
        }
    
    }
    public enum GroupState { 
        ACTIVE,
        DISBANDED,
    }
    public enum  GroupType {
        BACHELOR,
        SPECIALITY,
        MASTER,
        POSTGRADUATE
    }
}
