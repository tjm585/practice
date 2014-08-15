using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Graphs
{
    /// <summary>
    /// Class representing a graph.
    /// </summary>
    public class Graph
    {
        private Dictionary<int, Vertex> vertices;
        private List<Edge> edges;

        /// <summary>
        /// Creates a new instance of <see cref="Graph"/>.
        /// </summary>
        public Graph()
        {
            this.vertices = new Dictionary<int, Vertex>();
            this.edges = new List<Edge>();
        }

        /// <summary>
        /// Adds a new edge to the graph.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="weight"></param>
        public void AddEdge(Vertex a, Vertex b, int weight)
        {
            // only add the vertices to the list if it is
            // not in the list already.
            if (!this.vertices.ContainsKey(a.Id))
            {
                this.vertices.Add(a.Id, a);
            }

            if (!this.vertices.ContainsKey(b.Id))
            {
                this.vertices.Add(b.Id, b);
            }

            // add the new edge.
            // TODO: check for dupe edges as well?
            var edge = new Edge(a, b, weight);
            this.edges.Add(edge);
        }

        /// <summary>
        /// Returns an array of vertices in the graph.
        /// </summary>
        /// <returns></returns>
        public Vertex[] GetVertices()
        {
            return this.vertices.Values.ToArray();
        }

        public Vertex[] GetAdjacentVertices(Vertex vertex)
        {
            // TODO:
            return null;
        }

        /// <summary>
        /// Creates a string representing the graph.
        /// </summary>
        /// <returns></returns>
        public string PrintGraph()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var edge in this.edges)
            {
                sb.AppendLine(edge.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Prints a demo of the graph.
        /// </summary>
        public static void Demo()
        {
            Graph graph = new Graph();
            graph.AddEdge(new Vertex("Hello"), new Vertex("World"), 20);
            graph.AddEdge(new Vertex("Foo"), new Vertex("World"), 10);
            graph.AddEdge(new Vertex("Foo"), new Vertex("Bar"), 30);

            Console.WriteLine(graph.PrintGraph());
            Console.Read();
        }
    }

    /// <summary>
    /// Class representing a graph vertex.
    /// </summary>
    public class Vertex
    {
        private string value;

        /// <summary>
        /// Creates a new instance of <see cref="Vertex"/>.
        /// </summary>
        /// <param name="value"></param>
        public Vertex(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets a unique ID for the vertex.
        /// </summary>
        public int Id
        {
            get
            {
                return this.value.GetHashCode();
            }
        }

        /// <summary>
        /// Gets the value of the vertex.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }
        }
    }

    /// <summary>
    /// Class representing a graph edge.
    /// </summary>
    public class Edge
    {
        private Vertex[] vertices;

        /// <summary>
        /// Creates a new instance of <see cref="Edge"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="weight"></param>
        public Edge(Vertex a, Vertex b, int weight)
        {
            this.vertices = new Vertex[2];
            this.vertices[0] = a;
            this.vertices[1] = b;

            Weight = weight;
        }

        /// <summary>
        /// Gets or sets the weight of the edge.
        /// </summary>
        public int Weight
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("{0}->{1}, weight: {2}", this.vertices[0].Value, this.vertices[1].Value, this.Weight);
        }
    }
}
