using PacmanAStar;
using System.Linq.Expressions;
using Microsoft.Maui.Controls;

namespace PacmanAStar;

public partial class AStarResults : ContentPage
{
	public AStarResults(string data)
	{
		InitializeComponent();
        InitializeGrid(5); // Example size (you can change n to any number)

		//string catch_data = data;
		//result1.Text = catch_data;
	}

    private void InitializeGrid(int n)
    {
        for (int i = 0; i < n; i++)
        {
            MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var label = new Label
                {
                    Text = $"({i}, {j})",
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    BackgroundColor = Colors.LightGray,
                    //BorderColor = Colors.Black,
                    //CornerRadius = 0,
                    Padding = new Thickness(1),
                    //BackgroundColor = Colors.LightGray
                };
                MainGrid.Children.Add(label);
                Grid.SetRow(label, i);
                Grid.SetColumn(label, j);
            }
        }
    }
}



        
