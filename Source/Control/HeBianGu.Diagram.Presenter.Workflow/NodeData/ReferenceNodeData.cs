﻿using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace HeBianGu.Diagram.Presenter.Workflow
{
    [Display(Name = "跨页引用", GroupName = "基本流程图形状", Order = 11, Description = "跨页引用")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class ReferenceNodeData : WorkflowNodeBase
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.Height = 100;
            this.Width = 100;
        }
        protected override Geometry GetGeometry()
        {
            this.Height = 100;
            this.Width = 100;
            GeometryConverter converter = new GeometryConverter();
            return converter.ConvertFromString("F1M0,0 100,0 100,50 50,100 0,50Z") as Geometry;
        }
    }
}
