﻿

using HeBianGu.Diagram.DrawingBox;
using System.ComponentModel.DataAnnotations;


namespace HeBianGu.Diagram.Presenter.Workflow
{
    [Display(Name = "事件流程图", GroupName = "流程图", Order = 6)]
    public class EventWorkflow : WorkflowBase
    {
        public EventWorkflow()
        {
            this.LinkDrawer = new BrokenLinkDrawer();
            //this.NodeGroups = NodeFactory.GetNodeGroups(DiagramType.AuditWorkflow)?.ToObservable();
        }
    }
}
