﻿using GraphX.Controls;
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
        public MainWindow()
        {
            InitializeComponent();

            ZoomControl.SetViewFinderVisibility(zoomctrl, Visibility.Hidden);

        }

        private GraphExample GraphExample_Setup()
        {
            //Lets make new data graph instance
            var dataGraph = new GraphExample();
            //Now we need to create edges and vertices to fill data graph
            //This edges and vertices will represent graph structure and connections
            //Lets make some vertices
            for (int i = 1; i < 10; i++)
            {
                //Create new vertex with specified Text. Also we will assign custom unique ID.
                //This ID is needed for several features such as serialization and edge routing algorithms.
                //If you don't need any custom IDs and you are using automatic Area.GenerateGraph() method then you can skip ID assignment
                //because specified method automaticaly assigns missing data ids (this behavior is controlled by method param).
                var dataVertex = new DataVertex("MyVertex " + i);
                //Add vertex to data graph
                dataGraph.AddVertex(dataVertex);
            }

            //Now lets make some edges that will connect our vertices
            //get the indexed list of graph vertices we have already added
            var vlist = dataGraph.Vertices.ToList();
            //Then create two edges optionaly defining Text property to show who are connected
            var dataEdge = new DataEdge(vlist[0], vlist[1]) { Text = string.Format("{0} -> {1}", vlist[0], vlist[1]) };
            dataGraph.AddEdge(dataEdge);
            dataEdge = new DataEdge(vlist[2], vlist[3]) { Text = string.Format("{0} -> {1}", vlist[2], vlist[3]) };
            dataGraph.AddEdge(dataEdge);

            return dataGraph;
        }


        private void gg_but_randomgraph_Click(object sender, RoutedEventArgs e)
        {
            var logicCore = new GXLogicCoreExample() { Graph = GraphExample_Setup() };
            //This property sets layout algorithm that will be used to calculate vertices positions
            //Different algorithms uses different values and some of them uses edge Weight property.
            logicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.KK;
            //Now we can set parameters for selected algorithm using AlgorithmFactory property. This property provides methods for
            //creating all available algorithms and algo parameters.
            logicCore.DefaultLayoutAlgorithmParams = logicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.KK);
            //Unfortunately to change algo parameters you need to specify params type which is different for every algorithm.
            ((KKLayoutParameters)logicCore.DefaultLayoutAlgorithmParams).MaxIterations = 100;

            //This property sets vertex overlap removal algorithm.
            //Such algorithms help to arrange vertices in the layout so no one overlaps each other.
            logicCore.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            //Default parameters are created automaticaly when new default algorithm is set and previous params were NULL
            logicCore.DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 50;
            logicCore.DefaultOverlapRemovalAlgorithmParams.VerticalGap = 50;

            //This property sets edge routing algorithm that is used to build route paths according to algorithm logic.
            //For ex., SimpleER algorithm will try to set edge paths around vertices so no edge will intersect any vertex.
            //Bundling algorithm will try to tie different edges that follows same direction to a single channel making complex graphs more appealing.
            logicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;

            //This property sets async algorithms computation so methods like: Area.RelayoutGraph() and Area.GenerateGraph()
            //will run async with the UI thread. Completion of the specified methods can be catched by corresponding events:
            //Area.RelayoutFinished and Area.GenerateGraphFinished.
            logicCore.AsyncAlgorithmCompute = false;

            //Finally assign logic core to GraphArea object
            gvArea.LogicCore = logicCore;
            gvArea.GenerateGraph(true, true);

            gvArea.SetEdgesDashStyle(EdgeDashStyle.Dash);

            //This method sets edges arrows visibility. It is also applied to all edges in Area.EdgesList. You can also set property for
            //each edge individually using property, for ex: Area.EdgesList[0].ShowArrows = true;
            gvArea.ShowAllEdgesArrows(true);

            //This method sets edges labels visibility. It is also applied to all edges in Area.EdgesList. You can also set property for
            //each edge individually using property, for ex: Area.EdgesList[0].ShowLabel = true;
//            Area.ShowAllEdgesLabels(true);

            zoomctrl.ZoomToFill(); 

        }
    }
}
