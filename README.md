 # <img align="left" src="./Document/logo.png" width="40"/> WPF-Diagram  |  [English](README.md)
 WPF流程图控件
<p align="left"> 
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v7.0-windows.svg"></img>
</p>

<p align="left"> 
    <img alt="csharp-version" src="https://img.shields.io/badge/C%23-9.0-blue.svg"></img>
    <img alt="IDE-version" src="https://img.shields.io/badge/IDE-vs2022-blue.svg"></img>
</p>

<p align="left"> 
    <a href="https://www.nuget.org/packages?q=HeBianGu.Diagram.DrawingBox">
        <img alt="nuget-version" src="https://img.shields.io/nuget/v/HeBianGu.Diagram.DrawingBox.svg"></img>
    </a>
     <a href="https://github.com/HeBianGu/WPF-Diagram/actions?query=workflow%3Abuild">
        <img alt="Github-build-status" src="https://github.com/HeBianGu/WPF-Diagram/actions/workflows/main.yml/badge.svg"></img>
    </a>
</p>

## 预览

![qrcode](https://raw.githubusercontent.com/HeBianGu/WPF-Diagram/main/Document/1.png)
![qrcode](https://raw.githubusercontent.com/HeBianGu/WPF-Diagram/main/Document/2.png)
![qrcode](https://raw.githubusercontent.com/HeBianGu/WPF-Diagram/main/Document/3.png)
![qrcode](https://raw.githubusercontent.com/HeBianGu/WPF-Diagram/main/Document/4.png)

[演示视频](https://www.bilibili.com/video/BV1qy421i74b/?spm_id_from=333.999.0.0) 

## 推荐学习官方文档
https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls?view=windowsdesktop-8.0?wt.mc_id=MVP_380318
## 推荐查看在线源码
https://referencesource.microsoft.com/?wt.mc_id=MVP_380318


二次开发（开发者文档）—— WPF-Diagram 控件

本文档面向需要基于本控件进行二次开发的工程师，说明项目结构、关键类型、扩展点以及典型自定义场景（自定义节点、端口、连线与数据源转换）。

目录
- 项目与模块概览
- 关键类型与关系
- 在 XAML 中集成 Diagram
- 数据驱动与 GraphSource（推荐）
- 自定义功能节点（执行逻辑与 UI）
- 自定义端口与连线
- 布局、样式与模板
- 运行/调试/扩展建议
- 常见参考位置


1. 项目与模块概览

本仓库的控件主要代码位于：
- `..\Source\Control\HeBianGu.Diagram.DrawingBox` —— 控件实现（核心）
- `..\Source\Control\HeBianGu.Diagram.Presenter` —— 演示/Presenter 层（示例节点数据、样式与命令）
- `..\Source\App\HeBianGu.Test.Diagram` —— 演示应用（如有）

核心功能分层：
- `Diagram`：整体容器（`Diagram.xaml.cs`）—— 管理 `NodeLayer`、`LinkLayer`、Nodes/Links 数据、命令与路由事件（如 `AddLinked`、`ItemsChanged`、`RunningPartChanged`）。
- `Part`：基础元素基类（`Part`）—— `Node`、`Link`、`Port` 都继承自 `Part`。
- `Node` / `Port` / `Link`：图元实现，包含位置、连接关系与显示逻辑。
- `GraphSource`：数据源转换抽象，负责把业务 POCO（例如 `Unit` / `Wire`）转换为 `Node` / `Link` 实例。
- `IFlowable` / `IFlowableNode` / `IFlowablePort` / `IFlowableLink`：执行/运行时相关接口，支持节点/端口/连线的运行逻辑（用于流程执行功能）。

主要布局算法在 `Layout` 子目录（如 `TreeLayout`、`GridLayout`、`ForceDirectedLayout` 等）。


2. 关键类型与关系（快捷参考）
- `HeBianGu.Diagram.DrawingBox.Diagram`：主控件，XAML 使用 `diag:Diagram`。
- `Node`：代表一个可拖拽的节点（显示 + 内容）。节点的 `Content` 通常是业务数据（POCO）或实现 `IFlowableNode` 的对象。
- `Port`：节点上的连接点。通过 `Port.Create(node)` 创建并添加到节点。
- `Link`：连接线，使用 `Link.Create(from, to)` 或 `Link.Create(fromNode, toNode, fromPort, toPort)` 创建。
- `GraphSource<TNode, TLink>`：继承后实现 `ConvertToNode` 与 `ConvertToLink`，将业务数据转换成 `Node`、`Port`、`Link`。
- `Part`：`Node`/`Port`/`Link` 的公共父类，提供选择、删除、状态、事件等功能。


3. 在 XAML 中集成 Diagram（示例）

1) 引入命名空间：

```xml
xmlns:diag="clr-namespace:HeBianGu.Diagram.DrawingBox;assembly=HeBianGu.Diagram.DrawingBox"
```

2) 在窗口或控件中放置 Diagram：

```xml
<diag:Diagram x:Name="Diagram"
              NodesSource="{Binding NodesSource}"
              FlowableMode="Link" />
```

说明：`NodesSource` 接受一个 `IList`（默认是 `ObservableCollection<Node>`）。最常用的方式是将业务模型通过 `GraphSource` 转换为 `Node` 集合，再把该集合赋值给 `Diagram.NodesSource`。


4. 数据驱动与 GraphSource（推荐）

推荐使用 `GraphSource<TNodeData, TLinkData>` 将你的业务 POCO 转为 `Node`/`Link`。示例参考：`GraphSource\UnitGraphSource.cs`。

示例：
- 业务模型：`Unit`、`Wire`（见 `GraphSource\Model`）
- 转换器：`UnitGraphSource` 的 `ConvertToNode(Unit)` 中创建 `Node`、创建 `Port` 并把 `Unit` 作为 `Node.Content`。

创建并使用：

```csharp
List<Unit> units = LoadYourUnits();
List<Wire> wires = LoadYourWires();
var graph = new UnitGraphSource(units, wires); // GraphSource 会在构造时创建 Node 列表
Diagram.NodesSource = graph.NodeSource; // 或者绑定到 ViewModel
```

优点：
- 清晰分离业务数据与展示控件
- 支持序列化 / 反序列化（保存/加载）
- 可以在 `ConvertToNode` 中设置 `Node.Location`、添加 `Port`、为 `Node.Content` 赋具体对象（可能实现执行接口）


5. 自定义功能节点（执行逻辑与 UI）

实现要点：

方法 A（推荐，基于 `IFlowableNode`）：
- 定义业务节点类并实现 `IFlowableNode`（或继承 `FlowableNodeData` 示例类）：见 `HeBianGu.Diagram.Presenter\Presenter\Node\FlowableNodeData.xaml.cs`。
- 在 `ConvertToNode` 时把该对象作为 `Node.Content`：`node.Content = new MyFlowableNodeData { ... }`。
- 执行时，控件会调用 `node.GetContent<IFlowableNode>().TryInvokeAsync(...)` 等方法，实现节点运行逻辑（同步或异步）。

关键接口（摘录）：
- `IFlowableNode`（继承 `IFlowable`）：定义 `Invoke`、`InvokeAsync`、`TryInvokeAsync`，并有 `UseStart`、`State` 等属性。
- `IFlowablePort` / `IFlowableLink`：若你的端口/连线有运行逻辑，也应实现对应接口。

示例：一个简单的 `IFlowableNode` 实现（伪代码）：

```csharp
public class MyTaskNode : IFlowableNode
{
    public string ID { get; set; }
    public FlowableState State { get; set; }
    public Task<IFlowableResult> InvokeAsync(Part pre, Node current) { ... }
    public IFlowableResult Invoke(Part pre, Node current) { ... }
    public Task<IFlowableResult> TryInvokeAsync(Part pre, Node current) { ... }
    public void Clear() { ... }
}

// 在 GraphSource.ConvertToNode
Node node = new Node();
node.Content = new MyTaskNode { ID = unit.Id };
```

方法 B（自定义 `Node` 控件子类）：
- 如果需要扩展视觉树或重写鼠标/布局行为，可以继承 `Node` 并创建自定义 `Style`/`ControlTemplate`（`Node` 本身在 `Part\Node.xaml.cs` 中）。
- 将自定义 `Node` 实例加入 `NodesSource`。

UI 自定义：
- 节点模板/样式：`Diagram.NodeStyle`、`Node` 的默认样式（见 `Part\Node.xaml`/资源）可通过 `Style` 或 `DataTemplate` 覆盖。
- 节点内部 `Content` 的渲染，可通过为内容类型定义 `DataTemplate` 来实现（例如为 `MyTaskNode` 定义一个 `DataTemplate`）。

示例 DataTemplate：

```xml
<DataTemplate DataType="{x:Type presenter:MyTaskNode}">
  <Border Background="#FFF" Padding="6">
    <StackPanel>
      <TextBlock Text="{Binding Name}" FontWeight="Bold" />
      <TextBlock Text="{Binding Message}" FontSize="12" />
    </StackPanel>
  </Border>
</DataTemplate>
```


6. 自定义端口与连线

- 创建端口：`Port.Create(node)`，然后 `port.Content = yourPortData`，并设置 `port.Dock = Dock.Left/Top/...`。
- 创建连线：`Link.Create(fromNode, toNode, fromPort, toPort)` 或 `Link.Create(fromNode, toNode)`。
- 如果端口/连线需要特殊初始化或数据生成，实现接口：`ILinkInitializer`、`ILinkDataCreator`、`IPortDataCreator`，控件会在 `Link.Create` 中调用它们（请参考 `Link.xaml.cs`）。

连线样式与路径：
- `Diagram.LinkDrawer` 决定连线路径绘制（`BezierLinkDrawer`、`LineLinkDrawer`、`ArcLinkDrawer` 等实现位于 `LinkDrawer` 目录），可以替换或自定义自己的 `LinkDrawer`。


7. 布局、样式与模板

- `Diagram.Layout` 接受 `ILayout` 实例（如 `TreeLayout`、`GridLayout`、`ForceDirectedLayout`），可以通过设置 `Diagram.Layout` 来改变布局。
- `Diagram.NodeStyle` 与 `Diagram.LinkStyle` 可以在 XAML 中设置为 `StaticResource`，从而统一控制样式。
- 节点/端口/连线的默认样式资源在 `Part` 目录下的 XAML/资源中定义，扩展时建议复用并修改。


8. 运行/调试/扩展建议

- 运行逻辑：流程运行入口由 `Diagram` 的 `Start` 命令触发，会检索 `UseStart` 为 true 的起始节点并递归执行节点/端口/连线（见 `Diagram.xaml.cs` 与 `Node.StartPort`）。
- 调试方法：
  - 在 `GraphSource.ConvertToNode` 中临时设置断点，确认 `Node.Content`、`Port` 与 `Link` 建立正确；
  - 使用 `Diagram.RefreshData()` 强制刷新并观察 `NodeLayer` 与 `LinkLayer` 的 `Children`；
  - 确认 `DataTemplate`/`Style` 的 `DataType` 与 `Node.Content` 的类型一致。
- 性能：大量节点时尽量减少复杂绑定与视觉树深度，必要时使用布局策略或分区策略优化渲染。


9. 常见参考位置（仓库内）
- `..\Source\Control\HeBianGu.Diagram.DrawingBox\Diagram.xaml.cs` —— Diagram 控件实现
- `..\Source\Control\HeBianGu.Diagram.DrawingBox\Part\Node.xaml.cs` —— Node 实现
- `..\Source\Control\HeBianGu.Diagram.DrawingBox\Part\Port.xaml.cs` —— Port 实现
- `..\Source\Control\HeBianGu.Diagram.DrawingBox\Part\Link.xaml.cs` —— Link 实现
- `..\Source\Control\HeBianGu.Diagram.DrawingBox\GraphSource\UnitGraphSource.cs` —— GraphSource 使用示例
- `..\Source\Control\HeBianGu.Diagram.Presenter\Presenter\Node\FlowableNodeData.xaml.cs` —— `IFlowableNode` 示例实现


最后说明
- 本文为二次开发指南的概览与实践建议；如果需要我可以：
  - 生成一个最小可运行的 Sample（示例项目 / `MainWindow` + `ViewModel`），演示完整从 POCO -> GraphSource -> Diagram 的流程；
  - 或根据你要实现的具体功能（例如自定义节点带特殊端口行为、并行执行策略等）撰写更详细的实现步骤与代码示例。

