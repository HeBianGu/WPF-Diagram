using HeBianGu.Diagram.DrawingBox;
using System.Threading.Tasks;

namespace HeBianGu.Diagram.Presenter
{
    public interface IFlowableDiagram : IDiagram
    {
        DiagramFlowableMode StartMode { get; set; }
        DiagramFlowableState State { get; set; }
        string Message { get; set; }
        Task<bool?> Start();
        Task<bool?> Start(Node startNode);
    }
}
