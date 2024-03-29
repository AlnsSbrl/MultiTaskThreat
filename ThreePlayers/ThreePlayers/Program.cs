﻿using System;

namespace ThreePlayers
{

    internal class Program
    {
        delegate void Funciones();
        static readonly object l = new object();
        static string[] caracteresRandom = "|,/,-,\\".Split(',');
        static bool hasDisplayStopped = false;
        static bool hasGameStarted = false;
        static bool endGame = false;
        static int contador = 0;

        static void contar(int num)
        {
            contador += num;
            Console.SetCursorPosition(2, 10);
            Console.WriteLine("Contador: {0}".PadRight(Console.WindowWidth), contador);
        }
        static void Display()
        {
            Random r = new Random();
            while (!endGame)
            {
                Thread.Sleep(200);
                lock (l)
                {
                    {
                        Console.SetCursorPosition(10, 5);
                        Console.WriteLine(caracteresRandom[r.Next(0, caracteresRandom.Length)]);
                        if (hasDisplayStopped)
                        {
                            lock (l)
                            {
                                Monitor.Wait(l);
                            }
                        }
                    }
                }
            }
        }

        static void Player1()
        {
            Random random = new Random();
            int num;
            while (!endGame)
            {
                lock (l)
                {
                    num = random.Next(1, 11);
                    Console.SetCursorPosition(2, 2);
                    Console.Write(num.ToString().PadRight(Console.WindowWidth));
                    if ((num == 5 || num == 7) && !endGame)
                    {
                        if (hasDisplayStopped && hasGameStarted)
                        {
                            contar(5);
                        }
                        else
                        {
                            contar(1);
                            hasDisplayStopped = !hasDisplayStopped;
                        }
                    }
                    hasGameStarted = true;
                    if (contador >= 20)
                    {
                        endGame = true;
                    }
                }
                Thread.Sleep(random.Next(100, 100 * num));
            }
            lock (l)
            {
                Monitor.PulseAll(l);
            }
        }

        static void Player2()
        {
            Random random = new Random();
            int num;
            while (!endGame)
            {
                lock (l)
                {
                    num = random.Next(1, 11);
                    Console.SetCursorPosition(2, 6);
                    Console.Write(num.ToString().PadRight(Console.WindowWidth));
                    if ((num == 5 || num == 7) && !endGame)
                    {
                        if (!hasDisplayStopped && hasGameStarted)
                        {
                            contar(-5);
                        }
                        else
                        {
                            contar(-1);
                            hasDisplayStopped = !hasDisplayStopped;
                            Monitor.PulseAll(l);
                        }
                    }
                    hasGameStarted = true;
                    if (contador <= -20)
                    {
                        endGame = true;
                    }
                }
                Thread.Sleep(random.Next(100, 100 * num));
            }
        }

        static void Main(string[] args)
        {
            Console.Write("player 1");
            Console.SetCursorPosition(0, 5);
            Console.Write("player 2");
            Thread jugador1 = new Thread(Player1);
            Thread jugador2 = new Thread(Player2);
            Thread mostrar = new Thread(Display);
            //jugador1.IsBackground = true;
            //jugador2.IsBackground = true;
            //mostrar.IsBackground = true;
            jugador1.Start();
            jugador2.Start();
            mostrar.Start();
            lock (l)
            {
                while (!endGame)
                {
                    Monitor.Wait(l);
                }
            }
            Console.SetCursorPosition(0, 11);
            if (contador > 0)
            {
                Console.WriteLine("The winner is player 1");
            }
            else
            {
                Console.WriteLine("The winner is player 2");
            }
        }
    }
}