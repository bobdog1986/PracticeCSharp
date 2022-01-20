﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///844. Backspace String Compare
        ///Given two strings s and t, return true if they are equal when both are typed into empty text editors.
        ///'#' means a backspace character.Note that after backspacing an empty text, the text will continue empty.
        public bool BackspaceCompare(string s, string t)
        {
            var arr1=s.ToArray();
            var arr2=t.ToArray();

            Stack<char> stack1 = new Stack<char>();
            Stack<char> stack2 = new Stack<char>();

            for(int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == '#')
                {
                    if(stack1.Count > 0)
                        stack1.Pop();
                }
                else
                {
                    stack1.Push(arr1[i]);
                }
            }

            for (int j = 0; j < arr2.Length; j++)
            {
                if (arr2[j] == '#')
                {
                    if (stack2.Count > 0)
                        stack2.Pop();
                }
                else
                {
                    stack2.Push(arr2[j]);
                }
            }

            if (stack1.Count != stack2.Count)
                return false;
            int count = stack1.Count;
            for(int i = 0; i < count; i++)
            {
                if (stack1.Pop() != stack2.Pop())
                    return false;
            }

            return true;

        }
        /// 849. Maximize Distance to Closest Person
        public int MaxDistToClosest(int[] seats)
        {
            int max = 1;

            int len = 0;
            for (int i = 0; i < seats.Length; i++)
            {
                if (seats[i] == 0)
                {
                    len++;
                }
                else
                {
                    if (len > 0)
                    {
                        if (len == i)
                        {
                            //continous seats from 0-index
                            max = Math.Max(max, len);
                        }
                        else
                        {
                            max = Math.Max(max, (len + 1) / 2);
                        }

                        len = 0;
                    }
                }
            }

            //continous seats to last-index
            if (len > 0)
                max = Math.Max(max, len);
            return max;
        }

        ///875. Koko Eating Bananas
        ///There are n piles of bananas, the ith pile has piles[i] bananas.
        ///The guards have gone and will come back in h hours.
        ///Find min numb to eat all bananas in h hours. each time can only eat 1 index;
        public int MinEatingSpeed(int[] piles, int h)
        {
            if (piles.Length == h)
                return piles.Max();

            int low = 1, high = 1000000000;
            int mid = (low + high) / 2;
            while (low <= high)
            {
                int sum = 0;
                for (int i = 0; i < piles.Length; i++)
                    sum += (int)Math.Ceiling(1.0 * piles[i] / mid);

                if (sum > h)
                    low = mid + 1;
                else
                    high = mid - 1;

                mid = (low + high) / 2;
            }
            return low;
        }

        /// 876. Middle of the Linked List
        public ListNode MiddleNode(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            var next = head.next;

            int count = 1;
            //List<int> nodes = new List<int>();
            while (next != null)
            {
                count++;
                //nodes.Add(head.val);
                next = next.next;
            }

            int len = count / 2;

            while (len > 0)
            {
                head = head.next;
                len--;
            }

            return head;
        }
    }
}