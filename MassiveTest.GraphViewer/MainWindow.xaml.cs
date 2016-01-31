using GraphX.Controls;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.LayoutAlgorithms;
using MassiveTest.GraphVisualizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MassiveTest.GraphVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TestGraph graph = null;

        public MainWindow()
        {
            InitializeComponent();

            ZoomControl.SetViewFinderVisibility(zoomctrl, Visibility.Hidden);
        }

        // queries Graph Service for shortest path between two selected nodes
        private void CalcShortestPath()
        {
            if (graph != null)
            {
                try
                {
                    if (graph.StartNode == null)
                        throw new Exception("Start node is not specified");
                    if (graph.EndNode == null)
                        throw new Exception("End node is not specified");

                    var proxy = new MassiveTest.Wcf.Client.DomainSpecific.DomainSpecificServiceClient();
                    try
                    {
                        var path = proxy.GetShortestPath(graph.StartNode.Id, graph.EndNode.Id);
                        if (path.Length > 0)
                        {
                            foreach (var id in path)
                            {
                                var vertex = graph.GetVertexById(id);
                                if (vertex == null)
                                    throw new Exception("Unknown node in path detected! Graph is invalid, reload it please.");
                                vertex.VState = DataVertexState.IsInPath;
                            }
                        }
                        else
                        {
                            graph.StartNode.VState = DataVertexState.NotConnected;
                            graph.EndNode.VState = DataVertexState.NotConnected;
                        }
                        graph.isPathMarked = true;
                    }
                    finally
                    {
                        proxy.Close();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // loads graph from Graph Service
        private void LoadGraphFromGraphService()
        {
            var proxy = new MassiveTest.Wcf.Client.FEOriented.ClientServiceClient();
            try
            {
                try
                {
                    var nodes = proxy.GetGraph();
                    graph = new TestGraph();
                    var vertices = new Dictionary<string, DataVertex>();
                    foreach (var node in nodes)
                    {
                        var v = new DataVertex() { Id = node.Id, Text = node.Label, Owner = graph };
                        graph.AddVertex(v);
                        vertices.Add(v.Id, v);
                    }

                    foreach (var node in nodes)
                    {
                        foreach (var adjacentId in node.AdjacentNodes)
                        {
                            DataVertex adjacentVertex = vertices.ContainsKey(adjacentId) ? vertices[adjacentId] : null;
                            if (adjacentVertex != null)
                                graph.AddEdge(new DataEdge(vertices[node.Id], adjacentVertex));
                        }
                    }
                }
                finally
                {
                    proxy.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // vertex click handler
        private void gvArea_VertexSelected(object sender, GraphX.Controls.Models.VertexSelectedEventArgs args)
        {
            DataVertex v = (DataVertex)args.VertexControl.Vertex;
            v.Selected = !v.Selected;
        }
        
        private void btnCalcShortestPath_Click(object sender, RoutedEventArgs e)
        {
            CalcShortestPath();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (graph != null)
                graph.ResetVertexStates();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadGraphFromGraphService();

            var logicCore = new TestGXLogicCore() { Graph = graph };
            logicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.KK;
            logicCore.DefaultLayoutAlgorithmParams = logicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.KK);
            ((KKLayoutParameters)logicCore.DefaultLayoutAlgorithmParams).MaxIterations = 100;

            logicCore.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            logicCore.DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 50;
            logicCore.DefaultOverlapRemovalAlgorithmParams.VerticalGap = 50;

            logicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;

            logicCore.AsyncAlgorithmCompute = false;

            //Finally assign logic core to gvArea object
            gvArea.LogicCore = logicCore;
            gvArea.GenerateGraph(true, true);

            gvArea.SetEdgesDashStyle(EdgeDashStyle.Dash);
            gvArea.ShowAllEdgesArrows(true);

            gvArea.SetVerticesDrag(true, true);
            gvArea.SetVerticesHighlight(true, GraphControlType.Vertex);

            zoomctrl.ZoomToFill(); 
        }
    }
}
