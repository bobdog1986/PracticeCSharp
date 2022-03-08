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

        ///415. Add Strings
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

        ///417. Pacific Atlantic Water Flow. #Graph, #BFS
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


        /// 421. Maximum XOR of Two Numbers in an Array, #Trie
        ///return the maximum result of nums[i] XOR nums[j], where 0 <= i <= j < n.
        ///1 <= nums.length <= 2 * 10^5, 0 <= nums[i] <= 2^31 - 1
        public int FindMaximumXOR1(int[] nums)
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

        public int FindMaximumXOR(int[] nums)
        {
            Trie421 trie = new Trie421();
            trie.insert(nums);

            int max = 0;

            foreach (int num in nums)
            {
                var curr = trie.root;
                int currSum = 0;
                for (int i = 31; i >= 0; i--)
                {
                    int requiredBit = 1 - ((num >> i) & 1); // if A[i] is 0, we need 1 and if A[i] is 1, we need 0. Thus, 1 - A[i]
                    if (curr.children.ContainsKey(requiredBit))
                    {
                        currSum |= (1 << i); // set ith bit of curr result
                        curr = curr.children[requiredBit];
                    }
                    else
                    {
                        curr = curr.children[1 - requiredBit];
                    }
                }
                max = Math.Max(max, currSum); // get max number
            }
            return max;
        }

        public class Node421
        {
            public Dictionary<int, Node421> children;
            public Node421()
            {
                this.children = new Dictionary<int, Node421>();
            }
        }

        public class Trie421
        {
            public Node421 root;

            public Trie421()
            {
                this.root = new Node421();
            }

            public void insert(int[] nums)
            {
                foreach (int num in nums)
                {
                    Node421 curr = this.root;
                    for (int i = 31; i >= 0; i--)
                    {
                        int currBit = (num >> i) & 1;
                        if (!curr.children.ContainsKey(currBit))
                            curr.children.Add(currBit, new Node421());
                        curr = curr.children[currBit];
                    }
                }
            }
        }


        ///424. Longest Repeating Character Replacement, ### Siding Window
        ///You can choose any character of the string and change it to any other uppercase English character at most k times.
        ///Return the length of the longest substring containing the same letter you can get after performing the above operations.
        ///1 <= s.length <= 10^5, 0 <= k <= s.length, s consists of only uppercase English letters.
        public int CharacterReplacement(string s, int k)
        {
            if (k == s.Length)
                return k;

            int ans = k;
            int count = 0;
            int[] arr = new int[26];
            int left = 0;
            int max = 0;
            for (int i = 0; i < s.Length; i++)
            {
                arr[s[i] - 'A']++;
                count++;
                max = arr.Max();
                if (max + k >= count)
                {
                    ans = Math.Max(ans, count);
                }
                else
                {
                    arr[s[left] - 'A']--;
                    left++;
                    count--;
                }
            }
            return ans;
        }

        ///429. N-ary Tree Level Order Traversal
        ///Given an n-ary tree, return the level order traversal of its nodes' values.
        public IList<IList<int>> LevelOrder(Node root)
        {
            var ans=new List<IList<int>>();
            var nodes=new List<Node>() { root};
            while(nodes.Count > 0)
            {
                var next = new List<Node>();
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


        ///434. Number of Segments in a String
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

        /// 438. Find All Anagrams in a string
        /// Input: s = "cbaebabacd", p = "abc", Output: [0,6]
        /// The substring with start index = 0 is "cba", which is an anagram of "abc".
        /// The substring with start index = 6 is "bac", which is an anagram of "abc".
        /// should use sliding window
        public List<int> FindAnagrams(string s, string p)
        {
            var ans = new List<int>();
            if (p.Length > s.Length)
                return ans;
            int left = 0, right = 0;
            int[] arr = new int[26];
            int[] target = new int[26];

            while (right < p.Length)
            {
                arr[s[right] - 'a']++;
                target[p[right] - 'a']++;
                right++;
            }
            right--;

            while (right < s.Length)
            {
                bool isEqual = true;
                for (int i = 0; i < 26; i++)
                {
                    if (arr[i] != target[i])
                    {
                        isEqual = false;
                        break;
                    }
                }
                if (isEqual)
                    ans.Add(left);

                right++;
                if (right < s.Length)
                    arr[s[right] - 'a']++;

                arr[s[left] - 'a']--;
                left++;
            }
            return ans;
        }

        public IList<int> FindAnagrams_My(string s, string p)
        {
            List<int> ans = new List<int>();

            var arr1 = s.ToArray();
            var arr2 = p.ToArray();

            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (var i in arr2)
            {
                if (dict.ContainsKey(i))
                {
                    dict[i]++;
                }
                else
                {
                    dict.Add(i, 1);
                }
            }

            Dictionary<char, List<int>> match = new Dictionary<char, List<int>>();

            int len = 0;
            int start = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr2.Contains(arr1[i]))
                {
                    if (len == 0)
                        start = i;

                    if (match.ContainsKey(arr1[i]))
                    {
                        if (match[arr1[i]].Count < dict[arr1[i]])
                        {
                            match[arr1[i]].Add(i);
                            len++;
                        }
                        else
                        {
                            int j = match[arr1[i]][0];
                            foreach (var pair in match)
                            {
                                int k = pair.Value.RemoveAll(x => x <= j);
                                len = len - k;
                                start += k;
                            }

                            match[arr1[i]].Add(i);
                            len++;
                        }
                    }
                    else
                    {
                        match.Add(arr1[i], new List<int>() { i });
                        len++;
                    }

                    if (len == arr2.Length)
                    {
                        ans.Add(start);
                        foreach (var m in match)
                        {
                            if (m.Value.Contains(start))
                            {
                                m.Value.Remove(start);
                                break;
                            }
                        }
                        start++;
                        len--;
                    }
                }
                else
                {
                    match.Clear();
                    len = 0;
                }
            }

            return ans;
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

        ///446. Arithmetic Slices II - Subsequence, #DP
        ///at least 3 nums with same distance, eg. [1,2,3,4], [1,1,1]
        ///1  <= nums.length <= 1000, -2^31 <= nums[i] <= 2^31 - 1
        ///Subsequence may not continuous
        public int NumberOfArithmeticSlices(int[] nums)
        {
            int ans = 0;

            return ans;
        }
    }
}