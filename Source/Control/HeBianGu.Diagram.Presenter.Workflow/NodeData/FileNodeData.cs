using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace HeBianGu.Diagram.Presenter.Workflow
{
    [Display(Name = "文档", GroupName = "基本流程图形状", Order = 8, Description = "文档")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class FileNodeData : WorkflowNodeBase
    {
        protected override Geometry GetGeometry()
        {
            return GeometryFactory.File;
        }
    }
}
