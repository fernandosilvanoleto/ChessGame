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

                while(!partida.Terminada) {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirPartida(partida);                        

                        Console.WriteLine();

                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem, destino);

                        partida.realizaJogada(origem, destino);
                    }
                    catch (TabuleiroChessException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.imprimirPartida(partida);
            }
            catch (TabuleiroChessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
