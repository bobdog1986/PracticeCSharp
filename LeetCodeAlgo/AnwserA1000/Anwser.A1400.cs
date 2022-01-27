﻿using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1345. Jump Game IV
        ///Given an array of integers arr, you are initially positioned at the first index of the array.
        ///In one step you can jump from index i to index:
        ///i + 1 where: i + 1 < arr.length.
        ///i - 1 where: i - 1 >= 0.
        ///j where: arr[i] == arr[j] and i != j.
        ///Return the minimum number of steps to reach the last index of the array.
        ///Notice that you can not jump outside of the array at any time.

        public int MinJumps(int[] arr)
        {
            if (arr == null || arr.Length <= 1)
                return 0;

            if (arr.Length == 2 || arr[0] == arr[arr.Length - 1])
                return 1;

            if (arr[0] == arr[arr.Length - 2] || arr[1] == arr[arr.Length - 1])
                return 2;

            //len>=3

            Dictionary<int, List<int>> valueIndexDict = new Dictionary<int, List<int>>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (valueIndexDict.ContainsKey(arr[i]))
                {
                    valueIndexDict[arr[i]].Add(i);
                }
                else
                {
                    valueIndexDict.Add(arr[i], new List<int>() { i });
                }
            }

            if (valueIndexDict.Count == arr.Length)
            {
                return arr.Length - 1;
            }

            int specIndex = arr.Length - 1;

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (valueIndexDict[arr[i]].Count > 1)
                {
                    specIndex = i;
                    break;
                }
            }

            var stepToSpecIndex = arr.Length - 1 - specIndex;

            if (arr[0] == arr[specIndex])
            {
                return arr.Length - 1 - specIndex + 1;
            }

            if (arr[1] == arr[specIndex])
            {
                return arr.Length - 1 - specIndex + 1 + 1;
            }

            List<int> visitIndexList = new List<int>() { 0 };

            foreach (var pair in valueIndexDict)
            {
                if (pair.Value.Count > 1)
                {
                    var list = new List<int>(pair.Value);

                    for (int i = 1; i < list.Count - 1; i++)
                    {
                        if (arr[list[i]] == arr[list[i] - 1] && arr[list[i]] == arr[list[i] + 1])
                        {
                            visitIndexList.Add(list[i]);
                            pair.Value.Remove(list[i]);
                        }
                    }
                }
            }

            //List<int> visitValueList = new List<int>() {  };

            List<List<int>> allPath = new List<List<int>>
            {
                new List<int>() { 0 }
            };

            while (true)
            {
                var list = new List<int>();

                var lastList = allPath.Last();

                foreach (var i in lastList)
                {
                    if (specIndex < arr.Length - 1)
                    {
                        if (arr[i] == arr[specIndex])
                        {
                            if (i != specIndex)
                            {
                                stepToSpecIndex++;
                            }
                            return allPath.Count - 1 + stepToSpecIndex;
                        }

                        if (i < arr.Length - 1 && arr[i + 1] == arr[specIndex])
                        {
                            stepToSpecIndex++;
                            if (i + 1 != specIndex)
                            {
                                stepToSpecIndex++;
                            }
                            return allPath.Count - 1 + stepToSpecIndex;
                        }

                        if (i > 0 && arr[i - 1] == arr[specIndex])
                        {
                            stepToSpecIndex++;
                            if (i - 1 != specIndex)
                                stepToSpecIndex++;

                            return allPath.Count - 1 + stepToSpecIndex;
                        }
                    }

                    foreach (var j in valueIndexDict[arr[i]])
                    {
                        if (!visitIndexList.Contains(j) && i != j)
                        {
                            list.Add(j);

                            visitIndexList.Add(j);
                            //visitValueList.Add(arr[j]);
                        }
                    }

                    if (i > 0)
                    {
                        if (!visitIndexList.Contains(i - 1))
                        {
                            list.Add(i - 1);

                            visitIndexList.Add(i - 1);
                            //visitValueList.Add(arr[i - 1]);
                        }
                    }

                    if (i < arr.Length - 1)
                    {
                        if (!visitIndexList.Contains(i + 1))
                        {
                            list.Add(i + 1);

                            visitIndexList.Add(i + 1);
                            //visitValueList.Add(arr[i + 1]);
                        }
                    }
                }

                allPath.Add(list);

                if (list.Contains(arr.Length - 1))
                    break;
            }

            return allPath.Count - 1;
        }

        ///

        ///
    }
}