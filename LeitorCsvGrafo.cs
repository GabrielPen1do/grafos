namespace Grafos
{
    internal class LeitorCsvGrafo
    {
        public GrafoDirecionado Ler(string caminhoArquivo)
        {
            GrafoDirecionado grafo = new GrafoDirecionado();

            using StreamReader leitor = new StreamReader(caminhoArquivo);

            string linha = leitor.ReadLine() ?? "";
            int numeroLinha = 1;

            if (linha == "")
            {
                throw new FormatException("O arquivo CSV esta vazio.");
            }

            while (!leitor.EndOfStream)
            {
                linha = leitor.ReadLine() ?? "";
                numeroLinha++;

                if (string.IsNullOrWhiteSpace(linha))
                {
                    continue;
                }

                string[] partes = linha.Split(',');

                if (partes.Length != 3)
                {
                    throw new FormatException($"Linha {numeroLinha}: esperado origem,destino,peso.");
                }

                int origem;
                int destino;
                int peso;

                if (!int.TryParse(partes[0], out origem)
                    || !int.TryParse(partes[1], out destino)
                    || !int.TryParse(partes[2], out peso))
                {
                    throw new FormatException($"Linha {numeroLinha}: origem, destino e peso devem ser numeros inteiros.");
                }

                grafo.AdicionarAresta(origem, destino, peso);
            }

            if (grafo.ObterQuantidadeVertices() == 0)
            {
                throw new FormatException("O arquivo CSV nao possui arestas validas.");
            }

            return grafo;
        }
    }
}
