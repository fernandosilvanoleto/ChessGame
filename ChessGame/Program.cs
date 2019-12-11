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

                TabuleiroChess tabuleiro = new TabuleiroChess(8, 8);                

                Tela.imprimirTabuleiro(tabuleiro);

                Console.WriteLine();
                
                PosicaoXadrez pos = new PosicaoXadrez('c', 7);

                Console.WriteLine(pos.toPosicao());

                Console.WriteLine();
                */

                PartidaXadrez partida = new PartidaXadrez();

                Tela.imprimirTabuleiro(partida.tab);

            }
            catch (TabuleiroChessException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
