namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(File.ReadAllText("INPUT.TXT"));

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

            File.WriteAllText("OUTPUT.TXT", current.ToString());
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
