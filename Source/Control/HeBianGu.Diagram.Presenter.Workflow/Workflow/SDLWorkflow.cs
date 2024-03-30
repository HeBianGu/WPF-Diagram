﻿

using HeBianGu.Diagram.DrawingBox;
using System.ComponentModel.DataAnnotations;


namespace HeBianGu.Diagram.Presenter.Workflow
{
    [Display(Name = "SDL图", GroupName = "流程图", Order = 8)]
    public class SDLWorkflow : WorkflowBase
    {
        public SDLWorkflow()
        {
            this.LinkDrawer = new BrokenLinkDrawer();
            //this.NodeGroups = NodeFactory.GetNodeGroups(DiagramType.AuditWorkflow)?.ToObservable();
        }
    }
}
