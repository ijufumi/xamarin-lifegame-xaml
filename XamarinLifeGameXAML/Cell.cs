using System;
using Xamarin.Forms;

namespace XamarinLifeGameXAML
{
    public class Cell : Label
    {
        public Cell()
        {
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
    }
}
