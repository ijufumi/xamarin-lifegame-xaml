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

            Title.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));

            var rowDefinitions = new RowDefinition[CellUtils.CellSize];
            var rowCollections = new RowDefinitionCollection();
            for (var i = 0; i < CellUtils.CellSize; i++)
            {
                rowDefinitions[i] = new RowDefinition {Height = 40};
                rowCollections.Add(rowDefinitions[i]);
            }

            var columnDefinitions = new ColumnDefinition[CellUtils.CellSize];
            var columnCollections = new ColumnDefinitionCollection();
            for (var i = 0; i < CellUtils.CellSize; i++)
            {
                columnDefinitions[i] = new ColumnDefinition {Width = 40};
                columnCollections.Add(columnDefinitions[i]);
            }

            CellGrid.RowDefinitions = rowCollections;
            CellGrid.ColumnDefinitions = columnCollections;

            var viewModel = (LifeGameViewModel) BindingContext;
            _cells = new Logic.Cell[CellUtils.CellSize * CellUtils.CellSize];

            for (var i = 0; i < CellUtils.CellSize; i++)
            {
                for (var j = 0; j < CellUtils.CellSize; j++)
                {
                    var index = (j + i * CellUtils.CellSize);
                    _cells[index] = new Logic.Cell
                    {
                        IndexX = i,
                        IndexY = j,
                        Index = index,
                        MinimumHeightRequest = 20,
                        MinimumWidthRequest = 20,
                        // Text = index.ToString(),
                        FontSize = 20,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                    };

                    _cells[index]
                        .GestureRecognizers.Add(new TapGestureRecognizer
                        {
                            Command = viewModel.CellClick,
                            CommandParameter = index
                        });
                    _cells[index].ToDead();

                    CellGrid.Children.Add(_cells[index], i, j);
                }
            }

            viewModel.Cells = _cells;
        }

        public void CellClicked(object sender, EventArgs e)
        {
            var cell = (Logic.Cell) sender;

            var index = cell.Index;
            _cells[index].ChangeState();
        }
    }
}