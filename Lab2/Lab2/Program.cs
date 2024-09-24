namespace Lab2
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int n = int.Parse(File.ReadAllText("INPUT.TXT"));
                Console.WriteLine("[INPUT.TXT] OK");
                string result = GetResult(n).ToString();

                File.WriteAllText("OUTPUT.TXT", result);
                Console.WriteLine("[OUTPUT.TXT] OK");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
        }

        public static int GetResult(int n)
        {
            if (n > 0 && n <= 10000)
            {
                List<int> list = new List<int>() { 1 };

                int current = 0;

                for (int i = 0; i < n; i++)
                {
                    current = list[0];
                    list.RemoveAt(0);
                    AddToList(list, current * 2);
                    AddToList(list, current * 3);
                    AddToList(list, current * 5);

                    list.Sort();
                }
                return current;
            }
            else
            {
                throw new ArgumentException("The number must be 0 < n <= 10000");
            }
        }

        static void AddToList(List<int> list, int value)
        {
            if (!list.Contains(value))
            {
                list.Add(value);
            }
        }
    }
}
