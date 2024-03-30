﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using HeBianGu.Diagram.DrawingBox;
using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;

namespace HeBianGu.Diagram.DrawingBox
{
    /// <summary> Diagram接收放下的操作 </summary>
    public abstract class DiagramDropBehaviorBase : Behavior<Diagram>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.Drop += AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            IDragAdorner adorner = e.Data.GetData("DragGroup") as IDragAdorner;

            if (adorner == null) return;

            Point offset = adorner.Offset;

            Point location = e.GetPosition(this.AssociatedObject);

            ICloneable obj = ((adorner as System.Windows.Documents.Adorner).AdornedElement as FrameworkElement).DataContext as ICloneable;

            if (obj == null)
            {
                throw new ArgumentException("没有实现ICloneable接口");
            }

            //if (this.AssociatedObject.NodesSource is IList collection)
            //{
            object content = obj.Clone();

            Node node = this.Create(content as INodeData);

            node.Content = content;

            node.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            node.Location = new Point(location.X - offset.X + (node.DesiredSize.Width / 2), location.Y - offset.Y + (node.DesiredSize.Height / 2));


            //collection.Add(node);

            //this.AssociatedObject.RefreshData();

            this.AssociatedObject.AddNode(node);
            //}

        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Drop -= AssociatedObject_Drop;
        }

        protected abstract Node Create(INodeData data);

    }
}
