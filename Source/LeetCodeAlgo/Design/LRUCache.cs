using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    /// 146. LRU Cache
    /// Design a data structure that follows the constraints of a Least Recently Used (LRU) cache.
    ///The functions get and put must each run in O(1) average time complexity.

    public class LRUCache
    {
        private class DLinkedNode
        {
            public DLinkedNode prev;
            public DLinkedNode next;
            public int key, value;
            public DLinkedNode(int _key, int _value)
            {
                this.key = _key;
                this.value = _value;
            }
        }

        private DLinkedNode head;
        private DLinkedNode tail;
        private Dictionary<int, DLinkedNode> map = new Dictionary<int, DLinkedNode>();
        private int capacity;

        public LRUCache(int _capacity)
        {
            this.capacity = _capacity;

            //useful nodes between head and tail
            this.head = new DLinkedNode(0, 0);
            this.tail = new DLinkedNode(0, 0);
            this.head.next = tail;
            this.tail.prev = head;
        }

        public int Get(int key)
        {
            if (map.ContainsKey(key))
            {
                //update it
                var node = map[key];
                remove(node);
                insert(node);
                return node.value;
            }
            else
            {
                return -1;
            }
        }

        public void Put(int key, int value)
        {
            //put an exist node need update the frequence, so delete it first, then add again at next of head
            if (map.ContainsKey(key))
            {
                remove(map[key]);
            }
            if (map.Count == capacity)
            {
                remove(tail.prev);
            }
            insert(new DLinkedNode(key, value));
        }

        private void remove(DLinkedNode node)
        {
            map.Remove(node.key);
            node.prev.next = node.next;
            node.next.prev = node.prev;
        }

        private void insert(DLinkedNode node)
        {
            //insert to next of head
            map.Add(node.key, node);
            var headNext = head.next;
            head.next = node;
            node.prev = head;
            headNext.prev = node;
            node.next = headNext;
        }
    }

}
