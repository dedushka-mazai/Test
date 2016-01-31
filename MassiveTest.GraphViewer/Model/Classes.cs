using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphX;
using GraphX.Controls;
using QuickGraph;
using GraphX.PCL.Common.Models;
using GraphX.PCL.Logic.Models;
using System.ComponentModel;

namespace MassiveTest.GraphVisualizer.Model
{
    /// <summary>
    /// Graph Vertex States
    /// </summary>
    public enum DataVertexState { Undefined, Selected, IsInPath, NotConnected }
    
    /// <summary>
    /// Graph area class
    /// </summary>
    public class TestGraphArea : GraphArea<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>> { }

    /// <summary>
    /// Graph data class
    /// </summary>
    public class TestGraph : BidirectionalGraph<DataVertex, DataEdge> 
    {
        internal bool isPathMarked = false;
        private DataVertex startNode = null;
        private DataVertex endNode = null;

        public DataVertex StartNode { get { return startNode; } }
        public DataVertex EndNode { get { return endNode; } }

        public void NotifyVertexSelectedChanged(DataVertex vertex)
        {
            if (vertex.Selected)
            {
                if (startNode == null)
                    startNode = vertex;
                else
                {
                    if (endNode != null)
                        endNode.Selected = false;
                    endNode = vertex;
                }
            }
            else
            {
                if (vertex == endNode)
                    endNode = null;
                else if (vertex == startNode)
                    startNode = endNode;
            }
            RemovePathMarks();
        }

        public DataVertex GetVertexById(string id)
        {
            foreach (var vertex in Vertices)
            {
                if (vertex.Id == id)
                    return vertex;
            }
            return null;
        }

        public void RemovePathMarks()
        {
            if (isPathMarked)
            {
                foreach (var vertex in Vertices)
                {
                    vertex.VState = vertex.Selected ? DataVertexState.Selected : DataVertexState.Undefined;
                }
                isPathMarked = false;
            }
        }

        public void ResetVertexStates()
        {
            foreach (var vertex in Vertices)
            {
                vertex.VState = DataVertexState.Undefined;
                vertex.Selected = false;
            }
            isPathMarked = false;
            startNode = null;
            endNode = null;
        }
    }

    /// <summary>
    /// Graph logic class
    /// </summary>
    public class TestGXLogicCore : GXLogicCore<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>> { }

    /// <summary>
    /// Data vertex class
    /// </summary>
    public class DataVertex : VertexBase, INotifyPropertyChanged
    {
        public TestGraph Owner { get; set; }
        private bool selected = false;
        private DataVertexState vstate = DataVertexState.Undefined;
        
        public string Text { get; set; }
        public string Id { get; set; }
       
        /// <summary>
        /// Data vertex state. Used while vertex control rendering
        /// </summary>
        public DataVertexState VState 
        {
            get { return vstate; }
            set 
            {
                vstate = value;
                OnPropertyChanged("VState");
            }
        }

        /// <summary>
        /// Indicates whether vertex is selected
        /// </summary>
        public bool Selected 
        {
            get { return selected; }
            set 
            { 
                selected = value;
                Owner.NotifyVertexSelectedChanged(this);
                VState = value ? DataVertexState.Selected : DataVertexState.Undefined;
            }
        }

        /// <summary>
        /// Gets string desciption of the vertex
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }

        /// <summary>
        /// Default constructor for this class
        /// (required for serialization).
        /// </summary>
        public DataVertex()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Another constructor
        /// </summary>
        /// <param name="text">Some text</param>
        public DataVertex(string text = "")
        {
            Text = string.IsNullOrEmpty(text) ? "New Vertex" : text;
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }

    /// <summary>
    /// Edge data object
    /// </summary>
    public class DataEdge : EdgeBase<DataVertex>
    {
        public DataEdge(DataVertex source, DataVertex target, double weight = 1)
            : base(source, target, weight)
        {
        }

        public DataEdge()
            : base(null, null, 1)
        {
        }

        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

}
