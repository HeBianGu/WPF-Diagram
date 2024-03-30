
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace HeBianGu.Diagram.Presenter.Workflow
{
    [Display(Name = "纸带", GroupName = "基本流程图形状", Order = 5, Description = "纸带")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class WaveNodeData : WorkflowNodeBase
    {
        protected override Geometry GetGeometry()
        {
            return GeometryFactory.Wave;
        }
    }
}
