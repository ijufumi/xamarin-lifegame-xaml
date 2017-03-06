﻿using Xamarin.Forms;
using System.Diagnostics;

namespace XamarinLifeGameXAML.Logic
{
    public class Cell : Label
    {
        public static readonly BindableProperty IndexProperty = BindableProperty.Create("Index", typeof (int), typeof (Cell), 0, BindingMode.TwoWay, null, OnIndexPropertyChanged);
        public static readonly BindableProperty IndexXProperty = BindableProperty.Create("IndexX", typeof (int), typeof (Cell), 0, BindingMode.TwoWay, null, OnIndexXPropertyChanged);
        public static readonly BindableProperty IndexYProperty = BindableProperty.Create("IndexY", typeof (int), typeof (Cell), 0, BindingMode.TwoWay, null, OnIndexYPropertyChanged);
        public static readonly BindableProperty StateProperty = BindableProperty.Create("State", typeof (int), typeof (Cell), 1, BindingMode.TwoWay, null, OnStatePropertyChanged);

        private const int LiveValue = 1;
        private const int DeadValue = 0;

        public Cell()
        {
        }

        public int Index
        {
            get
            {
                return (int) GetValue(IndexProperty);
            }
            set
            {
                SetValue(IndexProperty, value);
            }
        }

        public int IndexY
        {
            get
            {
                return (int) GetValue(IndexYProperty);
            }
            set
            {
                SetValue(IndexYProperty, value);
            }
        }

        public int IndexX
        {
            get
            {
                return (int) GetValue(IndexXProperty);
            }
            set
            {
                SetValue(IndexXProperty, value);
            }
        }

        private int State
        {
            get
            {
                return (int) GetValue(StateProperty);
            }
            set
            {
                SetValue(StateProperty, value);
            }
        }

        public bool IsLive => State == LiveValue;

        public void ToLive()
        {
            State = LiveValue;
        }

        public void ToDead()
        {
            State = DeadValue;
        }

        public void ChangeState()
        {
            if (IsLive)
            {
                ToDead();
            }
            else
            {
                ToLive();
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
        private static void OnStatePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var cell = (Cell) bindable;
            if (cell.IsLive)
            {
                cell.BackgroundColor = Color.White;
                cell.TextColor = Color.Black;
            }
            else
            {
                cell.BackgroundColor = Color.Black;
                cell.TextColor = Color.White;
            }
        }
    }
}
