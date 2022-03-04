using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    /// 1146. Snapshot Array
    public class SnapshotArray
    {
        private int id;
        private Dictionary<int, int>[] dict;
        public SnapshotArray(int length)
        {
            id = 0;
            dict = new Dictionary<int, int>[length];
            for (int i = 0; i < length; i++)
                dict[i] = new Dictionary<int, int>();
        }

        public void Set(int index, int val)
        {
            var map = dict[index];
            if(map.ContainsKey(id))map[id] = val;
            else map.Add(id, val);
        }

        public int Snap()
        {
            return id++;
        }

        public int Get(int index, int snap_id)
        {
            var map=dict[index];
            while (snap_id >= 0)
            {
                if(map.ContainsKey(snap_id))return map[snap_id];
                snap_id--;
            }
            return 0;
        }
    }

    public class SnapshotArray2
    {
        private int id;
        private Dictionary<int, Dictionary<int, int>> dict;
        public SnapshotArray2(int length)
        {
            id = 0;
            dict = new Dictionary<int, Dictionary<int, int>>();
        }

        public void Set(int index, int val)
        {
            if (!dict.ContainsKey(id)) dict.Add(id, new Dictionary<int, int>());
            if (dict[id].ContainsKey(index)) dict[id][index] = val;
            else dict[id].Add(index, val);
        }

        public int Snap()
        {
            return id++;
        }

        public int Get(int index, int snap_id)
        {
            while (snap_id >= 0)
            {
                if (dict.ContainsKey(snap_id) && dict[snap_id].ContainsKey(index)) return dict[snap_id][index];
                else snap_id--;
            }
            return 0;
        }
    }
}
