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
        static private bool isPaused;
        //bool inicializado = false;
        
        Delegado delegado;

        public MyTimer(Delegado funcion)
        {          
            delegado = new Delegado(funcion);
        }

        public void Run()
        {
            isPaused = false;
            Thread threadFuncion = new Thread(Execution);
            threadFuncion.Start();
            //no creo que se busque que funcione así
            //basicamente mi codigo al pausar el hilo de la funcion (que se ejecuta en bucle) TERMINA
            //y al ejecutar otra vez Run() lanzo otro hilo que empieza de cero a ejecutarse en bucle
            //la funcionalidad acaba siendo la misma, pero creo que se busca jugar más
            //con Wait(l) y Pulse(l)
        }

        public void Pause()
        {         
            isPaused = true;
        }

        public void Execution()
        {
            do
            {
                lock (l)
                {
                    delegado.Invoke();
                    //if (isPaused)
                    //{
                    //    Monitor.Wait(l);
                    //}
                }
                Thread.Sleep(interval);
            } while (!isPaused);
        }

    }
}
