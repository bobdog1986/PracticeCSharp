using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///707. Design Linked List

    public class MyLinkedList
    {

        private class MyListNode
        {
            public int val;
            public MyListNode next;

            public MyListNode(int val,MyListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        private int count;
        private MyListNode head;
        public MyLinkedList()
        {
            head = new MyListNode(-1);
            count = 0;
        }

        public int Get(int index)
        {
            if (index < 0 || index >= count) return -1;
            var node = head;
            while (index-- >= 0)
            {
                node = node.next;
            }
            return node.val;
        }

        public void AddAtHead(int val)
        {
            var curr = new MyListNode(val,head.next);
            head.next = curr;
            count++;
        }

        public void AddAtTail(int val)
        {
            var node = head;
            while (node.next!=null)
            {
                node = node.next;
            }
            node.next = new MyListNode(val);
            count++;
        }

        public void AddAtIndex(int index, int val)
        {
            if (index < 0 || index > count) return;
            var node = head;
            while (index-- > 0)
            {
                node = node.next;
            }
            var insert = new MyListNode(val, node.next);
            node.next = insert;
            count++;
        }

        public void DeleteAtIndex(int index)
        {
            if (index < 0 || index >= count) return;
            var node = head;
            while (index-- > 0)
            {
                node = node.next;
            }
            node.next = node.next.next;
            count--;
        }
    }
}
