namespace PacmanAStar;

public partial class Results : ContentPage
{
	public Results((int, object, int) e_results, (int, object, int) m_results, int[,] grid, int grid_size, string[] start_node, TimeSpan time_e, TimeSpan time_m)
	{
		InitializeComponent();

        draw_grid_e(grid_size, e_grid, grid, e_results, start_node, e_layout, time_e);
        draw_grid_m(grid_size, m_grid, grid, m_results, start_node, m_layout, time_m);

    }

    public static void draw_grid_e(int n, Grid app_grid, int[,] grid, (int,object,int) e_results, string[] start_node, VerticalStackLayout e_layout, TimeSpan time_e)
    {
        app_grid.Clear();

        List<(int,int)> path = e_results.Item2 as List<(int,int)>;
        int node_count = e_results.Item1;
        int max_frontier = e_results.Item3;

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
                    if (path.Contains((i, j)))
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

        e_layout.Add(new Label
        {
            Margin = 10,
            Text = $"Node count: {node_count}{" ",10}" + $"Steps: {path.Count}{" ",10}" + $"Time: {time_e.ToString("ss\\.ffff")}s\n" + $"Max frontier: {max_frontier}"
        });

    }

    public static void draw_grid_m(int n, Grid app_grid, int[,] grid, (int, object, int) m_results, string[] start_node, VerticalStackLayout m_layout, TimeSpan time_m)
    {
        List<(int, int)> path = m_results.Item2 as List<(int, int)>;
        int node_count = m_results.Item1;
        int max_frontier = m_results.Item3;

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
                    if (path.Contains((i, j)))
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

        m_layout.Add(new Label
        {
            Margin = 10,
            Text = $"Node count: {node_count}{" ",10}" + $"Steps: {path.Count}{" ",10}" + $"Time: {time_m.ToString("ss\\.ffff")}s\n" + $"Max frontier: {max_frontier}"
        });

        //BoxView boxView = new BoxView
        //{
        //    BackgroundColor = Colors.White,
        //};
        //app_grid.SetColumnSpan(boxView, n);
        //app_grid.SetRow(boxView, n + 2);

        //Label label = new Label
        //{
        //    TextColor = Colors.Black,
        //    Text = $"Node count: {node_count}{" ", 10}" +
        //    $"Steps: {path.Count}",
        //    FontSize = 20
        //};
        //app_grid.SetColumnSpan(label, n);
        //app_grid.SetRow(label, n + 2);

        //app_grid.Add(boxView);
        //app_grid.Add(label);
    }

}