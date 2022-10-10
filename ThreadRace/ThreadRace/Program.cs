namespace ThreadRace
{
    internal class Program
    {
        static readonly private object candado = new object();
        static void Main(string[] args)
        {
            int suma = 0;


            bool parar = false;
            //dos hilos que compiten entre si, sumandose a si mismos, restando al otro
            Thread thread1 = new Thread(() =>
            {
                while (!parar)
                {
                    lock (candado)
                    {

                        if (!parar)
                        {

                            ++suma;
                            Console.WriteLine($"\tSuma: {suma}");
                        }
                        if (suma >= 1000)
                        {
                            parar = true;
                        }
                    }
                }
            });
            Thread thread2 = new Thread(() =>
            {
                while (!parar)
                {
                    lock (candado)
                    {

                        if (!parar)
                        {

                            --suma;
                            Console.WriteLine($"\tSuma: {suma}");
                        }
                        if (suma <= -1000) { parar = true; }
                    }
                }
            });
            thread1.Start();
            thread2.Start();
            Console.ReadKey();
        }
    }
}