namespace Lab3
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] lines = File.ReadAllLines("Lab3/INPUT.TXT");
                Console.Write($"[INPUT.TXT] ");

                foreach (var line in lines)
                {
                    if (lines.Length <= 5)
                    {
                        Console.Write($"{line} ");
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                }
                Console.WriteLine();
                string[] data = lines[0].Split(' ');

                int N, x1, y1, x2, y2;

                if (!int.TryParse(data[0], out N) ||
                    !int.TryParse(data[1], out x1) ||
                    !int.TryParse(data[2], out y1) ||
                    !int.TryParse(data[3], out x2) ||
                    !int.TryParse(data[4], out y2))
                {
                    throw new ArgumentException("All values must be integers");
                }

                if (data.Length != 5) throw new ArgumentException("Input file must contain 5 lines");

                int result = GetResult(N, x1, y1, x2, y2);

                Console.WriteLine($"[OUTPUT.TXT] {result}");

                File.WriteAllText("OUTPUT.TXT", result.ToString());
            }
            catch (Exception ex) 
            { 
                Console.WriteLine("[Error] " + ex.Message); 
            }
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
