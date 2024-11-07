namespace PacmanAStar;

using PacmanAStar.Models;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

public partial class AStarEResults : ContentPage
{
	public AStarEResults(List<(int, int)> path, int node_count, int[,] grid, int grid_size, string[] start_node, int max_frontier)
	{
		InitializeComponent();

        draw_grid_e(grid_size, e_grid, grid, path, start_node, node_count, main_layout, max_frontier);
        //avg_time(main_layout);
    }

    public static void draw_grid_e(int n, Grid app_grid, int[,] grid, List<(int, int)> path, string[] start_node, int node_count, HorizontalStackLayout main_layout, int max_frontier)
    {
        app_grid.Clear();

        var path_set = new HashSet<(int, int)>(path);

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i, j] == 0)
                {
                    if ((i, j) == (int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())))
                    {
                        app_grid.Add(new BoxView { BackgroundColor = Colors.Aqua, }, j, i);
                        continue;
                    }
                    if (path_set.Contains((i, j)))
                    {
                        app_grid.Add(new BoxView { BackgroundColor = Colors.Violet, }, j, i);
                        continue;
                    }
                    app_grid.Add(new BoxView { BackgroundColor = Colors.White, }, j, i);
                }
                else
                {
                    app_grid.Add(new BoxView { BackgroundColor = Colors.Black }, j, i);
                }
            }

        }

        string path1 = "";
        foreach (var p in path)
        {
            path1 += p;
        }

        List<long> times = new List<long>();
        int grid_size = 40;

        // AVERAGE TIME
        for (int i = 0; i < 100; i++)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var x = AStar.A_STAR_E((0, 0), (grid_size - 1, grid_size - 1), Utilities.RandomGrid(grid_size, grid_size, (0, 0), (grid_size - 1, grid_size - 1)));

            stopwatch.Stop();
            times.Add(stopwatch.ElapsedMilliseconds);
        }

        Label label = new Label();
        label.FontSize = 30;
        label.VerticalOptions = LayoutOptions.Center;
        label.Text = $"Node count: {node_count}{" ",10}\n" + $"Steps: {path.Count}\n" + $"Average time for 100, 20*20 grids: {times.Average()}s\n" + $"Max frontier: {max_frontier.ToString()}";
        main_layout.Add(label);

    }

    public static void avg_time(HorizontalStackLayout main_layout)
    {
        List<long> times = new List<long>();
        int grid_size = 20;


        for (int i = 0; i < 100; i++)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            var x = AStar.A_STAR_E((0, 0), (grid_size - 1, grid_size - 1), Utilities.RandomGrid(grid_size, grid_size, (0, 0), (grid_size - 1, grid_size - 1)));
            
            stopwatch.Stop();
            times.Add(stopwatch.ElapsedMilliseconds);
        }


    }
}