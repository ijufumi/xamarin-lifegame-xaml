using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform;

namespace XamarinLifeGameXAML
{
    public class Cell : Label
    {
        public static readonly BindableProperty IndexProperty = BindableProperty.Create("Index", typeof (int), typeof (Cell), (object) null, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate) null, new BindableProperty.BindingPropertyChangedDelegate(Cell.OnIndexPropertyChanged), (BindableProperty.BindingPropertyChangingDelegate) null, (BindableProperty.CoerceValueDelegate) null, (BindableProperty.CreateDefaultValueDelegate) null);

        public Cell()
        {
        }

        public int Index
        {
            get
            {
                return (int) this.GetValue(Cell.IndexProperty);
            }
            set
            {
                this.SetValue(Cell.IndexProperty, (object) value);
            }
        }

        public int IndexY
        {
            get;
            set;
        }

        public int IndexX
        {
            get;
            set;
        }

        private int state;
        public int State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
                if (this.state == 1)
                {
                    BackgroundColor = Color.Black;
                }
                else
                {
                    BackgroundColor = Color.White;
                }
            }
        }

        private static void OnIndexPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
//            var cell = (Cell) bindable;
//            LineBreakMode lineBreakMode = cell.LineBreakMode;
//            if ((uint) (cell.Constraint & LayoutConstraint.VerticallyFixed) <= 0U || (lineBreakMode == LineBreakMode.CharacterWrap || lineBreakMode == LineBreakMode.WordWrap))
//                ((VisualElement) bindable).InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);
//            if (newvalue == null)
//                return;
        }
    }
}
