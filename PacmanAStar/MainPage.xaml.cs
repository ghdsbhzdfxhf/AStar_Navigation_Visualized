using Microsoft.Maui.Controls;
using PacmanAStar.Models;
using System.Diagnostics;

namespace PacmanAStar
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        // AStar variables
        public string start_node { get; set; }
        public string destination { get; set; }
        public int grid_size { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            start_node = StartNode.Text;
            destination = DestinationNode.Text;
            grid_size = Convert.ToInt16(GridSize.Text);

            //int[,] grid = new int[grid_size, grid_size];
            //for (int i = 0; i < grid_size; i++)
            //{
            //    for (int j = 0; j < grid_size; j++)
            //    {
            //        grid[i, j] = 0;
            //    }
            //}

            int[,] grid = { 
                { 0, 1, 0, 0, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0 } };

            //var results = AStarManhattan.A_STAR((Convert.ToInt32(start_node[0]), Convert.ToInt32(start_node[1])), (Convert.ToInt32(destination[0]), Convert.ToInt32(destination[1])), grid);
            var results = AStarManhattan.A_STAR((0, 0), (4, 4), grid);
            int node_count = results.Item1;
            List<(int, int)> path = results.Item2 as List<(int, int)>;

            string path_string = "";
            foreach (var item in path)
            {
                path_string += item;
            }
            
            Results.Text = Convert.ToString(node_count) + "\n" + path_string + "\n" + start_node;

            Navigation.PushAsync(new AStarResults(path_string));
        }
    }

}

