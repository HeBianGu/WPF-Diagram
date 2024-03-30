// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.Linq;
#if NET
#endif 
using System.Windows;
using System.Windows.Documents;

namespace HeBianGu.Diagram.Presenter
{
    public class LoadedAdornerBehavior : AdornerBehaviorBase
    {
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            if (AdornerType == null) return;

            if (AdornerVisual == null)
                AdornerVisual = AssociatedObject;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(AdornerVisual);
            if (layer == null) return;

            var adorners = layer.GetAdorners(AdornerVisual as UIElement)?.Where(l => l.GetType() == AdornerType);

            if (adorners != null)
            {
                foreach (var item in adorners)
                {
                    layer.Remove(item);
                }
            }

            if (IsUse)
            {
                var adorner = Activator.CreateInstance(AdornerType, AssociatedObject) as System.Windows.Documents.Adorner;
                if (adorner == null)
                    return;
                layer.Add(adorner);
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

    }
}



