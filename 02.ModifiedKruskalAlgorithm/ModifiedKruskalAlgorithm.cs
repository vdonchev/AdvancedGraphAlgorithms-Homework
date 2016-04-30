namespace _02.ModifiedKruskalAlgorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ModifiedKruskalAlgorithm
    {
        public static void Main()
        {
            var nodesCount = InputToInteger(Console.ReadLine());
            var edgesCount = InputToInteger(Console.ReadLine());
            var allEdges = new List<Edge>();

            for (int e = 0; e < edgesCount; e++)
            {
                var connectionDetails = Console.ReadLine().Split(' ');
                var connectionValues = connectionDetails.Select(int.Parse).ToArray();
                var edge = new Edge(connectionValues[0], connectionValues[1], connectionValues[2]);
                allEdges.Add(edge);
            }

            var minSpanningTree = Kruskal(nodesCount, allEdges);

            Console.WriteLine("Minimum spanning forest weight: " + 
                minSpanningTree.Sum(e => e.Weight));

            foreach (var edge in minSpanningTree)
            {
                Console.WriteLine(edge);
            }
        }

        static List<Edge> Kruskal(int nodesCount, List<Edge> allEdges)
        {
            allEdges.Sort();
            
            var parent = new int[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                parent[i] = i;
            }
            
            var spanningTree = new List<Edge>();
            foreach (var edge in allEdges)
            {
                int rootStartNode = FindRoot(edge.StartNode, parent);
                int rootEndNode = FindRoot(edge.EndNode, parent);
                if (rootStartNode != rootEndNode)
                {
                    spanningTree.Add(edge);
                    parent[rootStartNode] = rootEndNode;
                }
            }

            return spanningTree;
        }

        static int FindRoot(int node, int[] parent)
        {
            int root = node;
            while (parent[root] != root)
            {
                root = parent[root];
            }
            
            while (node != root)
            {
                var oldParent = parent[node];
                parent[node] = root;
                node = oldParent;
            }

            return root;
        }

        private static int InputToInteger(string input)
        {
            var res = int.Parse(input.Split(' ')[1]);

            return res;
        }
    }
}
