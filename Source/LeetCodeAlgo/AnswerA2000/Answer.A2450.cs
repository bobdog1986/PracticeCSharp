using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2481. Minimum Cuts to Divide a Circle
        //public int NumberOfCuts(int n)
        //{
        //    if (n==1) return 0;
        //    if (n%2==0) return n/2;
        //    return n;
        //}

        ///2482. Difference Between Ones and Zeros in Row and Column
        //public int[][] OnesMinusZeros(int[][] grid)
        //{
        //    int m = grid.Length;
        //    int n= grid[0].Length;

        //    int[] onesRow = grid.Select(x => x.Count(o => o==1)).ToArray();
        //    int[] zerosRow = grid.Select(x => x.Count(o => o==0)).ToArray();

        //    int[] onesCol = new int[n];
        //    int[] zerosCol = new int[n];

        //    for(int j = 0; j<n; j++)
        //    {
        //        int ones = 0;
        //        int zeros = 0;
        //        for (int i = 0; i<m; i++)
        //        {
        //            if (grid[i][j]==0) zeros++;
        //            if (grid[i][j]==1) ones++;
        //        }
        //        onesCol[j]=ones;
        //        zerosCol[j]=zeros;
        //    }
        //    int[][] res = new int[m][];
        //    for (int i = 0; i<m; i++)
        //    {
        //        res[i]=new int[n];
        //        for(int j=0;j<n;j++)
        //            res[i][j]= onesRow[i]+onesCol[j]- zerosRow[i]-zerosCol[j];
        //    }
        //    return res;
        //}


        ///2485. Find the Pivot Integer
        //return x that sum of [1,x] == [x,n], if not exist, return -1
        //public int PivotInteger(int n)
        //{
        //    int left = 1;
        //    int right = n;
        //    while (left<=right)
        //    {
        //        int mid = (left+right)/2;
        //        int x = (1+mid)*mid/2;
        //        int y = (mid+n)*(n-mid+1)/2;
        //        if (x==y) return mid;
        //        else if (x<y)
        //        {
        //            left = mid+1;
        //        }
        //        else
        //        {
        //            right= mid-1;
        //        }
        //    }
        //    return -1;
        //}

        ///2486. Append Characters to String to Make Subsequence
        //Return the minimum chars that need to be appended to the end of s so that t becomes a subsequence of s.
        //public int AppendCharacters(string s, string t)
        //{
        //    int i = 0, j = 0;
        //    for(;i<s.Length && j<t.Length;)
        //    {
        //        if (s[i]==t[j])
        //        {
        //            i++;
        //            j++;
        //        }
        //        else
        //        {
        //            i++;
        //        }
        //    }
        //    return t.Length-j;
        //}

        ///2487. Remove Nodes From Linked List
        //Remove every node which has a node with a strictly greater value anywhere to the right side of it.
        public ListNode RemoveNodes_2487(ListNode head)
        {
            if (head==null||head.next==null)
                return head;
            ListNode[] arr = new ListNode[100000];
            int i = -1;
            while (head!=null)
            {
                if (i>=0)
                {
                    if (head.val>arr[i].val)
                    {
                        arr[i]=null;
                        i--;
                    }
                    else
                    {
                        arr[++i] = head;
                        head= head.next;
                    }
                }
                else
                {
                    arr[++i] = head;
                    head = head.next;
                }
            }

            var res = arr[0];
            for (int j = 0; j<=i; j++)
            {
                arr[j].next = arr[j+1];
            }
            arr[i].next=null;
            return res;
        }

        public ListNode RemoveNodes_2487_Recurr(ListNode head)
        {
            if (head==null) return null;
            var next = RemoveNodes_2487_Recurr(head.next);
            if (next == null) return head;
            else
            {
                if (next.val>head.val) return next;
                else
                {
                    head.next = next;
                    return head;
                }
            }
        }

        ///2490. Circular Sentence
        //public bool IsCircularSentence(string sentence)
        //{
        //    var arr = sentence.Split(' ').ToArray();
        //    for (int i = 0; i<arr.Length; i++)
        //    {
        //        int j = (i==arr.Length-1) ? 0 : i+1;
        //        if (arr[i].Last()!=arr[j].First())
        //            return false;
        //    }
        //    return true;
        //}

        ///2491. Divide Players Into Teams of Equal Skill
        //public long DividePlayers(int[] skill)
        //{
        //    Array.Sort(skill);
        //    int n = skill.Length;
        //    long res = 0;
        //    int sum = skill[0] + skill[n-1];
        //    for (int i = 0; i<n/2; i++)
        //    {
        //        if (skill[i] + skill[n-1-i] != sum)
        //            return -1;
        //        res+= skill[i] * skill[n-1-i];
        //    }
        //    return res;
        //}


        ///2492. Minimum Score of a Path Between Two Cities
        ///#TODO
        public int MinScore(int n, int[][] roads)
        {
            List<int[]>[] graph = new List<int[]>[n+1];
            for (int i = 0; i<graph.Length; i++)
                graph[i]=new List<int[]>();

            foreach (var r in roads)
            {
                graph[r[0]].Add(new int[] { r[1], r[2] });
                graph[r[1]].Add(new int[] { r[0], r[2] });
            }

            int res = int.MaxValue;
            var pq = new PriorityQueue<int[], int>();

            return res;
        }
    }
}
