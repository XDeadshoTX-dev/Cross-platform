namespace Lab3
{
    public class Program
    {
        static void Main(string[] args)
        {
            int N = 5;
            int x1 = 1, y1 = 1, x2 = 3, y2 = 1;
            Console.WriteLine(GetResult(N, x1, y1, x2, y2));
        }
        private static bool IsValid(int x, int y, int N)
        {
            return x >= 1 && x <= N && y >= 1 && y <= N;
        }
        public static int GetResult(int N, int x1, int y1, int x2, int y2)
        {
            int[,] moves = { {-2, -1}, {-1, -2}, {1, -2}, {2, -1}, {2, 1}, {1, 2}, {-1, 2}, {-2, 1}};

            Queue<(int, int, int)> queue = new Queue<(int, int, int)>();
            queue.Enqueue((x1, y1, 0));
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            visited.Add((x1, y1));

            while (queue.Count > 0)
            {
                var (x, y, movesCount) = queue.Dequeue();

                if (x == x2 && y == y2)
                    return movesCount;

                for (int i = 0; i < moves.GetLength(0); i++)
                {
                    int nx = x + moves[i, 0];
                    int ny = y + moves[i, 1];

                    if (IsValid(nx, ny, N) && !visited.Contains((nx, ny)))
                    {
                        visited.Add((nx, ny));
                        queue.Enqueue((nx, ny, movesCount + 1));
                    }
                }
            }

            return -1;
        }
    }
}
