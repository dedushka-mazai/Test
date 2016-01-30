using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveTest.Graphs;
using Xunit;
using MassiveTest.DataProvider;

namespace MassiveTest.Tests
{
    /// <summary>
    /// Contains all graph library tests
    /// </summary>
    public class GraphsTest
    {
        private Graph PrepareGraph()
        {
            var graph = new Graph();
            var provider = new TestDataProvider();
            provider.BuildGraph(graph);

            return graph;
        }

        [Fact]
        public void Apple2Intel()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("1", "2");
            Assert.Equal(path, new string[2] { "1", "2" });
        }

        [Fact]
        public void Apple2Google()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("1", "3");
            Assert.Equal(path, new string[2] { "1", "3" });
        }

        [Fact]
        public void Google2Apple()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("3", "1");
            Assert.Equal(path, new string[4] { "3", "5", "2", "1" });
        }

        [Fact]
        public void Google2Facebook()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("3", "10");
            Assert.Equal(path, new string[4] { "3", "5", "2", "10" });
        }

        [Fact]
        public void Google2eBay()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("3", "6");
            Assert.Equal(path, new string[4] { "3", "5", "7", "6" });
        }

        [Fact]
        public void Google2IBM()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("3", "4");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void IBM2Intel()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("4", "2");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void Intel2Yahoo()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("2", "8");
            Assert.Equal(path, new string[3] { "2", "5", "8" });
        }

        [Fact]
        public void Microsoft2Amazon()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("7", "9");
            Assert.Equal(path, new string[4] { "7", "6", "10", "9" });
        }

        [Fact]
        public void Microsoft2Intel()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("7", "2");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void Microsoft2Yahoo()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("7", "8");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void Microsoft2Microsoft()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("7", "7");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void Apple2Apple()
        {
            var graph = PrepareGraph();
            var path = graph.FindShortestPath("1", "1");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void GraphAddNodeNullId()
        {
            var graph = new Graph();
            Assert.Throws(typeof(ArgumentNullException), () => graph.AddNode(null, "Intel", null));
        }

        [Fact]
        public void GraphAddNodeEmptyId()
        {
            var graph = new Graph();
            Assert.Throws(typeof(ArgumentNullException), () => graph.AddNode("", "Intel", null));
        }

        [Fact]
        public void GraphAddNodeNullLabel()
        {
            var graph = new Graph();
            Assert.Throws(typeof(ArgumentNullException), () => graph.AddNode("2", null, null));
        }

        [Fact]
        public void GraphAddNodeEmptyLabel()
        {
            var graph = new Graph();
            Assert.Throws(typeof(ArgumentNullException), () => graph.AddNode("1", "", null));
        }

        [Fact]
        public void GraphFindShortestPathNullStartId()
        {
            var graph = new Graph();
            graph.AddNode("2", "Intel", null);
            Assert.Throws(typeof(ArgumentNullException), () => graph.FindShortestPath(null, "2"));
        }

        [Fact]
        public void GraphFindShortestPathNullEndId()
        {
            var graph = new Graph();
            graph.AddNode("2", "Intel", null);
            Assert.Throws(typeof(ArgumentNullException), () => graph.FindShortestPath("2", null));
        }

        [Fact]
        public void GraphFindShortestPathInvalidStartId()
        {
            var graph = new Graph();
            graph.AddNode("2", "Intel", null);
            Assert.Throws(typeof(ArgumentException), () => graph.FindShortestPath("10", "2"));
        }

        [Fact]
        public void GraphFindShortestPathInvalidEndId()
        {
            var graph = new Graph();
            graph.AddNode("2", "Intel", null);
            Assert.Throws(typeof(ArgumentException), () => graph.FindShortestPath("2", "100"));
        }

    }
}
