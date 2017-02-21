using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace XamarinLifeGameXAML
{
    public partial class XamarinLifeGameXAMLPage : ContentPage
    {
        private const int CellSize = 9;

        private Cell[] _cells;

        public XamarinLifeGameXAMLPage()
        {
            InitializeComponent();

            var RowDefinitions = new RowDefinition[CellSize];
            var RowCollections = new RowDefinitionCollection();
            for (var i = 0; i < CellSize; i++)
            {
                RowDefinitions[i] = new RowDefinition { Height = GridLength.Star};
                RowCollections.Add(RowDefinitions[i]);
            }

            var ColumnDefinitions = new ColumnDefinition[CellSize];
            var ColumnCollections = new ColumnDefinitionCollection();
            for (var i = 0; i < CellSize; i++)
            {
                ColumnDefinitions[i] = new ColumnDefinition { Width = GridLength.Star };
                ColumnCollections.Add(ColumnDefinitions[i]);
            }

            CellGrid.RowDefinitions = RowCollections;
            CellGrid.ColumnDefinitions = ColumnCollections;
            CellGrid.Padding = new Thickness(5, Device.OnPlatform(20, 0, 0), 5, 0);

            _cells = new Cell[CellSize * CellSize];
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += (sender, e) => CellClicked(sender, e);

            for (var i = 0; i < CellSize; i++)
            {
                for (var j = 0; j < CellSize; j++)
                {
                    var index = (j + i * CellSize);
                    _cells[index] = new Cell {
                        State = 0,
                        IndexX = i,
                        IndexY = j,
                        Index =  index,
                        Text = index.ToString(),
                        FontSize = 20,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        GestureRecognizers = { tgr }
                    };
                    CellGrid.Children.Add(_cells[index], i, j);
                }
            }
        }

        public void CellClicked(object sender, EventArgs e)
        {
            var vm = (LgViewModel) this.BindingContext;
            var cell = (Cell)sender;

            var index = cell.Index;

            Debug.WriteLine(index);
            _cells[index].ChangeState();
        }
    }
}
