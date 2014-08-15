using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Graphs.Dijkstra
{
    public class Dijkstra
    {
        private Graph graph;

        public Dijkstra(Graph graph)
        {
            this.graph = graph;
        }

        public List<Vertex> Run(Vertex start, Vertex end)
        {
            List<Vertex> output = new List<Vertex>();

            // should be a min-heap.
            Dictionary<int, VertexCost> unvisited = new Dictionary<int, VertexCost>();

            foreach (var vertex in this.graph.GetVertices())
            {
                if (vertex.Id == start.Id)
                {
                    output.Add(vertex);
                }
                else
                {
                    unvisited.Add(vertex.Id, new VertexCost(vertex));
                }
            }

            

            return output;
        }

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
        }
    }
}
