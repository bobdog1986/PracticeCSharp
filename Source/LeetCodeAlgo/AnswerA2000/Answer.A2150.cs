using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2150. Find All Lonely Numbers in the Array
        ///You are given an integer array nums. A number x is lonely when it appears only once,
        ///and no adjacent numbers (i.e. x + 1 and x - 1) appear in the array.
        ///Return all lonely numbers in nums.You may return the answer in any order.
        ///0 <= nums[i] <= 10^6
        public IList<int> FindLonely(int[] nums)
        {
            List<int> res = new List<int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach(var n in nums)
            {
                if (dict.ContainsKey(n)) dict[n]++;
                else dict.Add(n, 1);
            }
            foreach(var key in dict.Keys)
            {
                if (dict[key] == 1 && !dict.ContainsKey(key - 1) && !dict.ContainsKey(key + 1))
                    res.Add(key);
            }
            return res;
        }
        /// 2185. Counting Words With a Given Prefix
        public int PrefixCount(string[] words, string pref)
        {
            return words.Where(word => word.StartsWith(pref)).Count();
        }

        ///2190. Most Frequent Number Following Key In an Array
        ///0 <= i <= n - 2, nums[i] == key and, nums[i + 1] == target.
        ///Return the target with the maximum count.
        public int MostFrequent(int[] nums, int key)
        {
            int max = 0;
            int res = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            for(int i = 0; i < nums.Length-1; i++)
            {
                if(nums[i] == key)
                {
                    if(map.ContainsKey(nums[i+1]))map[nums[i+1]]++;
                    else map.Add(nums[i+1], 1);
                    if (map[nums[i + 1]] > max)
                    {
                        max = map[nums[i+1]];
                        res = nums[i + 1];
                    }
                }
            }
            return res;
        }
        ///2195. Append K Integers With Minimal Sum, #PriorityQueue, #Heap
        ///You are given an integer array nums and an integer k.
        ///Append k unique positive integers that do not appear in nums to nums such that the resulting total sum is minimum.
        ///Return the sum of the k integers appended to nums.
        public long MinimalKSum(int[] nums, int k)
        {
            long res = 0;
            PriorityQueue<int,int> priorityQueue = new PriorityQueue<int,int>();
            foreach(var n in nums)
                priorityQueue.Enqueue(n,n);//min heap

            int lastIndex = 0;
            while (priorityQueue.Count > 0)
            {
                int currIndex = priorityQueue.Dequeue();
                if (lastIndex == currIndex) continue;
                else
                {
                    //how many nums in range [lastIndex + 1,currIndex - 1], inclusive
                    int count = currIndex - 1 - (lastIndex+1) + 1;
                    if (count >= k) count = k;//if exceed k, only append k nums
                    res += (lastIndex + 1 + lastIndex + count) * (long)count / 2;
                    k -= count;
                    lastIndex = currIndex;//update lastIndex
                }
            }

            if (k > 0)// if still k>0
            {
                res += (lastIndex + 1 + lastIndex + k) * (long)k / 2;
            }

            return res;
        }
        /// 2196. Create Binary Tree From Descriptions
        ///descriptions[i] = [parenti, childi, isLefti] indicates that parenti is the parent of childi in a binary tree of unique values. Furthermore,
        ///If isLefti == 1, then childi is the left child of parenti.
        ///If isLefti == 0, then childi is the right child of parenti.
        ///Construct the binary tree described by descriptions and return its root.
        public TreeNode CreateBinaryTree(int[][] descriptions)
        {
            //set contains all nodes,but only the root's value is true
            Dictionary<TreeNode, bool> set = new Dictionary<TreeNode, bool>();
            Dictionary<int, TreeNode> dict = new Dictionary<int, TreeNode>();
            foreach(var desc in descriptions)
            {
                TreeNode parent = null;
                if (!dict.ContainsKey(desc[0])) dict.Add(desc[0], new TreeNode(desc[0]));
                parent = dict[desc[0]];
                if (!set.ContainsKey(parent)) set.Add(parent,true);


                TreeNode child = null;
                if (!dict.ContainsKey(desc[1])) dict.Add(desc[1], new TreeNode(desc[1]));
                child = dict[desc[1]];
                if (!set.ContainsKey(child)) set.Add(child, false);
                else set[child] = false;

                if (desc[2] == 1) parent.left = child;
                else parent.right = child;
            }

            return set.FirstOrDefault(x => x.Value).Key;
        }
    }
}
