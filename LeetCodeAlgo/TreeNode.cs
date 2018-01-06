namespace LeetCodeAlgo
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int x)
        {
            val = x;
        }
    }

    public class Interval
    {
        public int start;
        public int end;

        public Interval()
        {
            start = 0; end = 0;
        }

        public Interval(int s, int e)
        {
            start = s; end = e;
        }
    }
}