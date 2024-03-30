using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace HeBianGu.Diagram.Presenter.Workflow
{
    [Display(Name = "准备", GroupName = "基本流程图形状", Order = 1, Description = "准备")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class HexagonNodeData : WorkflowNodeBase
    {
        protected override Geometry GetGeometry()
        {
            return GeometryFactory.Hexagon;
        }
    }
}
