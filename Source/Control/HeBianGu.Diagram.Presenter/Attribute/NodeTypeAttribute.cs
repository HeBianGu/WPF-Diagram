using System;

namespace HeBianGu.Diagram.Presenter
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class NodeTypeAttribute : Attribute
    {
        public Type DiagramType { get; set; }
        public string GroupType { get; set; }
        public int GroupColumns { get; set; } = 4;
    }
}
