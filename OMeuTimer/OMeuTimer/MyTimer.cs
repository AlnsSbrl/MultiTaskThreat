using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMeuTimer
{
    public delegate void Delegado();
    internal class MyTimer
    {
        static readonly private object l = new object();
        public int interval;
        static private bool isPaused = false;
        Thread threadFuncion;
        static private bool hasStarted = false;
        Delegado delegado;

        public MyTimer(Delegado funcion)
        {
            delegado = new Delegado(funcion);
        }

        public void Run()
        {
            lock (l)
            {
                if (!hasStarted)
                {
                    hasStarted = true;
                    threadFuncion = new Thread(Execution);
                    threadFuncion.IsBackground = true;
                    threadFuncion.Start();
                }
                else
                {
                    isPaused = false;
                    Monitor.Pulse(l);
                }
            }
        }

        public void Pause()
        {
            lock (l)
            {
                isPaused = true;
            }
        }

        public void Execution()
        {
            while (!isPaused)
            {
                delegado.Invoke();
                Thread.Sleep(interval);          
                lock (l)
                {
                    if (isPaused)
                    {
                        Monitor.Wait(l);
                    }
                }
            }
        }
    }
}
