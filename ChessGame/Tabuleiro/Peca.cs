using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Tabuleiro
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Color cor { get; protected set; }
        public int quantMovimentos { get; protected set; }
        public TabuleiroChess tabuleiro { get; protected set; }

        public Peca(Posicao posicao, TabuleiroChess tab, Color cor)
        {
            this.posicao = posicao;
            this.tabuleiro = tab;
            this.cor = cor;
            this.quantMovimentos = 0;
        }

    }
}
