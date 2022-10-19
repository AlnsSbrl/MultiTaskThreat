#define DEBUG
using System.Security.Cryptography.X509Certificates;

namespace CarreraDeCaballos
{

    internal class Program
    {
        static readonly private object l = new object();
        static bool noHayGanador = true;
        public static void ClearCurrentConsoleLine(Caballo horse)
        {
            //POR ALGUN MOTIVO NO ME COLOCA BIEN EL Nº DEL CABALLO          
            Console.SetCursorPosition(0, horse.PosY);
            Console.Write("".PadRight(Console.WindowWidth));
            Console.SetCursorPosition(0, horse.PosY);
            Console.Write(horse.PosY + 1 + ")");
           
        }
        static void CabalosCorrendo(Object horse)
        {
            CabalosCorrendo((Caballo)horse);
        }
        static void CabalosCorrendo(Caballo horse)
        {
            Random rand = new Random();
            do
            {
                lock (l)
                {
                    if (noHayGanador)
                    {
                        horse.Correr(rand.Next(1, 6));                       
                        ClearCurrentConsoleLine(horse);
                        horse.Posicion();
                        Console.Write("*");
                        if (horse.PosX >= 70)
                        {
                            horse.esGanador = true;
                            noHayGanador = false;
                        }
                    }
                }
                Thread.Sleep(rand.Next(100, 300));               
            } while (!horse.esGanador && noHayGanador);
            lock (l)
            {
                if (horse.esGanador)
                {
                    Monitor.Pulse(l);
                }
              
            }
        }
        static void Main(string[] args)
        {
            string resposta;
            int cabaloEscollido;
            bool existeCabalo;
            Caballo[] cabalo = new Caballo[5];
            do
            {
                noHayGanador = true;
                lock (l)
                {
                    Monitor.PulseAll(l);
                }
                Console.Clear();
                do
                {
                    Console.WriteLine("1)*\n" +
                        "2)*\n" +
                        "3)*\n" +
                        "4)*\n" +
                        "5)*\n");
                    Console.WriteLine("For which one of the following horses do you wanna bet on?");
                    existeCabalo = int.TryParse(Console.ReadLine(), out cabaloEscollido);
                    if (!existeCabalo)
                    {
                        Console.Clear();
                        Console.WriteLine("We could not understand your option, please try again");
                    }
                    else if (cabaloEscollido <= 0 || cabaloEscollido > 5)
                    {
                        Console.Clear();
                        Console.WriteLine("The horse must be one of the following");
                    }

                } while (!existeCabalo || cabaloEscollido <= 0 || cabaloEscollido > 5);
                Console.WriteLine(cabaloEscollido);
                Thread[] cabaloThread = new Thread[5];
                lock (l)
                {
                    for (int i = 0; i < cabaloThread.Length; i++)
                    {
                        cabalo[i] = new Caballo(3, i, i + 1);
                        cabaloThread[i] = new Thread(CabalosCorrendo);
                        cabaloThread[i].Start(cabalo[i]);
                    }
                    Console.Clear();
                    Console.SetCursorPosition(0, cabaloThread.Length + 1);
                    Console.WriteLine("Chosen horse: {0}", cabaloEscollido);
                    Monitor.Wait(l);
                }
                Console.SetCursorPosition(0, 8);
                for (int i = 0; i < cabalo.Length; i++)
                {
                    if (cabalo[i].esGanador)
                    {
                        Console.WriteLine($"The winner is Horse number {i + 1}");
                        if (i == cabaloEscollido - 1)
                        {
                            Console.WriteLine("Congrats, you're the winner!");
                        }
                        else
                        {
                            Console.WriteLine("What a pathetic loser");
                        }
                    }
                }
                Console.WriteLine("Do you wanna bet again? (Y/N)");
                resposta = Console.ReadLine();
            } while (resposta.ToUpper().StartsWith('Y'));
            Console.WriteLine("It's been a pleasure, please come back any other time!");
            
        }
    }
}