using PacmanAStar.Models;
using System.Diagnostics;

namespace PacmanAStar
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        // AStar variables
        public string[] start_node { get; set; }
        public string[] destination { get; set; }
        public int grid_size { get; set; }
        public int[,] random_grid { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void A_Start_Manhattan(object sender, EventArgs e)
        {
            
            var results = AStar.A_STAR((int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())), (int.Parse(destination[0].ToString()), int.Parse(destination[1].ToString())), random_grid);
            int node_count = results.Item1;
            List<(int, int)> path = results.Item2 as List<(int, int)>;
            int max_frontier = results.Item3;

            Navigation.PushAsync(new AStarResults(path, node_count, random_grid, grid_size, start_node, max_frontier));
        }
        
        private void A_Start_Equlidean(object sender, EventArgs e)
        {
            var results = AStar.A_STAR_E((int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())), (int.Parse(destination[0].ToString()), int.Parse(destination[1].ToString())), random_grid);
            int node_count = results.Item1;
            List<(int, int)> path = results.Item2 as List<(int, int)>;
            int max_frontier = results.Item3;

            Navigation.PushAsync(new AStarEResults(path, node_count, random_grid, grid_size, start_node, max_frontier));
        }

        private void GridGenerate_Clicked(object sender, EventArgs e)
        {
            start_node = StartNode.Text.Split();
            destination = DestinationNode.Text.Split();
            grid_size = Convert.ToInt16(GridSize.Text);

            random_grid = Utilities.RandomGrid(grid_size, grid_size, (int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())), (int.Parse(destination[0].ToString()), int.Parse(destination[1].ToString())));

        }

        private void Results_clicked(object sender, EventArgs e)
        {
            DateTime time;

            time = DateTime.Now;
            var m_results = AStar.A_STAR((int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())), (int.Parse(destination[0].ToString()), int.Parse(destination[1].ToString())), random_grid);
            TimeSpan duration_m = time - DateTime.Now;

            time = DateTime.Now;
            var e_results = AStar.A_STAR_E((int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())), (int.Parse(destination[0].ToString()), int.Parse(destination[1].ToString())), random_grid);
            TimeSpan duration_e = time - DateTime.Now;

            Navigation.PushAsync(new Results(e_results, m_results, random_grid, grid_size, start_node, duration_m, duration_e));
        }

        private async void Results2_Clicked(object sender, EventArgs e)
        {
            DateTime time;

            time= DateTime.Now;
            var result_bfs = algs.BFS((int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())), (int.Parse(destination[0].ToString()), int.Parse(destination[1].ToString())), random_grid);
            TimeSpan bfs_time = time - DateTime.Now;

            time = DateTime.Now;
            var result_dfs = algs.DFS((int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())), (int.Parse(destination[0].ToString()), int.Parse(destination[1].ToString())), random_grid);
            TimeSpan dfs_time = time - DateTime.Now;

            time = DateTime.Now;
            var result_ids = algs.IDS((int.Parse(start_node[0].ToString()), int.Parse(start_node[1].ToString())), (int.Parse(destination[0].ToString()), int.Parse(destination[1].ToString())), random_grid);
            TimeSpan ids_time = time - DateTime.Now;

            await Navigation.PushAsync(new Results2(bfs_time, dfs_time, ids_time, result_bfs, result_dfs, result_ids, grid_size, random_grid, start_node));
        }
    }

}

