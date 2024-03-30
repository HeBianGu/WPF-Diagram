﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections.Generic;

namespace HeBianGu.Diagram.Core
{
    public interface IDataSource<NodeDataType, LinkDataType>
    {
        List<NodeDataType> GetNodeType();

        List<LinkDataType> GetLinkType();
    }
}
