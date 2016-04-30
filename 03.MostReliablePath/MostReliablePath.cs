namespace _03.MostReliablePath
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MostReliablePath
    {
        public static void Main()
        {
            var nodes = InputToInteger(Console.ReadLine());

            var graph = new int[nodes, nodes];

            var destionationPoints = Console.ReadLine()
                .Split(' ')
                .Where((e, i) => i % 2 == 1)
                .Select(int.Parse)
                .ToArray();

            var startPoint = destionationPoints[0];
            var endPoint = destionationPoints[1];
            var edges = InputToInteger(Console.ReadLine());

            for (int e = 0; e < edges; e++)
            {
                var edgeDetails = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                graph[edgeDetails[0], edgeDetails[1]] = edgeDetails[2];
                graph[edgeDetails[1], edgeDetails[0]] = edgeDetails[2];
            }

            FindSafestPath(graph, startPoint, endPoint);
        }

        private static void FindSafestPath(int[,] graph, int startPoint, int endPoint)
        {
            var path = Dijkstra(graph, startPoint, endPoint);
            if (path == null)
            {
                Console.WriteLine("no path");
            }
            else
            {
                var pathSafnest = 1d;
                for (int i = 0; i < path.Count - 1; i++)
                {
                    pathSafnest *= (graph[path[i], path[i + 1]] / 100d);
                }

                Console.WriteLine($"Most reliable path reliability: {pathSafnest * 100:f2}%");
                var formattedPath = string.Join(" -> ", path);
                Console.WriteLine("{0}", formattedPath);
            }
        }

        private static List<int> Dijkstra(int[,] graph, int startPoint, int endPoint)
        {
            int n = graph.GetLength(0);

            int[] distance = new int[n];
            for (int i = 0; i < n; i++)
            {
                distance[i] = int.MinValue;
            }

            distance[startPoint] = 0;

            var used = new bool[n];
            int?[] previous = new int?[n];
            while (true)
            {
                int minDistance = int.MinValue;
                int minNode = 0;
                for (int node = 0; node < n; node++)
                {
                    if (!used[node] && distance[node] > minDistance)
                    {
                        minDistance = distance[node];
                        minNode = node;
                    }
                }

                if (minDistance == int.MinValue)
                {
                    break;
                }

                used[minNode] = true;
                
                for (int i = 0; i < n; i++)
                {
                    if (graph[minNode, i] > 0)
                    {
                        int newDistance = distance[minNode] + graph[minNode, i];
                        if (newDistance > distance[i] && !used[i])
                        {
                            distance[i] = newDistance;
                            previous[i] = minNode;
                        }
                    }
                }
            }

            if (distance[endPoint] == int.MaxValue)
            {
                return null;
            }
            
            var path = new List<int>();
            int? currentNode = endPoint;
            while (currentNode != null)
            {
                path.Add(currentNode.Value);
                currentNode = previous[currentNode.Value];
            }

            path.Reverse();

            return path;
        }

        private static int InputToInteger(string input)
        {
            var res = int.Parse(input.Split(' ')[1]);

            return res;
        }
    }
}
