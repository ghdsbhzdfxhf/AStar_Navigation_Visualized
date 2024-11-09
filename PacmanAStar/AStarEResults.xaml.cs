namespace PacmanAStar;

using PacmanAStar.Models;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

public partial class AStarEResults : ContentPage
{
	public AStarEResults(List<(int, int)> path, int node_count, int[,] grid, int grid_size, string[] start_node, int max_frontier)
	{
		InitializeComponent();

        Utilities.draw_grid(grid_size, e_grid, grid, path, start_node, node_count, main_layout, max_frontier);

        Utilities.average_time(main_layout, "E");
    }

    
}