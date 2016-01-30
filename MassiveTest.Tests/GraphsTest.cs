using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveTest.Graphs;
using Xunit;

namespace MassiveTest.Tests
{
    /// <summary>
    /// Contains all graph library tests
    /// </summary>
    public class GraphsTest
    {
        private Graph PrepareGraph()
        {
            var g = new Graph();

            g.AddNode("1", "Apple", new string[3] { "3", "2", "11" });
            g.AddNode("2", "Intel", new string[5] { "1", "10", "5", "9", "7" });
            g.AddNode("3", "Google", new string[1] { "5" });
            g.AddNode("4", "IBM", null);
            g.AddNode("5", "PayPal", new string[4] { "2", "8", "5", "7" });
            g.AddNode("6", "eBay", new string[2] { "10", "7" });
            g.AddNode("7", "Microsoft", new string[1] { "6" });
            g.AddNode("8", "Yahoo", new string[1] { "9" });
            g.AddNode("9", "Amazon", new string[1] { "10" });
            g.AddNode("10", "Facebook", new string[1] { "9" });

            return g;
        }

        [Fact]
        public void Apple2Intel()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("1", "2");
            Assert.Equal(path, new string[2] { "1", "2" });
        }

        [Fact]
        public void Apple2Google()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("1", "3");
            Assert.Equal(path, new string[2] { "1", "3" });
        }

        [Fact]
        public void Google2Apple()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("3", "1");
            Assert.Equal(path, new string[4] { "3", "5", "2", "1" });
        }

        [Fact]
        public void Google2Facebook()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("3", "10");
            Assert.Equal(path, new string[4] { "3", "5", "2", "10" });
        }

        [Fact]
        public void Google2eBay()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("3", "6");
            Assert.Equal(path, new string[4] { "3", "5", "7", "6" });
        }

        [Fact]
        public void Google2IBM()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("3", "4");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void IBM2Intel()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("4", "2");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void Intel2Yahoo()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("2", "8");
            Assert.Equal(path, new string[3] { "2", "5", "8" });
        }

        [Fact]
        public void Microsoft2Amazon()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("7", "9");
            Assert.Equal(path, new string[4] { "7", "6", "10", "9" });
        }

        [Fact]
        public void Microsoft2Intel()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("7", "2");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void Microsoft2Yahoo()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("7", "8");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void Microsoft2Microsoft()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("7", "7");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void Apple2Apple()
        {
            var g = PrepareGraph();
            var path = g.FindShortestPath("1", "1");
            Assert.Equal(path, new string[0]);
        }

        [Fact]
        public void GraphAddNodeNullId()
        {
            var g = new Graph();
            Assert.Throws(typeof(ArgumentNullException), () => g.AddNode(null, "Intel", null));
        }

        [Fact]
        public void GraphAddNodeEmptyId()
        {
            var g = new Graph();
            Assert.Throws(typeof(ArgumentNullException), () => g.AddNode("", "Intel", null));
        }

        [Fact]
        public void GraphAddNodeNullLabel()
        {
            var g = new Graph();
            Assert.Throws(typeof(ArgumentNullException), () => g.AddNode("2", null, null));
        }

        [Fact]
        public void GraphAddNodeEmptyLabel()
        {
            var g = new Graph();
            Assert.Throws(typeof(ArgumentNullException), () => g.AddNode("1", "", null));
        }

        [Fact]
        public void GraphFindShortestPathNullStartId()
        {
            var g = new Graph();
            g.AddNode("2", "Intel", null);
            Assert.Throws(typeof(ArgumentNullException), () => g.FindShortestPath(null, "2"));
        }

        [Fact]
        public void GraphFindShortestPathNullEndId()
        {
            var g = new Graph();
            g.AddNode("2", "Intel", null);
            Assert.Throws(typeof(ArgumentNullException), () => g.FindShortestPath("2", null));
        }

        [Fact]
        public void GraphFindShortestPathInvalidStartId()
        {
            var g = new Graph();
            g.AddNode("2", "Intel", null);
            Assert.Throws(typeof(ArgumentException), () => g.FindShortestPath("10", "2"));
        }

        [Fact]
        public void GraphFindShortestPathInvalidEndId()
        {
            var g = new Graph();
            g.AddNode("2", "Intel", null);
            Assert.Throws(typeof(ArgumentException), () => g.FindShortestPath("2", "100"));
        }

    }
}
