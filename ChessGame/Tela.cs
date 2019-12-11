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
                Console.Write(8 - i + " ");
                for(int j=0; j<tabuleiro.Colunas; j++)
                {
                    if(tabuleiro.pecaPosicao(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        //Console.Write(tabuleiro.pecaPosicao(i, j) + " ");
                        imprimirPeca(tabuleiro.pecaPosicao(i, j));
                        Console.Write(" ");
                    }                   
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirPeca(Peca peca)
        {
            if(peca.cor == Color.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }

    }
}
