namespace _05.ShortestPathsNegativeEdges
{
    using System;
    using System.Collections.Generic;

    public static class ShortestPathsNegativeEdges
    {
        private static List<int> path; 

        static void Main()
        {
            int verticeCount = 10;

            var edges = new List<Edge>
            {
                new Edge(0, 3, -4),
                new Edge(0, 6, 10),
                new Edge(0, 8, 12),
                new Edge(1, 9, -50),
                new Edge(2, 5, 12),
                new Edge(2, 7, -7),
                new Edge(3, 2, -9),
                new Edge(3, 5, 15),
                new Edge(3, 6, 6),
                new Edge(3, 8, -3),
                new Edge(4, 1, 20),
                new Edge(4, 3, -5),
                new Edge(5, 1, -6),
                new Edge(5, 4, 11),
                new Edge(5, 7, -20),
                new Edge(6, 4, 17),
                new Edge(7, 1, 26),
                new Edge(7, 9, 3),
                new Edge(8, 2, 15),
            };

            var startNode = 0;
            var destination = 9;
            
            try
            {
                var distance = FindShortestPathBellmanFord(startNode, destination, verticeCount, edges);
                Console.WriteLine($"Distance [{startNode} -> {destination}]: {distance}");

                Console.WriteLine($"Path: {string.Join(" -> ", path)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static int FindShortestPathBellmanFord(int startNode, int destination, int verticeCount, List<Edge> edges)
        {
            var distance = new double[verticeCount];
            int?[] previous = new int?[verticeCount];
            for (int i = 0; i < distance.Length; i++)
            {
                distance[i] = double.PositiveInfinity;
            }

            distance[startNode] = 0;

            for (int i = 0; i < verticeCount - 1; i++)
            {
                foreach (Edge edge in edges)
                {
                    if (distance[edge.Start] + edge.Distance < distance[edge.End])
                    {
                        distance[edge.End] = distance[edge.Start] + edge.Distance;
                        previous[edge.End] = edge.Start;
                    }
                }
            }

            for (int i = 0; i < verticeCount - 1; i++)
            {
                foreach (Edge edge in edges)
                {
                    if (distance[edge.Start] + edge.Distance < distance[edge.End])
                    {
                        throw new ArgumentException(
                            string.Format("A negative weight cycle exists at edge ({0}, {1})",
                                edge.Start, edge.End
                                ));
                    }
                }
            }

            path = new List<int>();
            int? currentNode = destination;
            while (currentNode != null)
            {
                path.Add(currentNode.Value);
                currentNode = previous[currentNode.Value];
            }

            path.Reverse();

            return (int)distance[destination];
        }
    }
}
