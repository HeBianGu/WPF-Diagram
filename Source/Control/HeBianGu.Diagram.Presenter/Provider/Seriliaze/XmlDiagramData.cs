
using HeBianGu.Diagram.DrawingBox;
using System.Collections.Generic;

namespace HeBianGu.Diagram.Presenter
{
    public class XmlDiagramData
    {
        public List<INodeData> Nodes { get; set; } = new List<INodeData>();
        public List<ILinkData> Links { get; set; } = new List<ILinkData>();
        public List<XmlStringData> ReferenceTemplates { get; set; } = new List<XmlStringData>();
    }
}
