using PacmanAStar.Models;
using System.Diagnostics;

namespace PacmanAStar
{
    class Utilities
    {

        private static Random random = new Random();

        public static int[,] RandomGrid(int gridRows, int gridCols, (int, int) start, (int, int) destination, double wallProbability = 0.3)
        {
            while (true)
            {
                // Start with empty grid
                int[,] grid = new int[gridRows, gridCols];

                // Randomly add walls
                for (int i = 0; i < gridRows; i++)
                {
                    for (int j = 0; j < gridCols; j++)
                    {
                        if ((i, j) != start && (i, j) != destination)
                        {
                            if (random.NextDouble() < wallProbability)
                            {
                                grid[i, j] = 1;
                            }
                        }
                    }
                }

                // Check if there is a path from start to destination
                bool pathExists = CheckPathExists(start, destination, grid);
                if (pathExists)
                {
                    return grid;
                }
            }
        }

        private static bool CheckPathExists((int, int) start, (int, int) destination, int[,] grid)
        {
            Queue<(int, int)> queue = new Queue<(int, int)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            queue.Enqueue(start);
            visited.Add(start);
            (int, int)[] directions = { (-1, 0), (1, 0), (0, -1), (0, 1) };

            int gridRows = grid.GetLength(0);
            int gridCols = grid.GetLength(1);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current == destination)
                {
                    return true;
                }

                foreach (var direction in directions)
                {
                    int newRow = current.Item1 + direction.Item1;
                    int newCol = current.Item2 + direction.Item2;

                    if (newRow >= 0 && newRow < gridRows && newCol >= 0 && newCol < gridCols &&
                        grid[newRow, newCol] == 0 && !visited.Contains((newRow, newCol)))
                    {
                        queue.Enqueue((newRow, newCol));
                        visited.Add((newRow, newCol));
                    }
                }
            }

            return false;
        }


        public static void draw_grid(int n, Grid app_grid, int[,] grid, List<(int, int)> path, string[] start_node, int node_count, HorizontalStackLayout main_layout, int max_frontier)
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

            Label label = new Label();
            label.FontSize = 30;
            label.VerticalOptions = LayoutOptions.Center;
            label.Text = $"Node count: {node_count}{" ",10}\n" + $"Steps: {path.Count}\n" + $"Max frontier: {max_frontier.ToString()}";
            main_layout.Add(label);

        }

        public static void average_time(HorizontalStackLayout main_layout, string method_choice)
        {

            List<long> times = new List<long>();
            int grid_size = 40;

            // AVERAGE TIME
            for (int i = 0; i < 100; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                if (method_choice == "M")
                {
                    var x = AStar.A_STAR_E((0, 0), (grid_size - 1, grid_size - 1), Utilities.RandomGrid(grid_size, grid_size, (0, 0), (grid_size - 1, grid_size - 1)));
                }
                else if (method_choice == "E")
                {
                    var x = AStar.A_STAR((0, 0), (grid_size - 1, grid_size - 1), Utilities.RandomGrid(grid_size, grid_size, (0, 0), (grid_size - 1, grid_size - 1)));
                }
                stopwatch.Stop();
                times.Add(stopwatch.ElapsedMilliseconds);
            }
            Label label = new Label();
            label.Margin = 5;
            label.FontSize = 25;
            label.VerticalOptions = LayoutOptions.End;
            label.Text = $"Average time for 100, 20*20 grids: {times.Average()}s\n";
            main_layout.Add(label);
        }
    }
}
