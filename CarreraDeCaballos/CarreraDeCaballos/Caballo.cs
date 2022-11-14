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
        public string jinete;
        private string[] listaJinetes="Diego Brando,Gyro Zeppeli,Johnny Joestar,Mountain Tim,Hot Pants,Sandman,Fritz Von Stroheim,Hol Horse".Split(',');
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
            Random rnd = new Random();
            jinete = listaJinetes[rnd.Next(listaJinetes.Length)];
        }
    }
}
