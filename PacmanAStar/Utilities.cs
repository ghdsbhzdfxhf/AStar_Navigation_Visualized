using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanAStar
{
    class Utilities
    {

        public static int[,] RandomGrid(int gridRows, int gridCols, (int, int) start, (int, int) destination, double wallProbability = 0.3)
        {
            Random random = new Random();
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

                    if (newRow >= 0 && newRow < gridRows && newCol >= 0 && newCol < gridCols && grid[newRow, newCol] == 0 && !visited.Contains((newRow, newCol)))
                    {
                        queue.Enqueue((newRow, newCol));
                        visited.Add((newRow, newCol));
                    }
                }
            }

            return false;
        }

    }
}
