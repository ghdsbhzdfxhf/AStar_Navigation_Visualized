using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanAStar.Models
{
    class AStarManhattan
    {

        public static (int, object) A_STAR((int, int) start, (int, int) destination, int[,] grid)
        {
            List<(int, int)> openList = new List<(int, int)> { start };
            Dictionary<(int, int), int> visited = new Dictionary<(int, int), int> { { start, 0 } };
            Dictionary<(int, int), (int, int)?> parent = new Dictionary<(int, int), (int, int)?> { { start, null } };

            int nodeCount = 0;
            (int, int)[] directions = new (int, int)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

            while (openList.Count > 0)
            {
                (int, int) currentNode = openList[0];
                foreach (var node in openList)
                {
                    if (visited[node] + Manhattan(node, destination) < visited[currentNode] + Manhattan(currentNode, destination))
                    {
                        currentNode = node;
                    }
                }
                openList.Remove(currentNode);

                if (currentNode == destination)
                {
                    return (nodeCount, ReconstructPath(currentNode, parent));
                }

                foreach (var direction in directions)
                {
                    (int, int) newPosition = (currentNode.Item1 + direction.Item1, currentNode.Item2 + direction.Item2);

                    if (IsWithinBounds(newPosition, grid) && grid[newPosition.Item1, newPosition.Item2] != 1 && !visited.ContainsKey(newPosition))
                    {
                        int newCost = visited[currentNode] + 1;
                        if (!visited.ContainsKey(newPosition) || visited[newPosition] < newCost)
                        {
                            visited[newPosition] = newCost;
                            openList.Add(newPosition);
                            parent[newPosition] = currentNode;
                            nodeCount++;
                        }
                    }
                }
            }

            return (nodeCount, "Not Reachable");
        }

        public static int Manhattan((int, int) node, (int, int) destination)
        {
            return Math.Abs(node.Item1 - destination.Item1) + Math.Abs(node.Item2 - destination.Item2);
        }

        public static List<(int, int)> ReconstructPath((int, int) currentNode, Dictionary<(int, int), (int, int)?> parent)
        {
            List<(int, int)> path = new List<(int, int)>();

            while (currentNode != default)
            {
                path.Add(currentNode);
                currentNode = parent[currentNode] ?? default;
            }

            path.Reverse();
            return path;
        }

        public static bool IsWithinBounds((int, int) newPosition, int[,] grid)
        {
            int i = newPosition.Item1;
            int j = newPosition.Item2;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            return 0 <= i && i < rows && 0 <= j && j < cols;
        }

    }
}
