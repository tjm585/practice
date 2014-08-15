using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Graphs.Dijkstra
{
    /// <summary>
    /// Class that calculates a shortest path for a given graph.
    /// </summary>
    public class Dijkstra
    {
        private Graph graph;

        /// <summary>
        /// Creates a new Dijkstra's path solver for a graph.
        /// </summary>
        /// <param name="graph"></param>
        public Dijkstra(Graph graph)
        {
            this.graph = graph;
        }

        /// <summary>
        /// Runs the algorithm.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<Vertex> Run(Vertex start, Vertex end)
        {
            List<Vertex> output = new List<Vertex>();

            // should be a min-heap.
            Dictionary<int, VertexCost> unvisited = new Dictionary<int, VertexCost>();

            // set up the vertices.
            foreach (var vertex in this.graph.GetVertices())
            {
                VertexCost vertexCost = new VertexCost(vertex);
                unvisited.Add(vertex.Id, vertexCost);

                if (vertex.Id == start.Id)
                {
                    vertexCost.Cost = 0;
                    vertexCost.Previous = null;
                }
            }

            VertexCost currentNodeCost = null;
            while (unvisited.Count > 0)
            {
                // get the node with the lowest value and remove from unvisited array.
                currentNodeCost = unvisited.Values.OrderBy(o => o.Cost).First();
                unvisited.Remove(currentNodeCost.Vertex.Id);

                // we "visited" the node that we were looking to find the path for.
                if (currentNodeCost.Vertex.Id == end.Id)
                {
                    break;
                }

                Edge[] edges = this.graph.GetEdges(currentNodeCost.Vertex);
                foreach (var edge in edges)
                {
                    Vertex otherNode = edge.GetOtherVertex(currentNodeCost.Vertex);
                    int vertexDistance = currentNodeCost.Cost + edge.Weight;

                    if (unvisited.ContainsKey(otherNode.Id))
                    {
                        if (vertexDistance < unvisited[otherNode.Id].Cost)
                        {
                            unvisited[otherNode.Id].Cost = vertexDistance;
                            unvisited[otherNode.Id].Previous = currentNodeCost;
                        }
                    }
                }
            }

            if (currentNodeCost != null)
            {
                while (currentNodeCost != null)
                {
                    output.Add(currentNodeCost.Vertex);
                    currentNodeCost = currentNodeCost.Previous;
                }
            }

            output.Reverse();
            return output;
        }

        public static void Demo()
        {
            // set up test graph.
            Graph graph = new Graph();
            graph.AddEdge(new Vertex("A"), new Vertex("B"), 4);
            graph.AddEdge(new Vertex("A"), new Vertex("C"), 3);
            graph.AddEdge(new Vertex("A"), new Vertex("E"), 7);
            graph.AddEdge(new Vertex("B"), new Vertex("C"), 6);
            graph.AddEdge(new Vertex("C"), new Vertex("E"), 8);
            graph.AddEdge(new Vertex("B"), new Vertex("D"), 5);
            graph.AddEdge(new Vertex("C"), new Vertex("D"), 11);
            graph.AddEdge(new Vertex("E"), new Vertex("D"), 2);
            graph.AddEdge(new Vertex("D"), new Vertex("G"), 10);
            graph.AddEdge(new Vertex("E"), new Vertex("G"), 5);
            graph.AddEdge(new Vertex("D"), new Vertex("F"), 2);
            graph.AddEdge(new Vertex("G"), new Vertex("F"), 3);

            Console.WriteLine(graph.PrintGraph());

            // shortest path should be A->B->D->F for A to F.
            Dijkstra test = new Dijkstra(graph);
            var output = test.Run(new Vertex("A"), new Vertex("F"));
            Console.WriteLine(string.Join("->", output.Select(s => s.Value)));

            Console.Read();
        }

        /// <summary>
        /// Private class used to keep track of vertex distances.
        /// </summary>
        private class VertexCost
        {
            public VertexCost(Vertex vertex)
            {
                Cost = int.MaxValue;
                Vertex = vertex;
            }

            public int Cost
            {
                get;
                set;
            }

            public Vertex Vertex
            {
                get;
                set;
            }

            public VertexCost Previous
            {
                get;
                set;
            }
        }
    }
}
