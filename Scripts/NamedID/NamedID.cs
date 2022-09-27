using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


struct NamedId
{
    private uint _id;
    private string _str;

    public struct Comparer : IEqualityComparer<NamedId>
    {
        public bool Equals(NamedId x, NamedId y)
        {
            return x._id == y._id;
        }

        public int GetHashCode(NamedId obj)
        {
            return (int)obj._id;
        }
    }

    private static Dictionary<string, uint> _dict_string_id = new Dictionary<string, uint>();

    public NamedId(uint in_id, string in_name)
    {
        _id = in_id;
        _str = in_name;
    }

    public static NamedId GetNamedId(string inName)
    {
        if (_dict_string_id.TryGetValue(inName, out uint id))
            return new NamedId(id, inName);
        id = (uint)_dict_string_id.Count + 1;
        _dict_string_id.Add(inName, id);
        return new NamedId(id, inName);
    }
}
