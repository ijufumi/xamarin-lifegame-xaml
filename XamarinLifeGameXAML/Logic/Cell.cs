using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform;

namespace XamarinLifeGameXAML.Logic
{
    public class Cell : Label
    {
        public static readonly BindableProperty IndexProperty = BindableProperty.Create("Index", typeof (int), typeof (Cell), (object) 0, BindingMode.Default, (BindableProperty.ValidateValueDelegate) null, new BindableProperty.BindingPropertyChangedDelegate(Cell.OnIndexPropertyChanged), (BindableProperty.BindingPropertyChangingDelegate) null, (BindableProperty.CoerceValueDelegate) null, (BindableProperty.CreateDefaultValueDelegate) null);
        public static readonly BindableProperty IndexXProperty = BindableProperty.Create("IndexX", typeof (int), typeof (Cell), (object) 0, BindingMode.Default, (BindableProperty.ValidateValueDelegate) null, new BindableProperty.BindingPropertyChangedDelegate(Cell.OnIndexXPropertyChanged), (BindableProperty.BindingPropertyChangingDelegate) null, (BindableProperty.CoerceValueDelegate) null, (BindableProperty.CreateDefaultValueDelegate) null);
        public static readonly BindableProperty IndexYProperty = BindableProperty.Create("IndexY", typeof (int), typeof (Cell), (object) 0, BindingMode.Default, (BindableProperty.ValidateValueDelegate) null, new BindableProperty.BindingPropertyChangedDelegate(Cell.OnIndexYPropertyChanged), (BindableProperty.BindingPropertyChangingDelegate) null, (BindableProperty.CoerceValueDelegate) null, (BindableProperty.CreateDefaultValueDelegate) null);

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

        private int _state;
        public int State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                if (_state == 1)
                {
                    BackgroundColor = Color.Black;
                    TextColor = Color.White;
                }
                else
                {
                    BackgroundColor = Color.White;
                    TextColor = Color.Black;
                }
            }
        }

        public void ChangeState()
        {
            if (State == 0)
            {
                State = 1;
            }
            else
            {
                State = 0;
            }
        }

        private static void OnIndexPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
        }
        private static void OnIndexYPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
        }
        private static void OnIndexXPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
        }
    }
}
