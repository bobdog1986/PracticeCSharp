using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1652. Defuse the Bomb
        ///Given the circular array code and an integer key k, return the decrypted code to defuse the bomb!
        public int[] Decrypt(int[] code, int k)
        {
            var ans=new int[code.Length];
            for(int i=0; i<code.Length; i++)
            {
                if (k > 0)
                {
                    for (int j = 1; j <= k; j++)
                    {
                        var index = (i + j) % code.Length;
                        ans[i] += code[index];
                    }
                }
                else if (k < 0)
                {
                    for (int j = -1; j >= k; j--)
                    {
                        var index = (i + j) % code.Length;
                        if (index < 0)
                            index += code.Length;
                        ans[i] += code[index];
                    }
                }
            }
            return ans;
        }

        ///1669. Merge In Between Linked Lists
        ///Remove list1's nodes from the ath node to the bth node, and put list2 in their place.
        public ListNode MergeInBetween(ListNode list1, int a, int b, ListNode list2)
        {
            int i = 0;
            var node = list1;
            ListNode tail1 = null;
            ListNode nextHead1 = null;
            while (node != null)
            {
                i++;
                if (i == a) { tail1 = node; }
                node = node.next;
                if (i == b)
                {
                    nextHead1 = node.next;
                    break;
                }
            }
            node = list2;
            while (node.next != null)
            {
                node = node.next;
            }
            node.next=nextHead1;
            tail1.next = list2;
            return list1;
        }
    }
}
