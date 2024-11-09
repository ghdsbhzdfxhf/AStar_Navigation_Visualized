using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanAStar.Models
{
    class AStar
    {

        public static (int, List<(int, int)>, int) A_STAR_E((int, int) start, (int, int) destination, int[,] grid)
        {
            List<(int, int)> open_list = new List<(int, int)> { start };
            Dictionary<(int, int), double> visited = new Dictionary<(int, int), double> { { start, 0 } };
            Dictionary<(int, int), (int, int)?> parent = new Dictionary<(int, int), (int, int)?> { { start, null } };

            int max_frontier = 0;
            int node_count = 0;
            List<(int, int)> directions = new List<(int, int)>
        {
            (0, 1),
            (1, 0),
            (0, -1),
            (-1, 0)
        };

            while (open_list.Count > 0)
            {
                max_frontier = Math.Max(max_frontier, open_list.Count);
                var current_node = open_list.OrderBy(node => visited[node] + equlidean(node, destination)).First();
                open_list.Remove(current_node);

                if (current_node.Equals(destination))
                {
                    return (node_count, ReconstructPath(current_node, parent), max_frontier);
                }

                foreach (var direction in directions)
                {
                    var new_position = (current_node.Item1 + direction.Item1, current_node.Item2 + direction.Item2);

                    if (IsWithinBounds(new_position, grid)
                        && grid[new_position.Item1, new_position.Item2] != 1
                        && !visited.ContainsKey(new_position))
                    {
                        double new_cost = visited[current_node] + 1;
                        if (!visited.ContainsKey(new_position) || visited[new_position] < new_cost)
                        {
                            visited[new_position] = new_cost;
                            open_list.Add(new_position);
                            parent[new_position] = current_node;
                            node_count += 1;
                        }
                    }
                }
            }

            return (node_count, null, max_frontier); // "Not Reachable"
        }

        public static double equlidean((int, int) node, (int, int) destination)
        {
            return Math.Sqrt(Math.Pow(destination.Item1 - node.Item1, 2) + Math.Pow(destination.Item2 - node.Item2, 2));
        }

        public static (int, object, int) A_STAR((int, int) start, (int, int) destination, int[,] grid)
        {
            List<(int, int)> openList = new List<(int, int)> { start };
            Dictionary<(int, int), int> visited = new Dictionary<(int, int), int> { { start, 0 } };
            Dictionary<(int, int), (int, int)?> parent = new Dictionary<(int, int), (int, int)?> { { start, null } };

            int max_frontier = 0;
            int nodeCount = 0;
            (int, int)[] directions = { (0, 1), (1, 0), (0, -1), (-1, 0) };

            while (openList.Count > 0)
            {
                max_frontier = Math.Max(max_frontier, openList.Count);
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
                    return (nodeCount, ReconstructPath(currentNode, parent), max_frontier);
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

            return (nodeCount, "Not Reachable", max_frontier);
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
