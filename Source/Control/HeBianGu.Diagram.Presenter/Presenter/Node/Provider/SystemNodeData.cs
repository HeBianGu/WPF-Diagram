﻿
using HeBianGu.Diagram.DrawingBox;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace HeBianGu.Diagram.Presenter
{
    public interface ISystemNodeData : INodeData
    {
        public List<IPortData> PortDatas { get; set; }
    }

    public abstract class SystemNodeData : NodeData, ISystemNodeData
    {
        //[XmlIgnore]
        [Browsable(false)]
        public List<IPortData> PortDatas { get; set; } = new List<IPortData>();

        protected override void InitPort()
        {
            this.PortDatas.Clear();
            {
                IPortData port = CreatePortData();
                port.Dock = Dock.Left;
                port.PortType = PortType.Both;
                PortDatas.Add(port);
            }

            {
                IPortData port = CreatePortData();
                port.Dock = Dock.Right;
                port.PortType = PortType.Both;
                PortDatas.Add(port);
            }

            {
                IPortData port = CreatePortData();
                port.Dock = Dock.Top;
                port.PortType = PortType.Both;
                PortDatas.Add(port);
            }

            {
                IPortData port = CreatePortData();
                port.Dock = Dock.Bottom;
                port.PortType = PortType.Both;
                PortDatas.Add(port);
            }
        }

        public override object Clone()
        {
            SystemNodeData data = base.Clone() as SystemNodeData;
            data.PortDatas.Clear();
            data.InitPort();
            return data;
        }
    }
}
