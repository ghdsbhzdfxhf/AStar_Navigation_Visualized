using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanAStar.Models
{
    class algs
    {
        public static (int, List<(int, int)>, int) BFS((int, int) start, (int, int) destination, int[,] grid)
        {
            var queue = new Queue<(int, int)>();
            queue.Enqueue(start);
            var visited = new HashSet<(int, int)> { start };
            var parent = new Dictionary<(int, int), (int, int)?> { { start, default } };

            var directions = new (int, int)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
            int max_frontier = 0;
            int nodeCount = 0;

            while (queue.Count > 0)
            {
                max_frontier += queue.Count;

                var currentNode = queue.Dequeue();

                if (currentNode == destination)
                {
                    return (nodeCount, PathConstruct(currentNode, parent), max_frontier);
                }

                foreach (var direction in directions)
                {
                    var newPosition = (currentNode.Item1 + direction.Item1, currentNode.Item2 + direction.Item2);

                    if (IsWithinBounds(newPosition, grid) && grid[newPosition.Item1,newPosition.Item2] != 1 && !visited.Contains(newPosition))
                    {
                        visited.Add(newPosition);
                        queue.Enqueue(newPosition);
                        parent[newPosition] = currentNode;
                        nodeCount++;
                    }
                }
            }

            throw new InvalidOperationException("Not reachable");
        }

        public static (int, List<(int, int)>, int) DFS((int, int) start, (int, int) destination, int[,] grid)
        {
            var stack = new Stack<(int, int)>();
            stack.Push(start);
            var parent = new Dictionary<(int, int), (int, int)?>();
            parent[start] = default;
            var directions = new (int, int)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };

            int maxFrontier = 0;
            int nodeCount = 0;

            while (stack.Count > 0)
            {
                maxFrontier += stack.Count;
                var currentNode = stack.Pop();

                if (currentNode == destination)
                {
                    return (nodeCount, PathConstruct(currentNode, parent), maxFrontier);
                }

                foreach (var direction in directions)
                {
                    var newPosition = (currentNode.Item1 + direction.Item1, currentNode.Item2 + direction.Item2);

                    if (IsWithinBounds(newPosition, grid) && !PathConstruct(currentNode, parent).Contains(newPosition) && grid[newPosition.Item1, newPosition.Item2] != 1)
                    {
                        stack.Push(newPosition);
                        parent[newPosition] = currentNode;
                        nodeCount++;
                    }
                }
            }

            throw new Exception("Not reachable");
        }

        public static (int, List<(int, int)>, int) IDS((int, int) start, (int, int) destination, int[,] grid)
        {
            int maxDepth = grid.GetLength(0) * grid.GetLength(0);

            for (int depth = 1; depth < maxDepth; depth++)
            {
                var stack = new Stack<(int, int)>();
                stack.Push(start);
                var parent = new Dictionary<(int, int), (int, int)?> { { start, null } };
                var directions = new (int, int)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };

                int maxFrontier = 0;
                int nodeCount = 0;

                while (stack.Count > 0)
                {
                    maxFrontier = Math.Max(maxFrontier, stack.Count);
                    var currentNode = stack.Pop();

                    if (currentNode == destination)
                    {
                        return (nodeCount, PathConstruct(currentNode, parent), maxFrontier);
                    }

                    if (PathConstruct(currentNode, parent).Count < depth)
                    {
                        foreach (var direction in directions)
                        {
                            var newPosition = (currentNode.Item1 + direction.Item1, currentNode.Item2 + direction.Item2);

                            if (IsWithinBounds(newPosition, grid) &&
                                !PathConstruct(currentNode, parent).Contains(newPosition) &&
                                grid[newPosition.Item1,newPosition.Item2] != 1)
                            {
                                stack.Push(newPosition);
                                parent[newPosition] = currentNode;
                                nodeCount++;
                            }
                        }
                    }
                }
            }

            throw new InvalidOperationException("Not reachable");
        }

        private static List<(int, int)> PathConstruct((int, int) currentNode, Dictionary<(int, int), (int, int)?> parent)
        {
            var path = new List<(int, int)>();

            while (currentNode != default)
            {
                path.Add(currentNode);
                currentNode = parent[currentNode] ?? default;
            }

            path.Reverse();
            return path;
        }

        private static bool IsWithinBounds((int, int) newPosition, int[,] grid)
        {
            int i = newPosition.Item1;
            int j = newPosition.Item2;
            int rows = grid.GetLength(0);
            int cols = rows > 0 ? grid.GetLength(0) : 0;
            return i >= 0 && i < rows && j >= 0 && j < cols;
        }
    }
}
