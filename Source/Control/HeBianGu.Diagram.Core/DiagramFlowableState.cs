namespace HeBianGu.Diagram.Core
{
    public enum DiagramFlowableState
    {
        None = 0,
        Running,
        Success,
        Error,
        Stopped,
        Canceling,
        Canceled
    }
}
