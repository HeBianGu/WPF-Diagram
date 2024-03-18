﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media;

namespace H.Controls.Diagram
{
    public class OverAdorner : BorderAdorner
    {
        public OverAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this.Fill = new SolidColorBrush(Colors.LightSkyBlue) { Opacity = 0.1 };
            this.Pen = new Pen(new SolidColorBrush(Colors.SkyBlue), 1);
        }
    }
}
