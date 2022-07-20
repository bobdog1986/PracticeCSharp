using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///400. Nth Digit
        ///Given an integer n, return the nth digit of the infinite integer sequence [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, ...].
        ///1 <= n <= 231 - 1
        public int FindNthDigit(int n)
        {
            int len = 1, i = 1;
            long range = 9;
            while (n > len * range)
            {
                n -=(int)( len * range);
                len++;
                range *= 10;
                i *= 10;
            }

            i += (n - 1) / len;
            var s = i.ToString();
            return (int)(s[(n - 1) % len]-'0');
        }

        ///401. Binary Watch
        ///A binary watch has 4 LEDs on the top which represent the hours (0-11), and the 6 LEDs on the bottom represent the minutes (0-59).
        ///Each LED represents a zero or one, with the least significant bit on the right.
        public IList<string> ReadBinaryWatch_Bits(int turnedOn)
        {
            List<string> res = new List<string>();
            for(int hour = 0; hour < 12; hour++)
            {
                int hourBits = ReadBinaryWatch_GetBitsCount(hour);
                if (hourBits > turnedOn) continue;
                for (int minute = 0; minute < 60; minute++)
                {
                    int minuteBits = ReadBinaryWatch_GetBitsCount(minute);
                    if(hourBits+ minuteBits == turnedOn)
                    {
                        res.Add($"{hour}:{minute.ToString("00")}");
                    }
                }
            }
            return res;
        }

        private int ReadBinaryWatch_GetBitsCount(int n)
        {
            int res = 0;
            while (n > 0)
            {
                if ((n & 1) == 1) res++;
                n>>= 1;
            }
            return res;
        }

        public IList<string> ReadBinaryWatch(int turnedOn)
        {
            List<string> res =new List<string>();
            Dictionary<int, List<int>> hourDict = new Dictionary<int, List<int>>();
            List<int> hours = new List<int>() { 1, 2, 4, 8 };
            for(int i = 0; i <= hours.Count; i++)
            {
                List<int> list = new List<int>();
                ReadBinaryWatch_GetHour_BackTracking(i, 0, 0, hours, list);
                if(list.Count>0)
                    hourDict.Add(i, list);
            }

            Dictionary<int, List<int>> minuteDict = new Dictionary<int, List<int>>();
            List<int> minutes = new List<int>() { 1, 2, 4, 8,16,32 };
            for (int i = 0; i <= minutes.Count; i++)
            {
                List<int> list = new List<int>();
                ReadBinaryWatch_GetMinute_BackTracking(i, 0, 0, minutes, list);
                if (list.Count > 0)
                    minuteDict.Add(i, list);
            }

            for(int i = 0; i <= turnedOn; i++)
            {
                if (!hourDict.ContainsKey(i)) continue;
                if (!minuteDict.ContainsKey(turnedOn-i)) continue;
                foreach(var hour in hourDict[i])
                {
                    foreach(var minute in minuteDict[turnedOn - i])
                    {
                        res.Add($"{hour}:{minute.ToString("00")}");
                    }
                }
            }
            return res;
        }

        private void ReadBinaryWatch_GetHour_BackTracking(int count, int index, int curr, List<int> list, List<int> res,int max= 11, int min = 0)
        {
            if(count == 0)
            {
                if (curr >= min && curr <= max && !res.Contains(curr))
                    res.Add(curr);
                return;
            }
            if (index >= list.Count) return;
            ReadBinaryWatch_GetHour_BackTracking(count, index + 1, curr, list, res);
            for (int i = index; i < list.Count; i++)
            {
                ReadBinaryWatch_GetHour_BackTracking(count - 1, i + 1, curr + list[i], list, res);
            }
        }

        private void ReadBinaryWatch_GetMinute_BackTracking(int count, int index, int curr, List<int> list, List<int> res, int max = 59, int min = 0)
        {
            if (count == 0)
            {
                if (curr >= min && curr <= max && !res.Contains(curr))
                    res.Add(curr);
                return;
            }
            if (index >= list.Count) return;
            ReadBinaryWatch_GetMinute_BackTracking(count, index + 1, curr, list, res);
            for (int i = index; i < list.Count; i++)
            {
                ReadBinaryWatch_GetMinute_BackTracking(count - 1, i + 1, curr + list[i], list, res);
            }
        }
        /// 402. Remove K Digits
        ///a non-negative integer num, and an integer k, return the smallest possible integer after removing k digits from num.
        ///1 <= k <= num.length <= 105
        ///num consists of only digits.
        ///num does not have any leading zeros except for the zero itself.
        public string RemoveKdigits(string num, int k)
        {
            if (k == num.Length) return "0";

            var digits = num.ToArray().ToList();
            int i = 0;
            while (i++ < k)
            {
                int j = 0;
                while (j < digits.Count)
                {
                    if (j == digits.Count - 1 || digits[j] > digits[j + 1])
                    {
                        digits.RemoveAt(j);
                        break;
                    }
                    j++;
                }
            }

            i = 0;
            int count = digits.Count;
            while (i++ < count)
            {
                if (digits.Count > 0 && digits[0] == '0')
                {
                    digits.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
            return digits.Count == 0 ? "0" : new string(digits.ToArray());
        }
        ///404. Sum of Left Leaves
        ///Given the root of a binary tree, return the sum of all left leaves.
        public int SumOfLeftLeaves(TreeNode root)
        {
            int sum = 0;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while(queue.Count > 0)
            {
                int size = queue.Count;
                while (size-- > 0)
                {
                    var node = queue.Dequeue();
                    if (node == null) continue;
                    if (node.left != null && node.left.left == null && node.left.right==null) sum += node.left.val;
                    if (node.left != null) queue.Enqueue(node.left);
                    if (node.right != null) queue.Enqueue(node.right);
                }
            }
            return sum;
        }

        ///405. Convert a Number to Hexadecimal
        ///Given an integer num, return a string representing its hexadecimal representation.
        ///For negative integers, two’s complement method is used.
        public string ToHex(int num)
        {
            if(num==0) return "0";
            string res = "";
            List<string> digits = new List<string>() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            int count = 0;
            while (num != 0)
            {
                if (count >= 8) break;
                int hex = num & 15;
                res = digits[hex] + res;
                num >>= 4;//c# negative >> will pad 1 on the 31-bit to keep negative
                count++;
            }
            return res;
        }
        /// 406. Queue Reconstruction by Height, #Greedy
        ///You are given an array of people, people, which are the attributes of some people
        ///in a queue (not necessarily in order). Each people[i] = [hi, ki] represents
        ///the ith person of height hi with exactly ki other people in front who have a height greater than or equal to hi.
        ///Reconstruct and return the queue that is represented by the input array people.
        ///The returned queue should be formatted as an array queue, where queue[j] = [hj, kj]
        ///is the attributes of the jth person in the queue (queue[0] is the person at the front of the queue).
        public int[][] ReconstructQueue(int[][] people)
        {
            var mat = people.OrderBy(p => p[1]).ThenBy(p => p[0]).ToList();
            List<int[]> res = new List<int[]>();
            for(int i = 0; i < mat.Count; i++)
            {
                var p = mat[i];
                int count = p[1];
                int j = 0;
                while (count >= 0 && j < res.Count)
                {
                    if (res[j][0] >= p[0])
                    {
                        if (count == 0)
                        {
                            break;
                        }
                        else
                        {
                            count--;
                            j++;
                        }
                    }
                    else
                    {
                        j++;
                    }
                }
                res.Insert(j, p);
            }
            return res.ToArray();
        }
        /// 409. Longest Palindrome
        ///case sensitive, Aa is different
        public int LongestPalindrome_409(string s)
        {
            if (s.Length <= 1)
                return s.Length;

            Dictionary<char, int> dict = new Dictionary<char, int>();

            foreach (var c in s)
            {
                if (dict.ContainsKey(c))
                {
                    dict[c]++;
                }
                else
                {
                    dict.Add(c, 1);
                }
            }

            int sumOfEven = 0;
            int sumOfOdd = 0;

            foreach (var d in dict)
            {
                sumOfEven += d.Value / 2;
                sumOfOdd += d.Value % 2;
            }

            int ans = sumOfEven * 2;

            if (sumOfOdd > 0)
                ans++;

            return ans;
        }

        ///410. Split Array Largest Sum, #Binary Search
        //non-negative integers and an integer m, you can split the array into m non-empty continuous subarrays.
        //Write an algorithm to minimize the largest sum among these m subarrays.
        //0 <= nums[i] <= 10^6, 1 <= nums.length <= 1000
        public int SplitArray(int[] nums, int m)
        {
            int n = nums.Length;
            int left = 0;
            int right = 1_000_000_000;
            while (left < right)
            {
                int mid = (left + right) / 2;
                int curr = 0;
                int count = 1;
                foreach(var x in nums)
                {
                    if (curr + x > mid)
                    {
                        count++;
                        curr = x;
                    }
                    else curr += x;
                }

                if (count <= m)
                {
                    right=mid;
                }
                else left = mid +1;
            }

            int max = 0;
            int sum = 0;
            foreach (var x in nums)
            {
                if (sum + x > left)
                {
                    sum = x;
                }
                else sum += x;
                max = Math.Max(max, sum);
            }

            return max;
        }

        ///412. Fizz Buzz
        ///Given an integer n, return a string array answer (1-indexed) where:
        ///answer[i] == "FizzBuzz" if i is divisible by 3 and 5.
        ///answer[i] == "Fizz" if i is divisible by 3.
        ///answer[i] == "Buzz" if i is divisible by 5.
        ///answer[i] == i(as a string) if none of the above conditions are true.
        public IList<string> FizzBuzz(int n)
        {
            var ans = new string[n];
            for (int i = 1; i <= n; i++)
            {
                ans[i - 1] = i.ToString();
            }
            for (int i = 3-1; i < n; i += 3)
            {
                ans[i] = "Fizz";
            }
            for (int i = 5-1; i < n; i += 5)
            {
                ans[i] = "Buzz";
            }
            for (int i = 15-1; i < n; i += 15)
            {
                ans[i] = "FizzBuzz";
            }
            return ans;
        }

        /// 413. Arithmetic Slices, #DP
        ///at least 3 nums with same distance, eg. [1,2,3,4], [1,1,1]
        ///-1000 <= nums[i] <= 1000
        ///A subarray is a contiguous subsequence of the array.
        public int NumberOfArithmeticSlices_413(int[] nums)
        {
            int ans = 0;
            int dp = 0;
            for (int i = 2; i < nums.Length; i++)
            {
                if (nums[i] - nums[i - 1] == nums[i - 1] - nums[i - 2])
                {
                    dp++;
                    ans += dp;
                }
                else
                {
                    dp = 0;
                }
            }
            return ans;
        }

        ///414. Third Maximum Number
        ///Given an integer array nums, return the third distinct maximum number in this array.
        ///If the third maximum does not exist, return the maximum number.
        public int ThirdMax(int[] nums)
        {
            var pq = new PriorityQueue<int, long>();
            var set = new HashSet<int>();
            foreach (var x in nums)
            {
                if (!set.Contains(x))
                    pq.Enqueue(x, 0l - x);
                set.Add(x);
            }
            if (set.Count < 3) return pq.Dequeue();
            pq.Dequeue();
            pq.Dequeue();
            return pq.Dequeue();
        }


        /// 415. Add Strings
        ///Given two non-negative integers, num1 and num2 represented as string, return the sum of num1 and num2 as a string.
        // input maybe so big!!! cannot use any int/long type
        public string AddStrings(string num1, string num2)
        {
            if (num1 == "0")
                return num2;

            if (num2 == "0")
                return num1;

            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();

            foreach (var c in num1)
            {
                list1.Insert(0, getDigit(c));
            }
            foreach (var c in num2)
            {
                list2.Insert(0, getDigit(c));
            }

            List<char> ans = new List<char>();
            bool isCarry = false;

            for (int i = 0; i < list1.Count || i < list2.Count; i++)
            {
                int a = 0;
                if (list1.Count > i)
                    a = list1[i];

                int b = 0;
                if (list2.Count > i)
                    b = list2[i];

                int c = a + b;
                if (isCarry)
                    c++;

                isCarry = c / 10 == 1;

                ans.Insert(0, getChar(c % 10));
            }

            if (isCarry)
                ans.Insert(0, '1');

            return string.Join("", ans);
        }

        public char getChar(int c)
        {
            return (char)(c + '0');
        }

        public int getDigit(char c)
        {
            return c - '0';
        }

        ///416. Partition Equal Subset Sum, #DP, #Knapsack
        //non-empty nums(nums[i]>=1), find can be partitioned into two subsets with equal sum.
        public bool CanPartition(int[] nums)
        {
            int sum = nums.Sum();
            if (sum % 2 != 0) return false;
            int half = sum / 2;
            int n = nums.Length;
            bool[][] dp = new bool[n + 1][];
            for (int i = 0; i < dp.Length; i++)
                dp[i] = new bool[half + 1];
            dp[0][0] = true;
            for (int i = 1; i < dp.Length; i++)
                dp[i][0] = true;
            for (int i = 1; i < dp.Length; i++)
            {
                for (int j = 1; j <= half; j++)
                {
                    dp[i][j] = dp[i - 1][j];
                    if (j >= nums[i - 1])
                    {
                        dp[i][j] |= dp[i - 1][j - nums[i - 1]];
                    }
                }
            }
            return dp[n][half];
        }

        /// 417. Pacific Atlantic Water Flow. #Graph, #BFS
        ///Return a 2D list where result[i] = [ri, ci] denotes that rain water can flow to both the Pacific and Atlantic.
        public IList<IList<int>> PacificAtlantic(int[][] heights)
        {
            var res=new List<IList<int>>();
            int rowLen = heights.Length;
            int colLen=heights[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            bool[,] visitPacific = new bool[rowLen, colLen];//store all cells that can flow to Pacific
            Queue<int[]> q1=new Queue<int[]>();
            //add seed cells of Pacific
            for (int i=0; i<rowLen; i++)
            {
                if (i == 0)
                {
                    for(int j=0;j<colLen; j++)
                    {
                        q1.Enqueue(new int[] { i, j });
                        visitPacific[i, j] = true;
                    }
                }
                else
                {
                    q1.Enqueue(new int[] { i, 0 });
                    visitPacific[i, 0] = true;
                }
            }

            while (q1.Count > 0)
            {
                var p=q1.Dequeue();
                foreach(var d in dxy4)
                {
                    var r=p[0]+ d[0];
                    var c = p[1] + d[1];
                    if(r>=0 && r<rowLen && c>=0 &&c<colLen && !visitPacific[r, c] && heights[r][c] >= heights[p[0]][p[1]])
                    {
                        visitPacific[r, c] = true;
                        q1.Enqueue(new int[] { r, c });
                    }
                }
            }

            bool[,] visitAtlantic = new bool[rowLen, colLen];//store all cells that can flow to Atlantic
            Queue<int[]> atlanticQ = new Queue<int[]>();
            //add seed cells of Atlantic
            for (int i = 0; i < rowLen; i++)
            {
                if (i == rowLen-1)
                {
                    for (int j = 0; j < colLen; j++)
                    {
                        atlanticQ.Enqueue(new int[] { i, j });
                        visitAtlantic[i, j] = true;
                    }
                }
                else
                {
                    atlanticQ.Enqueue(new int[] { i, colLen-1 });
                    visitAtlantic[i, colLen - 1] = true;
                }
            }
            while (atlanticQ.Count > 0)
            {
                var p = atlanticQ.Dequeue();
                if(visitPacific[p[0], p[1]])
                {
                    res.Add(new List<int>() { p[0], p[1] });
                }
                foreach (var d in dxy4)
                {
                    var r = p[0] + d[0];
                    var c = p[1] + d[1];
                    if (r >= 0 && r < rowLen && c >= 0 && c < colLen&& !visitAtlantic[r, c] && heights[r][c] >= heights[p[0]][p[1]])
                    {
                        visitAtlantic[r, c] = true;
                        atlanticQ.Enqueue(new int[] { r, c });
                    }
                }
            }
            return res;
        }


        ///419. Battleships in a Board, #DFS
        public int CountBattleships(char[][] board)
        {
            int res = 0;
            int[][] dxy = new int[4][] { new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { -1, 0 } };
            for(int i = 0; i < board.Length; i++)
            {
                for(int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == '.') continue;
                    res++;
                    board[i][j] = '.';
                    CountBattleships_DFS(board, i, j, dxy);
                }
            }
            return res;
        }

        private void CountBattleships_DFS(char[][] board, int row,int col, int[][] dxy)
        {
            foreach(var d in dxy)
            {
                int r = row + d[0];
                int c = col + d[1];
                if(r>=0 && r<board.Length && c >=0 && c<board[0].Length && board[r][c] != '.')
                {
                    board[r][c] = '.';
                    CountBattleships_DFS(board, r, c, dxy);
                }
            }
        }
        /// 421. Maximum XOR of Two Numbers in an Array, #Trie, #Greedy, #Bit
        //return the maximum result of nums[i] XOR nums[j], where 0 <= i <= j < n.
        //1 <= nums.length <= 2 * 10^5, 0 <= nums[i] <= 2^31 - 1
        public int FindMaximumXOR_Greedy(int[] nums)
        {
            int maxResult = 0;
            int mask = 0;
            /*The maxResult is a record of the largest XOR we got so far. if it's 11100 at i = 2, it means
            before we reach the last two bits, 11100 is the biggest XOR we have, and we're going to explore
            whether we can get another two '1's and put them into maxResult

            This is a greedy part, since we're looking for the largest XOR, we start
            from the very begining, aka, the 31st postition of bits. */
            for (int i = 31; i >= 0; i--)
            {

                //The mask will grow like  100..000 , 110..000, 111..000,  then 1111...111
                //for each iteration, we only care about the left parts
                mask = mask | (1 << i);

                HashSet<int> set = new HashSet<int>();
                foreach (int num in nums)
                {
                    /* we only care about the left parts, for example, if i = 2, then we have
                    {1100, 1000, 0100, 0000} from {1110, 1011, 0111, 0010}*/
                    int leftPartOfNum = num & mask;
                    set.Add(leftPartOfNum);
                }

                // if i = 1 and before this iteration, the maxResult we have now is 1100,
                // my wish is the maxResult will grow to 1110, so I will try to find a candidate
                // which can give me the greedyTry;
                int greedyTry = maxResult | (1 << i);

                foreach (int leftPartOfNum in set)
                {
                    //This is the most tricky part, coming from a fact that if a ^ b = c, then a ^ c = b;
                    // now we have the 'c', which is greedyTry, and we have the 'a', which is leftPartOfNum
                    // If we hope the formula a ^ b = c to be valid, then we need the b,
                    // and to get b, we need a ^ c, if a ^ c exisited in our set, then we're good to go
                    int anotherNum = leftPartOfNum ^ greedyTry;
                    if (set.Contains(anotherNum))
                    {
                        maxResult = greedyTry;
                        break;
                    }
                }

                // If unfortunately, we didn't get the greedyTry, we still have our max,
                // So after this iteration, the max will stay at 1100.
            }

            return maxResult;
        }

        public int FindMaximumXOR_Trie(int[] nums)
        {
            var root = new TrieItem();
            nums = nums.ToHashSet().ToArray();
            foreach (int num in nums)
            {
                var curr = root;
                for (int i = 30; i >= 0; i--)
                {
                    int currBit = (num >> i) & 1;
                    if (!curr.intDict.ContainsKey(currBit))
                        curr.intDict.Add(currBit, new TrieItem());
                    curr = curr.intDict[currBit];
                }
            }

            int max = 0;
            foreach (int num in nums)
            {
                var curr = root;
                int currSum = 0;
                for (int i = 30; i >= 0; i--)
                {
                    if (i != 30 &&  (long)currSum + (1 << (i + 1)) -1 <= max) break;
                    // if A[i] is 0, we need 1 and if A[i] is 1, we need 0. Thus, 1 - A[i]
                    int requiredBit = 1 - ((num >> i) & 1);
                    if (curr.intDict.ContainsKey(requiredBit))
                    {
                        currSum |= (1 << i); // set ith bit of curr result
                        curr = curr.intDict[requiredBit];
                    }
                    else
                    {
                        curr = curr.intDict[1 - requiredBit];//must exist itself, every level trie set contain at least 1 element
                    }
                }
                max = Math.Max(max, currSum); // get max number
            }
            return max;
        }

        ///424. Longest Repeating Character Replacement, #Sliding Window
        ///You can choose any character of the string and change it to any other uppercase English character at most k times.
        ///Return the length of the longest substring containing the same letter you can get after performing the above operations.
        ///1 <= s.length <= 10^5, 0 <= k <= s.length, s consists of only uppercase English letters.
        public int CharacterReplacement(string s, int k)
        {
            int res = 0;
            int[] arr = new int[26];
            int left = 0;
            for(int i = 0; i < s.Length; i++)
            {
                arr[s[i] - 'A']++;
                while(left<=i && arr.Max()+k< i-left+1)
                {
                    arr[s[left++] - 'A']--;
                }
                res = Math.Max(res, i - left + 1);
            }
            return res;
        }

        ///429. N-ary Tree Level Order Traversal
        ///Given an n-ary tree, return the level order traversal of its nodes' values.
        public IList<IList<int>> LevelOrder(Node_Childs root)
        {
            var ans=new List<IList<int>>();
            var nodes=new List<Node_Childs>() { root};
            while(nodes.Count > 0)
            {
                var next = new List<Node_Childs>();
                var list = new List<int>();
                foreach(var n in nodes)
                {
                    if (n == null) continue;
                    list.Add(n.val);
                    next.AddRange(n.children);
                }
                if(list.Count>0)ans.Add(list);
                nodes = next;
            }
            return ans;
        }

        ///430. Flatten a Multilevel Doubly Linked List
        public Node Flatten(Node head)
        {
            if (head == null) return null;
            Node curr = head;
            Node prev = null;
            Node childHead = null;
            Stack<Node> stack = new Stack<Node>();
            Stack<Node> subHeads = new Stack<Node>();
            while (curr != null || stack.Count > 0)
            {
                if (curr == null)
                {
                    var top = stack.Pop();
                    var next = top.next;
                    if (prev != null)
                        prev.next = next;
                    if (next != null)
                        next.prev = prev;

                    top.next = childHead;
                    childHead.prev = top;

                    childHead = subHeads.Count > 0 ? subHeads.Pop() : null;

                    curr = next;
                }
                else
                {
                    if (curr.child == null)
                    {
                        prev = curr;
                        curr = curr.next;
                    }
                    else
                    {
                        if (childHead != null)
                            subHeads.Push(childHead);

                        childHead = curr.child;
                        curr.child = null;
                        stack.Push(curr);
                        curr = childHead;
                    }
                }
            }
            return head;
        }

        ///433. Minimum Genetic Mutation, #BFS, #Graph
        ///return the minimum number of mutations needed to mutate from start to end.Or return -1 if not exist
        public int MinMutation(string start, string end, string[] bank)
        {
            bool[] visit=new bool[bank.Length];
            if (!bank.Contains(end)) return -1;
            List<string> list = new List<string>() { start };
            int step = 1;
            while (list.Count > 0)
            {
                List<string> next = new List<string>();
                foreach(var str in list)
                {
                    for(int j=0;j< bank.Length;j++)
                    {
                        if (visit[j]) continue;
                        if(MinMutation_Can(str, bank[j]))
                        {
                            if (bank[j] == end) return step;
                            next.Add(bank[j]);
                            visit[j] = true;
                        }
                    }
                }
                step++;
                list = next;
            }
            return -1;
        }
        private bool MinMutation_Can(string s1, string s2)
        {
            int diff = 0;
            for(int i=0;i<s1.Length; i++)
            {
                if (s1[i] != s2[i]) diff++;
                if (diff >= 2) return false;
            }
            return diff == 1;
        }

        /// 434. Number of Segments in a String
        ///return the number of segments , segment is a contiguous sequence of non-space characters.0 <= s.length <= 300
        public int CountSegments(string s)
        {
            if(s.Length==0) return 0;
            var arr = s.Split(' ');
            return arr.Where(x => x.Length > 0).Count();
        }
        /// 435. Non-overlapping Intervals
        /// there are some embeded intervals, use Math.Min()
        public int EraseOverlapIntervals(int[][] intervals)
        {
            int ans = 0;
            var mat = intervals.OrderBy(x => x[0]).ToList();
            int end = mat[0][1];
            for (int i = 1; i < mat.Count; i++)
            {
                if (mat[i][0] < end)
                {
                    ans++;
                    end = Math.Min(end, mat[i][1]);
                }
                else
                {
                    end = mat[i][1];
                }
            }
            return ans;
        }

        ///436. Find Right Interval, #Binary Search
        //intervals[i] = [starti, endi] and each starti is unique.
        //The right interval for an interval i is an interval j such that startj >= endi and startj is minimized.
        //Note that i may equal j.
        //Return an array of right interval indices for each interval i.
        //If no right interval exists for interval i, then put -1 at index i.
        public int[] FindRightInterval(int[][] intervals)
        {
            int n = intervals.Length;
            if (n == 1) return new int[] { -1 };
            int[] res = new int[n];
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for(int i = 0; i < n; i++)
                dict.Add(intervals[i][0], i);

            var keys=dict.Keys.OrderBy(x=>x).ToArray();

            for(int i = 0; i < n; i++)
            {
                int currEnd = intervals[i][1];
                if (currEnd <= keys[0])
                {
                    res[i] = dict[keys[0]];
                }
                else if (currEnd > keys[n - 1])
                {
                    res[i] = -1;
                }
                else
                {
                    int left = 0;
                    int right = n - 1;
                    while (left < right)
                    {
                        int mid =(left + right)/2;
                        if (keys[mid] >= currEnd)
                        {
                            right = mid;
                        }
                        else
                        {
                            left = mid + 1;
                        }
                    }
                    res[i] = dict[keys[left]];
                }
            }
            return res;
        }

        /// 437. Path Sum III, #BTree, #Prefix Sum
        ///return the number of paths where the sum of the values along the path equals targetSum.
        ///The path does not need to start or end at the root or a leaf, but it must go downwards.
        public int PathSum_437(TreeNode root, int targetSum)
        {
            int res = 0;
            Dictionary<int, int> prefixSumDict = new Dictionary<int, int>();
            PathSum_Recur(root, prefixSumDict, targetSum, ref res);
            return res;
        }
        private void PathSum_Recur(TreeNode root, Dictionary<int, int> prefixSumDict, int targetSum, ref int res)
        {
            if (root == null) return;

            if (root.val == targetSum)
                res++;//find a single-node path

            if (prefixSumDict.ContainsKey(root.val))
                res += prefixSumDict[root.val];//all path go downward to parent of node

            //update all prefixSum by -root.val, then build a new dict
            var nextDict = new Dictionary<int, int>();
            foreach (var key in prefixSumDict.Keys)
            {
                var nextkey = key - root.val;
                if (nextDict.ContainsKey(nextkey)) nextDict[nextkey] += prefixSumDict[key];
                else nextDict.Add(nextkey, prefixSumDict[key]);
            }

            //add or update prefixSum, starting at current node
            if (nextDict.ContainsKey(targetSum - root.val)) nextDict[targetSum - root.val]++;
            else nextDict.Add(targetSum - root.val, 1);

            PathSum_Recur(root.left, nextDict, targetSum, ref res);
            PathSum_Recur(root.right, nextDict, targetSum, ref res);
        }

        /// 438. Find All Anagrams in a string, #Sliding Window
        // Input: s = "cbaebabacd", p = "abc", Output: [0,6]
        // The substring with start index = 0 is "cba", which is an anagram of "abc".
        // The substring with start index = 6 is "bac", which is an anagram of "abc".
        public List<int> FindAnagrams(string s, string p)
        {
            var res = new List<int>();
            if (p.Length > s.Length)
                return res;
            int left = 0;
            int[] arr = new int[26];
            int[] target = new int[26];

            for(int i = 0; i < p.Length; i++)
            {
                arr[s[i] - 'a']++;
                target[p[i] - 'a']++;
            }

            for(int i = p.Length - 1; i < s.Length; i++)
            {
                if (i > p.Length - 1)
                {
                    arr[s[i] - 'a']++;
                    arr[s[left++] - 'a']--;
                }

                bool isEqual = true;
                for (int j = 0; j < 26; j++)
                {
                    if (arr[j] != target[j])
                    {
                        isEqual = false;
                        break;
                    }
                }
                if (isEqual)
                    res.Add(left);
            }
            return res;
        }

        ///441. Arranging Coins, #Binary Search
        ///You have n coins and you want to build a staircase with these coins.
        ///The staircase consists of k rows where the ith row has exactly i coins. The last row of the staircase may be incomplete.
        ///Given the integer n, return the number of complete rows of the staircase you will build.
        public int ArrangeCoins(int n)
        {
            int res = 0;
            int row = 1;
            while (n > 0)
            {
                n -= row++;
                if(n>=0) res++;
            }
            return res;
        }
        public int ArrangeCoins_BinarySearch(int n)
        {
            long left = 0;
            long right = n;
            while (left <= right)
            {
                long mid = left + (right - left) / 2;
                long coinsUsed = mid * (mid + 1) / 2;
                if (coinsUsed == n) return (int)mid;
                else if (n < coinsUsed) right = mid - 1;
                else left = mid + 1;
            }
            return (int)right;
        }
        ///442. Find All Duplicates in an Array
        ///Given an integer array nums of length n where all of nums in the range [1, n]
        ///and each integer appears once or twice, return an array of all the integers that appears twice.
        public IList<int> FindDuplicates(int[] nums)
        {
            var res=new List<int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach(var n in nums)
            {
                if(!dict.ContainsKey(n))dict.Add(n, 0);
                dict[n]++;
            }
            foreach (var k in dict.Keys)
                if (dict[k] == 2) res.Add(k);

            return res;
        }
        /// 443
        public int Compress(char[] chars)
        {
            if (chars.Length == 1) return chars.Length;
            List<char> result = new List<char>();
            char pre = chars[0];
            int occured = 1;
            char current;
            for (int i = 1; i < chars.Length; i++)
            {
                current = chars[i];
                if (current == pre)
                {
                    occured++;
                }
                else
                {
                    result.Add(pre);
                    if (occured > 1) { result.AddRange(occured.ToString().ToCharArray()); }

                    pre = current;
                    occured = 1;
                }

                if (i == chars.Length - 1)
                {
                    result.Add(pre);
                    if (occured > 1) { result.AddRange(occured.ToString().ToCharArray()); }
                }
            }

            Array.Copy(result.ToArray(), chars, result.Count);
            return result.Count;
        }

        public char GetSingleNumChar(int num)
        {
            return (char)(num + 0x30);
        }

        public char[] GetNumCharArray(int num)
        {
            return num.ToString().ToCharArray();
        }

        ///445. Add Two Numbers II
        ///You are given two non-empty linked lists representing two non-negative integers.
        ///The most significant digit comes first and each of their nodes contains a single digit.
        ///Add the two numbers and return the sum as a linked list.
        public ListNode AddTwoNumbers_445(ListNode l1, ListNode l2)
        {
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            while (l1 != null)
            {
                list1.Insert(0, l1.val);
                l1 = l1.next;
            }
            while (l2 != null)
            {
                list2.Insert(0, l2.val);
                l2 = l2.next;
            }
            int carry = 0;
            List<int> list = new List<int>();
            for (int i = 0; i < list1.Count || i<list2.Count ; i++)
            {
                int a = i < list1.Count ? list1[i] : 0;
                int b = i < list2.Count ? list2[i] : 0;
                int sum = a + b + carry;
                list.Insert(0,sum%10);
                carry = sum/10;
            }

            if (carry != 0) list.Insert(0, carry);
            ListNode res = new ListNode();
            var curr = res;
            for(int i=0; i<list.Count; i++)
            {
                curr.val = list[i];
                if (i != list.Count - 1)
                {
                    curr.next = new ListNode();
                    curr = curr.next;
                }
            }
            return res;
        }

        /// 446. Arithmetic Slices II - Subsequence, #DP
        ///at least 3 nums with same distance, eg. [1,2,3,4], [1,1,1]
        ///1  <= nums.length <= 1000, -2^31 <= nums[i] <= 2^31 - 1
        ///Subsequence may not continuous
        public int NumberOfArithmeticSlices(int[] nums)
        {
            int ans = 0;

            return ans;
        }

        ///447. Number of Boomerangs
        //Return the number of points (i, j, k) that the distance between i and j equal to i and k
        public int NumberOfBoomerangs(int[][] points)
        {
            int res = 0;
            int n = points.Length;
            Dictionary<int, int>[] mapArr=new Dictionary<int, int>[n];
            for (int i = 0; i < n; i++)
                mapArr[i] = new Dictionary<int, int>();
            for(int i = 0; i < n - 1; i++)
            {
                for(int j = i + 1; j < n; j++)
                {
                    int[] p1 = points[i];
                    int[] p2 = points[j];
                    int dist =( p1[0] - p2[0]) * (p1[0] - p2[0]) + (p1[1] - p2[1]) * (p1[1] - p2[1]);
                    var map1 = mapArr[i];
                    var map2 = mapArr[j];
                    if (!map1.ContainsKey(dist)) map1.Add(dist, 0);
                    map1[dist]++;
                    if (!map2.ContainsKey(dist)) map2.Add(dist, 0);
                    map2[dist]++;
                }
            }
            foreach(var map in mapArr)
            {
                foreach(var k in map.Keys)
                {
                    if (map[k] <= 1) continue;
                    res += map[k] * (map[k] - 1);
                }
            }
            return res;
        }

        ///448. Find All Numbers Disappeared in an Array
        ///Given an array nums of n integers where nums[i] is in the range [1, n],
        ///return an array of all the integers in the range [1, n] that do not appear in nums.
        public IList<int> FindDisappearedNumbers(int[] nums)
        {
            var res=new List<int>();
            HashSet<int> set = new HashSet<int>();
            foreach(var n in nums)
                set.Add(n);

            for (int i=1;i<= nums.Length; i++)
                if(!set.Contains(i))res.Add(i);

            return res;
        }

        ///449. Serialize and Deserialize BST, see Codec_449

    }
}