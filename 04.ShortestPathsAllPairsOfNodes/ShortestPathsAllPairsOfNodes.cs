namespace _04.ShortestPathsAllPairsOfNodes
{
    using System;
    using System.Linq;

    public static class ShortestPathsAllPairsOfNodes
    {
        private const double Inf = double.PositiveInfinity;

        public static void Main()
        {
            var nodes = InputToInteger(Console.ReadLine());
            var edges = InputToInteger(Console.ReadLine());
            var distances = new double[nodes, nodes];

            for (int row = 0; row < nodes; row++)
            {
                for (int col = 0; col < nodes; col++)
                {
                    if (row == col)
                    {
                        distances[row, col] = 0;
                    }
                    else
                    {
                        distances[row, col] = Inf;
                    }
                }
            }

            for (int e = 0; e < edges; e++)
            {
                var edgeValues = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                distances[edgeValues[0], edgeValues[1]] = edgeValues[2];
                distances[edgeValues[1], edgeValues[0]] = edgeValues[2];
            }

            for (int k = 0; k < nodes; k++)
            {
                for (int i = 0; i < nodes; i++)
                {
                    for (int j = 0; j < nodes; j++)
                    {
                        if (distances[i, k] + distances[k, j] < distances[i, j])
                        {
                            distances[i, j] = distances[i, k] + distances[k, j];
                        }
                    }
                }
            }

            for (int row = 0; row < nodes; row++)
            {
                for (int col = 0; col < nodes; col++)
                {
                    Console.Write($"{distances[row, col],-3}");
                }

                Console.WriteLine();
            }
        }

        private static int InputToInteger(string input)
        {
            var res = int.Parse(input.Split(' ')[1]);

            return res;
        }
    }
}
