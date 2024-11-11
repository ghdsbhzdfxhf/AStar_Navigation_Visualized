namespace PacmanAStar;

public partial class Results2 : ContentPage
{
    public Results2(TimeSpan bfs_time, TimeSpan dfs_time, TimeSpan ids_time, (int, List<(int, int)>, int) result_bfs, (int, List<(int, int)>, int) result_dfs, (int, List<(int, int)>, int) result_ids, int grid_size, int[,] random_grid, string[] start_node)
    {
        InitializeComponent();


        draw_bfs(result_bfs, grid_size, random_grid, start_node, bfs_grid, bfs_stack, bfs_time);
        draw_dfs(result_dfs, grid_size, random_grid, start_node, dfs_grid, dfs_stack, dfs_time);
        draw_ids(result_bfs, grid_size, random_grid, start_node, ids_grid, ids_stack, ids_time);


    }

    public static void draw_bfs((int, List<(int, int)>, int) result, int grid_size, int[,] random_grid, string[] start_node, Grid grid, VerticalStackLayout stack, TimeSpan time)
    {
        Device.BeginInvokeOnMainThread(() =>
        {


            // bfs grid
            int node_count = result.Item1;
            List<(int, int)> path = result.Item2 as List<(int, int)>;
            int max_frontier = result.Item3;

            for (int i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    if (random_grid[i, j] == 0)
                    {
                        if ((i, j) == (int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())))
                        {
                            grid.Add(new BoxView { BackgroundColor = Colors.Aqua, }, j, i);
                            continue;
                        }
                        if (path.Contains((i, j)))
                        {
                            grid.Add(new BoxView { BackgroundColor = Colors.Violet, }, j, i);
                            continue;
                        }
                        grid.Add(new BoxView { BackgroundColor = Colors.White, }, j, i);
                    }
                    else
                    {
                        grid.Add(new BoxView { BackgroundColor = Colors.Black }, j, i);
                    }
                }

            }

            stack.Add(new Label
            {
                Margin = 10,
                Text = $"Node count: {node_count}{" ",10}" + $"Steps: {path.Count}{" ",10}" + $"Time: {time.ToString("ss\\.ffff")}s\n" + $"Max frontier: {max_frontier}"
            });
        });
    }

    public static void draw_dfs((int, List<(int, int)>, int) result, int grid_size, int[,] random_grid, string[] start_node, Grid grid, VerticalStackLayout stack, TimeSpan time)
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            // bfs grid
            int node_count = result.Item1;
            List<(int, int)> path = result.Item2 as List<(int, int)>;
            int max_frontier = result.Item3;

            for (int i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    if (random_grid[i, j] == 0)
                    {
                        if ((i, j) == (int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())))
                        {
                            grid.Add(new BoxView { BackgroundColor = Colors.Aqua, }, j, i);
                            continue;
                        }
                        if (path.Contains((i, j)))
                        {
                            grid.Add(new BoxView { BackgroundColor = Colors.Violet, }, j, i);
                            continue;
                        }
                        grid.Add(new BoxView { BackgroundColor = Colors.White, }, j, i);
                    }
                    else
                    {
                        grid.Add(new BoxView { BackgroundColor = Colors.Black }, j, i);
                    }
                }

            }

            stack.Add(new Label
            {
                Margin = 10,
                Text = $"Node count: {node_count}{" ",10}" + $"Steps: {path.Count}{" ",10}" + $"Time: {time.ToString("ss\\.ffff")}s\n" + $"Max frontier: {max_frontier}"
            });
        });
    }

    public static void draw_ids((int, List<(int, int)>, int) result, int grid_size, int[,] random_grid, string[] start_node, Grid grid, VerticalStackLayout stack, TimeSpan time)
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            // bfs grid
            int node_count = result.Item1;
            List<(int, int)> path = result.Item2 as List<(int, int)>;
            int max_frontier = result.Item3;

            for (int i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    if (random_grid[i, j] == 0)
                    {
                        if ((i, j) == (int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())))
                        {
                            grid.Add(new BoxView { BackgroundColor = Colors.Aqua, }, j, i);
                            continue;
                        }
                        if (path.Contains((i, j)))
                        {
                            grid.Add(new BoxView { BackgroundColor = Colors.Violet, }, j, i);
                            continue;
                        }
                        grid.Add(new BoxView { BackgroundColor = Colors.White, }, j, i);
                    }
                    else
                    {
                        grid.Add(new BoxView { BackgroundColor = Colors.Black }, j, i);
                    }
                }

            }

            stack.Add(new Label
            {
                Margin = 10,
                Text = $"Node count: {node_count}{" ",10}" + $"Steps: {path.Count}{" ",10}" + $"Time: {time.ToString("ss\\.ffff")}s\n" + $"Max frontier: {max_frontier}"
            });
        });
    }

}