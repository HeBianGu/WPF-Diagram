﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media;

namespace H.Controls.Diagram
{
    public class BorderAdorner : AdornerBase
    {
        public BorderAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this.Pen = BorderAdorner.GetPen(adornedElement);
            this.Fill = BorderAdorner.GetFill(adornedElement);
        }

        public Brush Fill { get; set; }
        public double ScaleLen { get; set; }
        public Pen Pen { get; set; }
        protected override void OnRender(DrawingContext dc)
        {
            Rect rect = new Rect(this.AdornedElement.RenderSize);
            dc.DrawRectangle(this.Fill, this.Pen, new Rect(rect.Left - ScaleLen, rect.Top - ScaleLen, rect.Width + (2 * ScaleLen), rect.Height + (2 * ScaleLen)));
        }
    }
}
