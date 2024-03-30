﻿


using HeBianGu.Diagram.DrawingBox;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HeBianGu.Diagram.Presenter
{
    public class FlowablePortData : TextPortData, IFlowablePort
    {
        public FlowablePortData()
        {

        }

        public FlowablePortData(string nodeID, PortType portType) : base(nodeID, portType)
        {

        }

        private FlowableState _state = FlowableState.Ready;
        //[XmlIgnore]
        [Browsable(false)]
        public FlowableState State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged("State");
            }
        }


        private string _message;
        //[XmlIgnore]
        [Browsable(false)]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        private bool _isBuzy;
        [XmlIgnore]
        [Browsable(false)]
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged("IsBuzy");
            }
        }

        private Exception _exception;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        [XmlIgnore]
        public Exception Exception
        {
            get { return _exception; }
            set
            {
                _exception = value;
                RaisePropertyChanged("Exception");
            }
        }


        private bool _useInfoLogger = true;
        [XmlIgnore]
        [Browsable(false)]
        public bool UseInfoLogger
        {
            get { return _useInfoLogger; }
            set
            {
                _useInfoLogger = value;
                RaisePropertyChanged();
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        protected Random Random { get; } = new Random();
        protected virtual IFlowableResult OK(string message = "运行成功")
        {
            this.Message = message;
            return new FlowableResult(message) { State = FlowableResultState.OK };
        }

        protected virtual IFlowableResult Error(string message = "运行错误")
        {
            this.Message = message;
            return new FlowableResult(message) { State = FlowableResultState.Error };
        }

        [XmlIgnore]
        [Display(Name = "执行")]
        public RelayCommand InvokeCommand => new RelayCommand(async l => await this.TryInvokeAsync(null, null));

        public IFlowableResult Invoke(Part previors, Port current)
        {
            Thread.Sleep(100);
            if (true)
            {
                if (this.Random.Next(0, 19) == 1)
                    return this.Error("模拟仿真一个错误信息");
                return this.OK("模拟仿真一个成功信息");
            }
            return this.OK("运行成功");
        }

        public virtual async Task<IFlowableResult> InvokeAsync(Part previors, Port current)
        {
            return await Task.Run(() =>
            {
                return this.Invoke(previors, current);
            });
        }
        public virtual async Task<IFlowableResult> TryInvokeAsync(Part previors, Port current)
        {
            try
            {
                this.State = FlowableState.Running;

                this.IsBuzy = true;

                //IocLog.Instance?.Info($"正在执行<{this.GetType().Name}>:{this.Text}");

                IFlowableResult result = await InvokeAsync(previors, current);

                //IocLog.Instance?.Info(result.State == FlowableResultState.Error ? $"运行错误<{this.GetType().Name}>:{this.Text} {result.Message}" : $"执行完成<{this.GetType().Name}>:{this.Text} {result.Message}");

                this.State = result.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;

                return result;
            }
            catch (Exception ex)
            {
                this.State = FlowableState.Error;
                this.Exception = ex;
                this.Message = ex.Message;

                //IocLog.Instance?.Info($"执行错误<{this.GetType().Name}>:{this.Text} {this.Message}");
                //IocLog.Instance?.Error($"执行错误<{this.GetType().Name}>:{this.Text} {this.Message}");
                //IocLog.Instance?.Error(ex);


                return this.Error();
            }
            finally
            {
                this.IsBuzy = false;
            }
        }

        public virtual void Clear()
        {

        }

        public virtual void Dispose()
        {

        }
        public override ILinkData CreateLinkData()
        {
            return new FlowableLinkData() { FromNodeID = this.NodeID, FromPortID = this.ID };
        }

        public override bool CanDrop(Part part, out string message)
        {
            message = null;
            if (part.Content is IPortData port)
            {
                if (port.PortType == PortType.OutPut)
                {
                    message = "此节点是输出节点";
                    return false;
                }

                if (port.NodeID == this.NodeID)
                {
                    message = "不能连接同一个节点";
                    return false;
                }
            }


            if (!(part.Content is IFlowable))
            {
                message = "不是Flowable节点";
                return false;
            }
            return true;
        }

    }

    public class FlowablePortData<T> : FlowablePortData where T : Enum
    {
        public FlowablePortData()
        {

        }

        public FlowablePortData(string nodeID, PortType portType) : base(nodeID, portType)
        {

        }
        public override ILinkData CreateLinkData()
        {
            return new FlowableLinkData<T>() { FromNodeID = this.NodeID, FromPortID = this.ID };
        }

    }
}
