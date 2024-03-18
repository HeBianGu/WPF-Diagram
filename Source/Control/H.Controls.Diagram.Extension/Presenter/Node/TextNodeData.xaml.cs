﻿



using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace H.Controls.Diagram.Extension
{
    public class TextNodeData : SystemNodeData
    {
        public TextNodeData()
        {
            this.Text = this.Name;
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            //this.FontSize = (double)Application.Current.FindResource(FontSizeKeys.Default);
            //this.FontFamily = Application.Current.FindResource(LayoutKeys.FontFamily) as FontFamily;
            this.FontStyle = FontStyles.Normal;
            this.FontWeight = FontWeights.Normal;
            this.FontStretch = FontStretches.Normal;
        }

        private string _text;
        [Display(Name = "文本", GroupName = "常用")]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }

        private double _fontSize;
        [Display(Name = "字号", GroupName = "常用")]
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                RaisePropertyChanged();
            }
        }

        private FontFamily _fontFamily;
        [Display(Name = "字体", GroupName = "文本")]
        public FontFamily FontFamily
        {
            get { return _fontFamily; }
            set
            {
                _fontFamily = value;
                RaisePropertyChanged();
            }
        }


        private FontStyle _fontStyle;
        [Display(Name = "字体样式", GroupName = "文本")]
        public FontStyle FontStyle
        {
            get { return _fontStyle; }
            set
            {
                _fontStyle = value;
                RaisePropertyChanged();
            }
        }

        private FontWeight _fontWeight;
        [Display(Name = "字体加粗", GroupName = "文本")]
        public FontWeight FontWeight
        {
            get { return _fontWeight; }
            set
            {
                _fontWeight = value;
                RaisePropertyChanged();
            }
        }

        private FontStretch _fontStretch;
        [Display(Name = "字体展开", GroupName = "文本")]
        public FontStretch FontStretch
        {
            get { return _fontStretch; }
            set
            {
                _fontStretch = value;
                RaisePropertyChanged();
            }
        }

        private Brush _foreground;
        [DefaultValue(null)]
        [Display(Name = "文本颜色", GroupName = "常用")]
        public Brush Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                RaisePropertyChanged();
            }
        }

        private Thickness _textMargin = new Thickness(0);
        public Thickness TextMargin
        {
            get { return _textMargin; }
            set
            {
                _textMargin = value;
                RaisePropertyChanged();
            }
        }

        public override void ApplayStyleTo(INodeData to)
        {
            base.ApplayStyleTo(to);

            if (to is TextNodeData textNodeData)
            {
                textNodeData.Foreground = this.Foreground;
                textNodeData.FontFamily = this.FontFamily;
                textNodeData.FontSize = this.FontSize;
                textNodeData.FontStretch = this.FontStretch;
                textNodeData.FontStyle = this.FontStyle;
                textNodeData.FontWeight = this.FontWeight;
            }
        }
    }
}
