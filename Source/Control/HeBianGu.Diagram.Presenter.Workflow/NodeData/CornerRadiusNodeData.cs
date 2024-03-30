﻿using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace HeBianGu.Diagram.Presenter.Workflow
{
    [Display(Name = "流程", GroupName = "基本流程图形状", Order = 3, Description = "流程")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class CornerRadiusNodeData : WorkflowNodeBase
    {
        private double _value = 5;
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        protected override Geometry GetGeometry()
        {
            return GeometryFactory.CreateCornerRadius(this.Width, this.Height, this.Value);
        }
    }
}
