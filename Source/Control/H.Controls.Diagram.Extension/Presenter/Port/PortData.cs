﻿



using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace H.Controls.Diagram.Extension
{
    public abstract class PortData : PortDataBase, IDropable, ILinkDataCreator
    {
        public PortData()
        {

        }

        public PortData(string nodeID) : base(nodeID)
        {

        }

        public virtual bool CanDrop(Part part, out string message)
        {
            message = null;
            if (part.Content is IPortData port)
            {
                if (port.PortType == PortType.OutPut)
                {
                    message = "不能连接输出端口";
                    return false;
                }

                if (port.NodeID == this.NodeID)
                {
                    message = "不能连接同一个节点";
                    return false;
                }

            }

            if (part.Content?.GetType() != this.GetType())
            {
                message = $"不是<{this.Name}>类型端口";
                return false;
            }

            return true;
        }

        public virtual ILinkData CreateLinkData()
        {
            return new DefaultLinkData() { FromNodeID = this.NodeID, FromPortID = this.ID };
        }
    }

    public abstract class PortDataBase : DisplayBindableBase, IPortData
    {
        public PortDataBase()
        {
            this.Name = this.Name == this.ID ? "端口" : this.Name;
            this.ID = Guid.NewGuid().ToString();
        }

        public PortDataBase(string nodeID) : this()
        {
            this.NodeID = nodeID;
        }

        [Browsable(false)]
        public Dock Dock { get; set; }
        [Browsable(false)]
        public string NodeID { get; set; }
        public PortType PortType { get; set; }
        public Thickness PortMargin { get; set; } = new Thickness(0, 0, 0, 0);

        [XmlIgnore]
        [Display(Name = "删除", GroupName = "操作")]
        public RelayCommand DeleteCommand => new RelayCommand((s, e) =>
        {
            if (e is Part part)
            {
                part.Delete();
            }
        });

        [XmlIgnore]
        [Display(Name = "恢复默认", GroupName = "操作")]
        public override RelayCommand LoadDefaultCommand => new RelayCommand((s, e) =>
        {
            this.LoadDefault();
        });

        [XmlIgnore]
        [Display(Name = "保存模板", GroupName = "操作")]
        public RelayCommand SaveAsTemplateCommand => new RelayCommand((s, e) =>
        {
            if (e is Node node)
            {

            }
        });

        [XmlIgnore]
        [Display(Name = "缩放定位", GroupName = "操作")]
        public RelayCommand LocateFullCommand => new RelayCommand((s, e) =>
        {
            if (e is Port node)
            {
                if (node.GetDiagram().DataContext is DiagramBase diagram)
                {
                    diagram.LocateRect(node);
                }
            }
        });

        [XmlIgnore]
        [Display(Name = "平移定位", GroupName = "操作")]
        public RelayCommand LocateMoveCommand => new RelayCommand((s, e) =>
        {
            if (e is Port node)
            {
                if (node.GetDiagram().DataContext is DiagramBase diagram)
                {
                    diagram.LocateCenter(node);
                }
            }
        });

        [XmlIgnore]
        [Display(Name = "应用到全部", GroupName = "操作")]
        public RelayCommand ApplyToAllCommand => new RelayCommand((s, e) =>
        {
            if (e is Port part)
            {
                Diagram diagram = part.GetParent<Diagram>();

                foreach (PortData item in diagram.Nodes.SelectMany(x => x.GetPorts()).Select(x => x.Content).OfType<PortData>())
                {
                    this.ApplayStyleTo(item);
                }
            }
        });

        [XmlIgnore]
        [Display(Name = "应用到同类型", GroupName = "操作")]
        public RelayCommand ApplyToTypeCommand => new RelayCommand((s, e) =>
        {
            if (e is Port node)
            {
                Diagram diagram = node.GetParent<Diagram>();
                foreach (PortData item in diagram.Nodes.SelectMany(x => x.GetPorts()).Select(x => x.Content).OfType<PortData>().Where(x => x.GetType().IsAssignableFrom(this.GetType())))
                {
                    this.ApplayStyleTo(item);
                }
            }
        });


        public virtual void ApplayStyleTo(IPortData node)
        {

        }

        public virtual void InitLink(Link link)
        {

        }


        private bool _hasError;
        /// <summary> 说明  </summary>
        public bool HasError
        {
            get { return _hasError; }
            set
            {
                _hasError = value;
                RaisePropertyChanged();
            }
        }

    }
}
