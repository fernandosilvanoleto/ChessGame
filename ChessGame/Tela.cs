using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Tabuleiro;

namespace ChessGame
{
    class Tela
    {
        public static void imprimirTabuleiro(TabuleiroChess tabuleiro)
        {
            for(int i=0; i<tabuleiro.Linhas; i++)
            {
                for(int j=0; j<tabuleiro.Colunas; j++)
                {
                    if(tabuleiro.pecaPosicao(i, j) == null)
                    {
                        Console.Write(" - ");
                    }
                    else
                    {
                        Console.Write(tabuleiro.pecaPosicao(i, j) + " ");
                    }                   
                }
                Console.WriteLine();
            }
        }
    }
}
