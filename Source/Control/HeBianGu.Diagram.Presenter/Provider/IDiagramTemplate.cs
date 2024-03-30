namespace HeBianGu.Diagram.Presenter
{
    public interface IDiagramTemplate
    {
        public string Name { get; set; }

        public string GroupName { get; set; }

        public string TypeName { get; set; }

        public IDiagram Diagram { get; set; }
    }
}
