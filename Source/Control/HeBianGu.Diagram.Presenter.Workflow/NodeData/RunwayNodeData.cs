using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace HeBianGu.Diagram.Presenter.Workflow
{
    [Display(Name = "开始或结束", GroupName = "基本流程图形状", Order = 2, Description = "开始或结束")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class RunwayNodeData : WorkflowNodeBase
    {
        public RunwayNodeData()
        {
            this.UseStart = true;
        }
        protected override Geometry GetGeometry()
        {
            return GeometryFactory.CreateRunway(this.Width, this.Height);
        }
    }
}
