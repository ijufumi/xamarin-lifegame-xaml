using System;
using System.Diagnostics;
using Xamarin.Forms;
using XamarinLifeGameXAML.Logic;
using XamarinLifeGameXAML.ViewModels;

namespace XamarinLifeGameXAML.Views
{
    public partial class LifeGame : ContentPage
    {
        private readonly Logic.Cell[] _cells;
        public LifeGame()
        {
            InitializeComponent();


            var rowDefinitions = new RowDefinition[CellUtils.CellSize];
            var rowCollections = new RowDefinitionCollection();
            for (var i = 0; i < CellUtils.CellSize; i++)
            {
                rowDefinitions[i] = new RowDefinition { Height = GridLength.Star};
                rowCollections.Add(rowDefinitions[i]);
            }

            var columnDefinitions = new ColumnDefinition[CellUtils.CellSize];
            var columnCollections = new ColumnDefinitionCollection();
            for (var i = 0; i < CellUtils.CellSize; i++)
            {
                columnDefinitions[i] = new ColumnDefinition { Width = GridLength.Star };
                columnCollections.Add(columnDefinitions[i]);
            }

            CellGrid.RowDefinitions = rowCollections;
            CellGrid.ColumnDefinitions = columnCollections;
            CellGrid.Padding = new Thickness(5, Device.OnPlatform(20, 0, 0), 5, 0);

            _cells = new Logic.Cell[CellUtils.CellSize * CellUtils.CellSize];
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += CellClicked;

            for (var i = 0; i < CellUtils.CellSize; i++)
            {
                for (var j = 0; j < CellUtils.CellSize; j++)
                {
                    var index = (j + i * CellUtils.CellSize);
                    _cells[index] = new Logic.Cell {
                        IndexX = i,
                        IndexY = j,
                        Index =  index,
                        FontSize = 20,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        GestureRecognizers = { tgr }
                    };
                    _cells[index].ToDead();
                    CellGrid.Children.Add(_cells[index], i, j);
                }
            }

            var viewModel = (LifeGameViewModel) BindingContext;
            viewModel.Cells = _cells;
        }

        public void CellClicked(object sender, EventArgs e)
        {
            var cell = (Logic.Cell)sender;

            var index = cell.Index;
            _cells[index].ChangeState();
        }
    }
}
