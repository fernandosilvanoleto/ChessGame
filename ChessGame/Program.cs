﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Tabuleiro;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Posicao P;
            P = new Posicao(3, 4);
            Console.WriteLine("Posição: " + P);
            Console.WriteLine();
            */

            TabuleiroChess tabuleiro = new TabuleiroChess(8, 8);

            Console.WriteLine();

        }
    }
}
