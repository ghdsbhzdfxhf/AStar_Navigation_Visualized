using PacmanAStar.Models;
using PacmanAStar;
using System.Diagnostics;

namespace PacmanAStar;
public partial class AStarResults : ContentPage
{


    public AStarResults(List<(int, int)> path, int node_count, int[,] grid, int grid_size, string[] start_node, int max_frontier)
    {
        InitializeComponent();


        Utilities.draw_grid(grid_size, main_grid, grid, path, start_node, node_count, main_layout, max_frontier);

        Utilities.average_time(main_layout, "M");
    }

}




