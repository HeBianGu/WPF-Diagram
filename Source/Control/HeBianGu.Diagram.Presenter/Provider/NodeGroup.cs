
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace HeBianGu.Diagram.Presenter
{
    public class NodeGroup : ObservableCollection<NodeData>
    {
        public NodeGroup()
        {

        }

        public NodeGroup(IEnumerable<NodeData> collection) : base(collection)
        {

        }
        public string Name { get; set; }

        public int Columns { get; set; }
    }
}
