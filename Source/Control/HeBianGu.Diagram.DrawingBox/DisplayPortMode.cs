// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control




using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Diagram.DrawingBox
{
    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum DisplayPortMode
    {
        [Display(Name = "始终")]
        Always = 0,
        [Display(Name = "悬停")]
        MouseOver,
        [Display(Name = "选中")]
        Selected
    }
}
