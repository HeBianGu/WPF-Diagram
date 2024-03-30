

using HeBianGu.Diagram.DrawingBox;
using System.ComponentModel.DataAnnotations;


namespace HeBianGu.Diagram.Presenter.Workflow
{
    [Display(Name = "业务流程建模标注图", GroupName = "流程图", Order = 4)]
    public class BusinessMarkWorkflow : WorkflowBase
    {
        public BusinessMarkWorkflow()
        {
            this.LinkDrawer = new BrokenLinkDrawer();
            //this.NodeGroups = NodeFactory.GetNodeGroups(DiagramType.AuditWorkflow)?.ToObservable();
        }
    }
}
