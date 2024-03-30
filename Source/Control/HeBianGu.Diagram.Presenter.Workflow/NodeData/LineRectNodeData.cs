﻿using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace HeBianGu.Diagram.Presenter.Workflow
{
    [Display(Name = "子流程", GroupName = "基本流程图形状", Order = 2, Description = "子流程")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class LineRectNodeData : WorkflowNodeBase
    {
        private double _lineMargin = 10;
        /// <summary> 说明  </summary>
        public double LineMargin
        {
            get { return _lineMargin; }
            set
            {
                _lineMargin = value;
                RaisePropertyChanged();
            }
        }

        protected override Geometry GetGeometry()
        {
            return GeometryFactory.CreateLineRect(this.Width, this.Height, this.LineMargin);
        }
    }
}
