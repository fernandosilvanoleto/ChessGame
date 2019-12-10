using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Tabuleiro;
using ChessGame.Xadrez;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /*
                Posicao P;
                P = new Posicao(3, 4);
                Console.WriteLine("Posição: " + P);
                Console.WriteLine();
                */

                TabuleiroChess tabuleiro = new TabuleiroChess(8, 8);

                tabuleiro.colocarPeca(new Torre(tabuleiro, Color.Preta), new Posicao(0, 0));
                tabuleiro.colocarPeca(new Torre(tabuleiro, Color.Preta), new Posicao(1, 3));
                tabuleiro.colocarPeca(new Rei(tabuleiro, Color.Preta), new Posicao(0, 2));
                tabuleiro.colocarPeca(new Rei(tabuleiro, Color.Preta), new Posicao(1, 7));

                Tela.imprimirTabuleiro(tabuleiro);

                Console.WriteLine();
            }
            catch (TabuleiroChessException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
