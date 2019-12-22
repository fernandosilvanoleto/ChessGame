using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Tabuleiro;
using System.Collections.Generic;

namespace ChessGame.Xadrez
{
    class PartidaXadrez
    {
        public TabuleiroChess tab { get; private set; }
        public int turno { get; private set; }
        public Color jogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEmPassant { get; private set; }

        public PartidaXadrez()
        {
            tab = new TabuleiroChess(8, 8);
            turno = 1;
            jogadorAtual = Color.Branca;
            Terminada = false;
            xeque = false;
            vulneravelEmPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdeMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // #JOGADAESPECIAL ROQUE PEQUENO
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = tab.retirarPeca(origemTorre);
                torre.incrementarQtdeMovimento();
                tab.colocarPeca(torre, destinoTorre);
            }

            // #JOGADAESPECIAL ROQUE GRANDE
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = tab.retirarPeca(origemTorre);
                torre.incrementarQtdeMovimento();
                tab.colocarPeca(torre, destinoTorre);
            }

            // #JOGADOESPECIAL em Passant

            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.cor == Color.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }

            }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdeMovimento();

            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            tab.colocarPeca(p, origem);

            // #JOGADAESPECIAL ROQUE PEQUENO
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = tab.retirarPeca(destinoTorre);
                torre.decrementarQtdeMovimento();
                tab.colocarPeca(torre, origemTorre);
            }

            // #JOGADAESPECIAL ROQUE GRANDE
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = tab.retirarPeca(destinoTorre);
                torre.decrementarQtdeMovimento();
                tab.colocarPeca(torre, origemTorre);
            }

            // #JOGADOESPECIAL em Passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEmPassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if (p.cor == Color.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    tab.colocarPeca(peao, posP);
                }
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroChessException("Você não pode ser colocar em Xeque!!!");
            }

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (XequeMate(adversaria(jogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }

            Peca p = tab.peca(destino);

            // #JOGADAESPECIAL em Passant

            if (p is Peao && (destino.Linha == origem.Linha - 2 
                || destino.Linha == origem.Linha + 2 ))
            {
                vulneravelEmPassant = p;
            }
            else
            {
                vulneravelEmPassant = null;
            }

        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroChessException("Não existe peça na posição de origem escolhida!");
            }
            if(jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroChessException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroChessException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroChessException("Posição de destino inválida!");
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Color.Branca)
            {
                jogadorAtual = Color.Preta;
            }
            else
            {
                jogadorAtual = Color.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Color cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca item in capturadas)
            {
                if (item.cor == cor)
                {
                    aux.Add(item);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Color cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca item in pecas)
            {
                if (item.cor == cor)
                {
                    aux.Add(item);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Color adversaria(Color cor)
        {

            if (cor == Color.Branca)
            {
                return Color.Preta;
            }
            else
            {
                return Color.Branca;
            }
        }

        private Peca rei(Color cor) // vai devolver o rei de uma determinada cor
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei) // x é uma instância de Rei
                {
                    return x;
                }
            }

            return null;
        }

        public bool estaEmXeque(Color cor)
        {                       
            Peca R = rei(cor);
            
            if (R == null)
            {
                throw new TabuleiroChessException("Não tem Rei da cor " + cor + " no tabuleiro! Jogo deu ruim!");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();

                if (mat[R.posicao.Linha, R.posicao.Coluna])
                {
                    return true;
                }
            }
            return false;           
        }


        public bool XequeMate(Color cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.Linhas; i++)
                {
                    for (int j = 0; j < tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Color.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Color.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Color.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Color.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Color.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Color.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Color.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Color.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Color.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Color.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Color.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Color.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Color.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Color.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Color.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Color.Branca, this));



            colocarNovaPeca('a', 8, new Torre(tab, Color.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Color.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Color.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Color.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Color.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Color.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Color.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Color.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Color.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Color.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Color.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Color.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Color.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Color.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Color.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Color.Preta, this));
                        
        }

    }
}
