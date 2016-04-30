namespace _01.ExtendACableNetwork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ExtendACableNetwork
    {
        public static void Main()
        {
            var budget = InputToInteger(Console.ReadLine());
            var customersCount = InputToInteger(Console.ReadLine());
            var connectionsCount = InputToInteger(Console.ReadLine());
            var allConnections = new List<Edge>();
            var alreadyConnected = new bool[customersCount];

            for (int e = 0; e < connectionsCount; e++)
            {
                var connectionDetails = Console.ReadLine().Split(' ');
                var connectionValues = connectionDetails.Take(3).Select(int.Parse).ToArray();
                if (connectionDetails.Length > 3)
                {
                    alreadyConnected[connectionValues[0]] = true;
                    alreadyConnected[connectionValues[1]] = true;
                }

                var edge = new Edge(connectionValues[0], connectionValues[1], connectionValues[2]);
                allConnections.Add(edge);
            }

            allConnections.Sort();
            var usedBudget = 0;
            foreach (var connection in allConnections)
            {
                if (connection.Weight > budget)
                {
                    break;
                }

                if (alreadyConnected[connection.StartNode] ^ alreadyConnected[connection.EndNode])
                {
                    Console.WriteLine(connection);
                    var newPoint = alreadyConnected[connection.StartNode] ? connection.EndNode : connection.StartNode;

                    alreadyConnected[newPoint] = true;
                    budget -= connection.Weight;
                    usedBudget += connection.Weight;
                }
            }

            Console.WriteLine($"Budget used: {usedBudget}");
        }

        private static int InputToInteger(string input)
        {
            var res = int.Parse(input.Split(' ')[1]);

            return res;
        }
    }
}
