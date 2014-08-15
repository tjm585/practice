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
    }
}
