namespace BecerroDelegado
{
    //public static void view(int grade)
    //{
    //    Console.ForegroundColor = grade >= 5 ? ConsoleColor.Green : ConsoleColor.Red;
    //    Console.WriteLine($"Student grade: {grade,3}.");
    //    Console.ResetColor();
    //}
    //public static bool pass(int num)
    //{
    //    return num >= 5;
    //}
    class Program
    {

        static void Main(string[] args)
        {
            int[] v = { 2, 2, 6, 7, 1, 10, 3 };
            Array.ForEach(v, v =>
            {
                Console.ForegroundColor = v >= 5 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"Student grade: {v,3}");
            });
            Console.ForegroundColor= ConsoleColor.White;
            int res = Array.FindIndex(v, v => v >= 5);
            Console.WriteLine($"The first passing student is number {res + 1} in the list.");
            bool hasAnyonePassed= Array.Exists(v,v=>v>=5);
            if (hasAnyonePassed)
            {
                Console.WriteLine("Some guy passed the test");
            }
            else
            {
                Console.WriteLine("Nobody passed the test");
            }
            int lastStudent = Array.FindLastIndex(v,v=>v>=5);
            
            Console.WriteLine($"The last student that passed the test is the {lastStudent+1}th one");
            Array.ForEach(v, v => Console.WriteLine($"The inverse grade is: {1.0/v,3}"));
            Console.ReadKey();
        }
    }

}