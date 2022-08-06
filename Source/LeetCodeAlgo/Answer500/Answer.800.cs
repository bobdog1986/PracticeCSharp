using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///802. Find Eventual Safe States, #DFS, #Graph
        public IList<int> EventualSafeNodes(int[][] graph)
        {
            HashSet<int> map = new HashSet<int>();
            int[] dp = new int[graph.Length];
            bool[] visit = new bool[graph.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                EventualSafeNodes_dfs(graph, i, visit, dp, map);
            }
            return map.OrderBy(x => x).ToList();
        }

        public int EventualSafeNodes_dfs(int[][] graph, int i, bool[] visit, int[] dp, HashSet<int> map)
        {
            if (visit[i])
            {
                return dp[i];
            }
            visit[i] = true;
            int ans = 1;
            foreach (var j in graph[i])
            {
                if (visit[j]) ans &= dp[j];
                else ans &= EventualSafeNodes_dfs(graph, j, visit, dp, map);
            }

            if (ans == 1)
            {
                if (!map.Contains(i)) map.Add(i);
            }
            dp[i] = ans;
            return ans;
        }

        ///804. Unique Morse Code Words
        public int UniqueMorseRepresentations(string[] words)
        {
            string[] morseDict = new string[] { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..",
                ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-",
                ".--", "-..-", "-.--", "--.." };
            var set = new HashSet<string>();
            foreach (var word in words)
            {
                var morseCode = string.Join("", word.Select(x => morseDict[x - 'a']));
                set.Add(morseCode);
            }
            return set.Count;
        }

        ///805. Split Array With Same Average, #DP
        //split to two non-empty subset that the double average of each is same
        //1 <= nums.length <= 30,0 <= nums[i] <= 10^4
        public bool SplitArraySameAverage(int[] nums)
        {
            Array.Sort(nums);//sort ascending
            return SplitArraySameAverage_Memo(nums, 0, 0, 0, 0, nums.Sum(), new Dictionary<int, HashSet<int>>());
        }

        private bool SplitArraySameAverage_Memo(int[] nums, int i, int count, int sum1, int sum2, int total, Dictionary<int, HashSet<int>> visit)
        {
            //sum1 is sum of part1, sum2 is sum of part2, and count is part1's elements count, i is current index
            if (i == nums.Length)
            {
                if (count == 0 || count == nums.Length) return false;//part1 or part2 is empty
                double a = 1.0 * sum1 / count;//average of part1
                double b = 1.0 * sum2 / (nums.Length - count);//average of part2
                return a == b;
            }
            else
            {
                // visit is a memoization , store {index*1000 +countOfPart1, visitedSum1 set }
                int k = i * 1000 + count;
                if (visit.ContainsKey(k) && visit[k].Contains(sum1))
                    return false;//this will help us skip duplicate visit
                if (!visit.ContainsKey(k))
                    visit.Add(k, new HashSet<int>());
                visit[k].Add(sum1);
                if (count > 0)
                {
                    //count>0, so part1 is non-empty, we try to assign all left elements to part2
                    double a = 1.0 * sum1 / count;
                    double b = 1.0 * (total - sum1) / (nums.Length - count);
                    if (a == b) return true;//it works ,so return true
                    else if (a > b) return false;//nums sort in ascending, so this operation must increase b, but it still not enough, return false
                }
                if (count < i)
                {
                    //count<i, so part1 is non-empty, we try to assign all left elements to part1
                    double a = 1.0 * (total - sum2) / (nums.Length - (i - count));
                    double b = 1.0 * sum2 / (i - count);
                    if (a == b) return true;//it works ,so return true
                    else if (a < b) return false;//nums sort in ascending, so this operation must increase a, but it still not enough, return false
                }
                //dfs, try assign current element to part2
                if (SplitArraySameAverage_Memo(nums, i + 1, count, sum1, sum2 + nums[i], total, visit))
                    return true;
                //dfs, try assign current element to part1
                if (SplitArraySameAverage_Memo(nums, i + 1, count + 1, sum1 + nums[i], sum2, total, visit))
                    return true;
                return false;
            }
        }

        ///806. Number of Lines To Write String
        public int[] NumberOfLines(int[] widths, string s)
        {
            int lines = 0;
            int curr = 0;
            foreach (var c in s)
            {
                if (curr + widths[c - 'a'] > 100)
                {
                    lines++;
                    curr = widths[c - 'a'];
                }
                else
                {
                    curr += widths[c - 'a'];
                }
            }
            lines++;
            return new int[] { lines, curr };
        }

        /// 807. Max Increase to Keep City Skyline
        public int MaxIncreaseKeepingSkyline(int[][] grid)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            int[] rowMax = grid.Select(x => x.Max()).ToArray();
            int[] colMax = new int[colLen];
            for (int c = 0; c < colLen; c++)
            {
                int max = int.MinValue;
                for (int r = 0; r < rowLen; r++)
                {
                    max = Math.Max(max, grid[r][c]);
                }
                colMax[c] = max;
            }

            int res = 0;
            for (int r = 0; r < rowLen; r++)
            {
                for (int c = 0; c < colLen; c++)
                {
                    int max = Math.Min(rowMax[r], colMax[c]);
                    res += max > grid[r][c] ? max - grid[r][c] : 0;
                }
            }
            return res;
        }

        ///809. Expressive Words
        //groups of adjacent letters that are all the same
        //choose a group of characters c, and add some c to the group so that size of group is three or more.
        public int ExpressiveWords(string s, string[] words)
        {
            int res = 0;
            var list = ExpressiveWords_Parse(s);
            foreach (var word in words)
            {
                var curr = ExpressiveWords_Parse(word);
                if (list.Count != curr.Count) continue;
                bool match = true;
                for (int i = 0; i < list.Count && match; i++)
                {
                    if (list[i][0] != curr[i][0])
                        match = false;
                    else
                    {
                        if (list[i][1] == curr[i][1]) continue;
                        else if (list[i][1] < 3 || list[i][1] < curr[i][1])
                            match = false;
                    }
                }
                if (match)
                    res++;
            }
            return res;
        }

        private List<int[]> ExpressiveWords_Parse(string s)
        {
            List<int[]> list = new List<int[]>();
            char prev = s[0];
            int count = 1;
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == prev) count++;
                else
                {
                    list.Add(new int[] { prev, count });

                    prev = s[i];
                    count = 1;
                }
            }
            list.Add(new int[] { prev, count });
            return list;
        }

        ///811. Subdomain Visit Count
        public IList<string> SubdomainVisits(string[] cpdomains)
        {
            var res = new List<string>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var cpdomain in cpdomains)
            {
                var arr1 = cpdomain.Split(' ');
                int time = int.Parse(arr1[0]);
                string str = arr1[1];
                var domains = str.Split('.');
                string curr = "";
                for (int i = domains.Length - 1; i >= 0; i--)
                {
                    curr = domains[i] + curr;
                    if (dict.ContainsKey(curr)) dict[curr] += time;
                    else dict.Add(curr, time);
                    curr = "." + curr;
                }
            }
            foreach (var key in dict.Keys)
            {
                res.Add($"{dict[key]} {key}");
            }
            return res;
        }

        ///812. Largest Triangle Area
        //return the area of the largest triangle that can be formed by any three different points.
        public double LargestTriangleArea(int[][] points)
        {
            double res = 0;
            int n = points.Length;
            for (int i = 0; i < n - 2; i++)
                for (int j = 0; j < n - 1; j++)
                    for (int k = 0; k < n; k++)
                        res = Math.Max(res, 0.5 * Math.Abs(
                            points[i][0] * points[j][1] +
                            points[j][0] * points[k][1] +
                            points[k][0] * points[i][1] -
                            points[j][0] * points[i][1] -
                            points[k][0] * points[j][1] -
                            points[i][0] * points[k][1]));
            return res;
        }

        /// 814. Binary Tree Pruning, #BTree
        ///return the same tree where every subtree (of the given tree) not containing a 1 has been removed.
        ///A subtree of a node node is node plus every node that is a descendant of node.
        public TreeNode PruneTree(TreeNode root)
        {
            if (!PruneTree_AnyChildEqualToOne(root)) return null;
            return root;
        }

        private bool PruneTree_AnyChildEqualToOne(TreeNode root)
        {
            if (root == null) return false;
            var leftHasOne = PruneTree_AnyChildEqualToOne(root.left);
            var rightHasOne = PruneTree_AnyChildEqualToOne(root.right);
            if (!leftHasOne) root.left = null;
            if (!rightHasOne) root.right = null;

            if (root.val == 1 || leftHasOne || rightHasOne) return true;
            else return false;
        }

        ///815. Bus Routes, #BFS
        //routes[0] = [1, 5, 7], 0th bus travels in the sequence 1 -> 5 -> 7 -> 1 -> 5 -> 7 -> ... forever.
        //Return the least number of buses you must take to travel from source to target. Return -1 if not possible.
        //1 <= routes.length <= 500. 1 <= routes[i].length <= 10^5
        public int NumBusesToDestination(int[][] routes, int source, int target)
        {
            int n = 0;//store max of bus stopId
            var dict = new Dictionary<int, HashSet<int>>();//store {stopId,busIdSet}
            for (int i = 0; i < routes.Length; i++)
            {
                foreach (var j in routes[i])
                {
                    n = Math.Max(n, j);
                    if (!dict.ContainsKey(j))
                        dict.Add(j, new HashSet<int>());
                    dict[j].Add(i);
                }
            }
            if (!dict.ContainsKey(target) || !dict.ContainsKey(source))
                return -1;
            Queue<int> q = new Queue<int>();
            q.Enqueue(source);
            int level = 0;
            HashSet<int> visit = new HashSet<int>();
            visit.Add(source);
            while (q.Count > 0)
            {
                int size = q.Count;
                while (size-- > 0)
                {
                    var top = q.Dequeue();
                    if (top == target) return level;
                    foreach (var busId in dict[top])
                    {
                        foreach (var stopId in routes[busId])
                        {
                            if (visit.Contains(stopId)) continue;
                            visit.Add(stopId);
                            q.Enqueue(stopId);
                        }
                    }
                }
                level++;
            }
            return -1;
        }

        ///819. Most Common Word
        public string MostCommonWord(string paragraph, string[] banned)
        {
            var words = paragraph.Split(" !?',;.".ToCharArray()).Select(x => x.ToLower()).ToList();
            var set = new HashSet<string>(banned.Select(x => x.ToLower()));
            var dict = new Dictionary<string, int>();
            foreach (var w in words)
            {
                if (string.IsNullOrEmpty(w)) continue;
                if (set.Contains(w)) continue;
                if (dict.ContainsKey(w)) dict[w]++;
                else dict.Add(w, 1);
            }

            var keys = dict.Keys.OrderBy(x => -dict[x]).ToList();
            return keys[0];
        }

        ///820. Short Encoding of Words, #Trie
        //from tail to head,if not match, add '#'. return the total length
        public int MinimumLengthEncoding(string[] words)
        {
            var root = new TrieItem();
            words = words.ToHashSet().ToArray();
            var set = new HashSet<string>();
            foreach (var word in words)
            {
                var curr = root;
                for (int i = word.Length - 1; i >= 0; --i)
                {
                    if (!string.IsNullOrEmpty(curr.word))
                        set.Remove(curr.word);

                    if (!curr.dict.ContainsKey(word[i]))
                        curr.dict.Add(word[i], new TrieItem());
                    curr = curr.dict[word[i]];
                }
                if (curr.dict.Count == 0)
                {
                    curr.word = word;
                    set.Add(word);
                }
            }
            return set.Sum(x => x.Length + 1);
        }

        /// 821. Shortest Distance to a Character
        public int[] ShortestToChar(string s, char c)
        {
            int n = s.Length;
            int[] res = new int[n];
            int[] head = new int[n];
            int[] tail = new int[n];
            int left = -1;
            int right = -1;
            for (int i = 0; i < n; i++)
            {
                if (s[i] == c) left = i;
                head[i] = left;

                if (s[n - 1 - i] == c) right = n - 1 - i;
                tail[n - 1 - i] = right;
            }

            for (int i = 0; i < n; i++)
            {
                int min = int.MaxValue;
                if (head[i] != -1) min = Math.Min(min, i - head[i]);
                if (tail[i] != -1) min = Math.Min(min, tail[i] - i);
                res[i] = min;
            }
            return res;
        }

        ///824. Goat Latin
        public string ToGoatLatin(string sentence)
        {
            var set = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
            var curr = "a";
            var words = sentence.Split(' ').Select(x =>
            {
                var str = x;
                if (set.Contains(x[0]))
                {
                    str += "ma";
                }
                else
                {
                    str = $"{x.Substring(1, x.Length - 1)}{x[0]}ma";
                }
                str += curr;
                curr += "a";
                return str;
            });
            return string.Join(" ", words);
        }

        ///825. Friends Of Appropriate Ages, #Binary Search, #HashMap
        //only request to friend that : age[y] <= 0.5 * age[x] + 7 && age[y] > age[x]
        public int NumFriendRequests(int[] ages)
        {
            Array.Sort(ages);
            int res = 0;
            int n = ages.Length;
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                double young = 0.5 * ages[i] + 7;

                if (i > 0 && ages[i - 1] > young)
                {
                    int left = 0;
                    int right = i - 1;
                    while (left < right)
                    {
                        int mid = (left + right) / 2;
                        if (ages[mid] > young)
                        {
                            right = mid;
                        }
                        else
                        {
                            left = mid + 1;
                        }
                    }
                    count += i - 1 - left + 1;
                }

                if (i < n - 1 && ages[i + 1] <= ages[i] && ages[i + 1] > young)
                {
                    int left = i + 1;
                    int right = n - 1;
                    while (left < right)
                    {
                        int mid = (left + right + 1) / 2;
                        if (ages[mid] > ages[i])
                        {
                            right = mid - 1;
                        }
                        else
                        {
                            left = mid;
                        }
                    }
                    count += left - (i + 1) + 1;
                }
                res += count;
            }
            return res;
        }

        public int NumFriendRequests_HashMap(int[] ages)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var i in ages)
            {
                if (dict.ContainsKey(i)) dict[i]++;
                else dict.Add(i, 1);
            }

            int res = 0;
            foreach (var i in dict.Keys)
            {
                foreach (var j in dict.Keys)
                {
                    if (j > 0.5 * i + 7 && j <= i)
                    {
                        if (i == j) res += dict[i] * (dict[j] - 1);
                        else res += dict[i] * dict[j];
                    }
                }
            }
            return res;
        }

        ///826. Most Profit Assigning Work, #Binary Search
        //worker[i]>=difficulty[j], then can take profit[j]
        public int MaxProfitAssignment(int[] difficulty, int[] profit, int[] worker)
        {
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < difficulty.Length; i++)
            {
                if (!dict.ContainsKey(difficulty[i])) dict.Add(difficulty[i], 0);
                dict[difficulty[i]] = Math.Max(dict[difficulty[i]], profit[i]);
            }

            var keys = dict.Keys.OrderBy(x => x).ToList();
            int[] maxProfit = new int[keys.Count];
            int max = 0;
            for (int i = 0; i < keys.Count; i++)
            {
                max = Math.Max(max, dict[keys[i]]);
                maxProfit[i] = max;
            }

            int res = 0;
            foreach (var w in worker)
            {
                if (w < keys[0]) continue;
                if (w >= keys.Last())
                {
                    res += maxProfit.Last();
                }
                else
                {
                    int left = 0;
                    int right = keys.Count - 1;
                    while (left < right)
                    {
                        var mid = (left + right + 1) / 2;
                        if (w >= keys[mid])
                        {
                            left = mid;
                        }
                        else
                        {
                            right = mid - 1;
                        }
                    }
                    res += maxProfit[left];
                }
            }

            return res;
        }

        /// 830. Positions of Large Groups
        ///A group is considered large if it has 3 or more characters.
        ///Return the intervals of every large group sorted in increasing order by start index.
        public IList<IList<int>> LargeGroupPositions(string s)
        {
            var ans = new List<IList<int>>();
            int count = 1;
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == s[i - 1])
                {
                    count++;
                }
                else
                {
                    if (count >= 3)
                    {
                        ans.Add(new List<int>() { i - count, i - 1 });
                    }
                    count = 1;
                }
            }
            if (count >= 3)
            {
                ans.Add(new List<int>() { s.Length - count, s.Length - 1 });
            }
            return ans;
        }

        ///831. Masking Personal Information
        public string MaskPII(string s)
        {
            string res = "";
            if (s.Contains('@'))
            {
                var arr = s.Split('@');
                string name = arr[0].ToLower();
                string domain = arr[1].ToLower();
                res = $"{name[0]}*****{name.Last()}" + "@" + domain;
            }
            else
            {
                var digits = s.Where(c => char.IsDigit(c)).ToList();
                int n = digits.Count;
                res = "***-***-" + new string(digits.Skip(n - 4).Take(4).ToArray());
                if (n > 10)
                {
                    res = "+" + new string(Enumerable.Repeat('*', digits.Count - 10).ToArray()) + "-" + res;
                }
            }
            return res;
        }

        ///832. Flipping an Image
        public int[][] FlipAndInvertImage(int[][] image)
        {
            int m = image.Length;
            int n = image[0].Length;
            int[][] res = new int[m][];
            for (int i = 0; i < m; i++)
            {
                res[i] = new int[n];
                for (int j = 0; j < n; j++)
                    res[i][j] = image[i][n - 1 - j] ^ 1;
            }
            return res;
        }

        ///836. Rectangle Overlap
        //[x1, y1, x2, y2],  (x1, y1) bottom-left corner and (x2, y2) top-right corner.
        public bool IsRectangleOverlap(int[] rec1, int[] rec2)
        {
            if (rec1[0] > rec2[0])
            {
                return IsRectangleOverlap(rec2, rec1);
            }
            return rec2[0] < rec1[2] && rec2[1] < rec1[3] && rec2[3] > rec1[1];
        }

        /// 841. Keys and Rooms, #BFS
        //all the rooms are locked except room[0], rooms[i] is the set of keys that you can obtain if visited room i,
        //return true if you can visit all the rooms, or false otherwise.
        public bool CanVisitAllRooms(IList<IList<int>> rooms)
        {
            int n = rooms.Count;
            Queue<int> q = new Queue<int>();
            q.Enqueue(0);
            HashSet<int> visit = new HashSet<int>() { 0 };
            while (q.Count > 0)
            {
                int size = q.Count;
                while (size-- > 0)
                {
                    var top = q.Dequeue();
                    foreach (var i in rooms[top])
                    {
                        if (!visit.Contains(i))
                        {
                            visit.Add(i);
                            q.Enqueue(i);
                        }
                    }
                }
            }
            return visit.Count == n;
        }

        /// 844. Backspace String Compare
        ///Given two strings s and t, return true if they are equal when both are typed into empty text editors.
        ///'#' means a backspace character.Note that after backspacing an empty text, the text will continue empty.
        public bool BackspaceCompare(string s, string t)
        {
            var str1 = BackspaceCompare_Get(s);
            var str2 = BackspaceCompare_Get(t);
            return str1 == str2;
        }

        private string BackspaceCompare_Get(string s)
        {
            char[] arr = new char[s.Length];
            int i = 0;
            for (int j = 0; j < s.Length; j++)
            {
                if (s[j] == '#')
                {
                    if (i > 0)
                        i--;
                }
                else arr[i++] = s[j];
            }
            return new string(arr.Take(i).ToArray());
        }

        ///846. Hand of Straights
        //each group is of size groupSize, and consists of groupSize consecutive cards.
        //hand[i] is the value written on the ith card and an integer groupSize,
        //return true if she can rearrange the cards, or false otherwise.
        public bool IsNStraightHand(int[] hand, int groupSize)
        {
            int n = hand.Length;
            if (n % groupSize != 0) return false;
            Array.Sort(hand);
            int count = n / groupSize;
            int[][] dp = new int[count][];
            for (int i = 0; i < count; i++)
                dp[i] = new int[2];

            for (int i = 0; i < n; i++)
            {
                bool append = false;
                for (int j = 0; j < count; j++)
                {
                    if (dp[j][0] == groupSize) continue;
                    else
                    {
                        if (dp[j][0] == 0)
                        {
                            dp[j][0]++;
                            dp[j][1] = hand[i];
                            append = true;
                            break;
                        }
                        else
                        {
                            if (hand[i] > dp[j][1] + 1) return false;
                            else if (hand[i] == dp[j][1] + 1)
                            {
                                dp[j][0]++;
                                dp[j][1] = hand[i];
                                append = true;
                                break;
                            }
                        }
                    }
                }
                if (!append) return false;
            }
            return true;
        }

        ///847. Shortest Path Visiting All Nodes, #Graph, #BFS
        ///You have an undirected, connected graph of n nodes labeled from 0 to n - 1.
        ///You are given an array graph where graph[i] is a list of all the nodes connected with node i by an edge.
        ///Return the length of the shortest path that visits every node.
        ///You may start and stop at any node, you may revisit nodes multiple times, and you may reuse edges.
        public int ShortestPathLength(int[][] graph)
        {
            int n = graph.Length;
            //n<=12
            //[bitMaskOfVisited, currNode , nodeCountOfVisited]
            Queue<int[]> queue = new Queue<int[]>();
            HashSet<int> set = new HashSet<int>();

            for (int i = 0; i < n; i++)
            {
                int tmp = (1 << i);
                set.Add((tmp << 4) + i);
                queue.Enqueue(new int[] { tmp, i, 1 });
            }

            while (queue.Count > 0)
            {
                var curr = queue.Dequeue();
                if (curr[0] == (1 << n) - 1)
                {
                    return curr[2] - 1;//step = count -1
                }
                else
                {
                    foreach (int neighbor in graph[curr[1]])
                    {
                        int bitMask = curr[0] | (1 << neighbor);
                        if (!set.Add((bitMask << 4) + neighbor)) continue;
                        queue.Enqueue(new int[] { bitMask, neighbor, curr[2] + 1 });
                    }
                }
            }
            return -1;
        }

        /// 848. Shifting Letters, #Prefix Sum
        ///Now for each shifts[i] = x, we want to shift the first i + 1 letters of s, x times.
        ///Return the final string after all such shifts to s are applied.
        public string ShiftingLetters(string s, int[] shifts)
        {
            //cache the total shifts of s[i]
            long[] arr = new long[s.Length];
            //cache the sum from right to left, reduce time complexity from O(n^2) to O(n)
            long sum = 0;
            for (int i = shifts.Length - 1; i >= 0; i--)
            {
                sum += shifts[i];
                arr[i] = sum;
            }
            var carr = s.ToCharArray();
            for (int i = 0; i < carr.Length; i++)
            {
                var val = carr[i] + arr[i] % 26;//mod of 26
                if (val > 'z') val -= 26;
                carr[i] = (char)(val);
            }
            return new string(carr);
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
    }
}