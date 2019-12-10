using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Tabuleiro
{
    class TabuleiroChessException : Exception
    {
        public TabuleiroChessException(string msg) : base(msg)
        { }
    }
}
