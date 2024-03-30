// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control




using System.Windows.Controls;

namespace HeBianGu.Diagram.DrawingBox
{
    public class RemoveNodeCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is Node node)
                node.Delete();

            if (parameter is ContextMenu menu)
            {
                menu.PlacementTarget.GetParent<Node>()?.Delete();
            }
        }
    }
}
