using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarreraDeCaballos
{
    internal class Caballo

    {
        public int PosX;
        public int PosY;
        public bool esGanador;
        public int num;
        public void Correr(int casillas)
        {
            PosX+=casillas;
        }

        public void Posicion()
        {
            Console.SetCursorPosition(PosX, PosY);
        }

        public Caballo(int PosX, int PosY, int num)
        {
            this.PosX = PosX;
            this.PosY = PosY;
            this.num = num;
            esGanador = false;
        }
    }
}
