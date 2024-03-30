



using HeBianGu.Diagram.DrawingBox;
using System.Linq;
using System.Windows.Controls;

namespace HeBianGu.Diagram.Presenter
{
    public class ApplayStyleCommand : MarkupCommandBase
    {
        public bool UseApplayToAll { get; set; }

        public override void Execute(object parameter)
        {
            if (parameter is ContextMenu menu)
            {
                Node node = menu.PlacementTarget.GetParent<Node>();
                HeBianGu.Diagram.DrawingBox.Diagram diagram = node.GetParent<HeBianGu.Diagram.DrawingBox.Diagram>();
                if (node.Content is NodeData data)
                {
                    if (UseApplayToAll)
                    {
                        diagram.Nodes.ForEach(x =>
                        {
                            if (x.Content is NodeData nodeData)
                                data.ApplayStyleTo(nodeData);
                        });
                    }
                    else
                    {
                        System.Collections.Generic.IEnumerable<NodeData> finds = diagram.Nodes.Select(x => x.Content).OfType<NodeData>().Where(x => x.GetType().IsAssignableFrom(data.GetType()));

                        foreach (NodeData item in finds)
                        {
                            data.ApplayStyleTo(item);
                        }
                    }

                }
            }


        }
    }
}
