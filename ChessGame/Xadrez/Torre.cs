using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Tabuleiro;

namespace ChessGame.Xadrez
{
    class Torre : Peca
    {
        public Torre(TabuleiroChess tab, Color cor) : base(tab, cor)
        { }

        public override string ToString()
        {
            return "T";
        }
    }
}
