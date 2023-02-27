using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeProblems
{
    public class NeighborsNode
    {
        public class Node
        {
            public int val;
            public IList<Node> neighbors;

            public Node()
            {
                val = 0;
                neighbors = new List<Node>();
            }

            public Node(int _val)
            {
                val = _val;
                neighbors = new List<Node>();
            }

            public Node(int _val, List<Node> _neighbors)
            {
                val = _val;
                neighbors = _neighbors;
            }
        }

        /// 133. Clone Graph, #Graph, #DFS
        //Given a reference of a node in a connected undirected graph.
        //Return a deep copy(clone) of the graph.
        //Each node in the graph contains a value(int) and a list(List[Node]) of its neighbors.
        public Node CloneGraph(Node node)
        {
            if (node == null) return null;
            Node res = null;
            Dictionary<Node, int> dict = new Dictionary<Node, int>();
            List<Node> list = new List<Node>();
            res = new Node(node.val);
            dict.Add(node, list.Count);
            list.Add(res);
            res.neighbors = CloneGraph_DFS(node.neighbors, dict, list);
            return res;
        }

        private IList<Node> CloneGraph_DFS(IList<Node> neighbors, IDictionary<Node, int> dict, IList<Node> list)
        {
            if (neighbors == null) return null;
            if (neighbors.Count == 0) return new List<Node>();
            var res = new List<Node>();
            foreach (var node in neighbors)
            {
                if (dict.ContainsKey(node))
                {
                    res.Add(list[dict[node]]);
                }
                else
                {
                    dict.Add(node, list.Count);
                    var clone = new Node(node.val);
                    list.Add(clone);
                    res.Add(clone);
                    clone.neighbors = CloneGraph_DFS(node.neighbors, dict, list);
                }
            }
            return res;
        }

    }
}
