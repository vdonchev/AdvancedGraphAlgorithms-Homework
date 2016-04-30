namespace _01.ExtendACableNetwork
{
    using System;

    public class Edge : IComparable<Edge>
    {
        public Edge(int startNode, int endNode, int weight)
        {
            this.StartNode = startNode;
            this.EndNode = endNode;
            this.Weight = weight;
        }

        public int StartNode { get; private set; }

        public int EndNode { get; private set; }

        public int Weight { get; private set; }

        public int CompareTo(Edge other)
        {
            int weightCompared = this.Weight.CompareTo(other.Weight);

            return weightCompared;
        }

        public override string ToString()
        {
            return $"{{{this.StartNode}, {this.EndNode}}} -> {this.Weight}";
        }
    }
}