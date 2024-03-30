using HeBianGu.Diagram.DrawingBox;
using System.Collections.Generic;

namespace HeBianGu.Diagram.Presenter
{
    public class DiagramFlowableDataSourceConverter : DiagramDataSourceConverter
    {
        public DiagramFlowableDataSourceConverter(List<Node> nodeSource) : base(nodeSource)
        {
        }

        public DiagramFlowableDataSourceConverter(IEnumerable<INodeData> nodes, IEnumerable<ILinkData> links) : base(nodes, links)
        {

        }

        protected override ILinkData CreateLinkData()
        {
            return new FlowableLinkData();
        }

    }
}
