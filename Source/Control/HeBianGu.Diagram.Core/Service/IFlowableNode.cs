﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Threading.Tasks;

namespace HeBianGu.Diagram.Core
{
    public interface IFlowableNode : IFlowable
    {
        public bool UseStart { get; set; }
        IFlowableResult Invoke(Part previors, Node current);
        Task<IFlowableResult> InvokeAsync(Part previors, Node current);
        Task<IFlowableResult> TryInvokeAsync(Part previors, Node current);
    }
}
