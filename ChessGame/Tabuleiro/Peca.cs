using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Color cor { get; protected set; }
        public int quantMovimentos { get; protected set; }
        public TabuleiroChess tabuleiro { get; protected set; }

        public Peca(TabuleiroChess tabuleiro, Color cor)
        {
            this.posicao = null;
            this.tabuleiro = tabuleiro;
            this.cor = cor;
            this.quantMovimentos = 0;
        }

        public void incrementarQtdeMovimento()
        {
            quantMovimentos++;
        }

        public abstract bool[,] movimentosPossiveis();      

    }
}
