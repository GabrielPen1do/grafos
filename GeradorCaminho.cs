namespace Grafos
{
    internal class GeradorCaminho
    {
        private readonly Dijkstra _dijkstra = new Dijkstra();

        public ResultadoCaminho GerarCaminhoVisitandoTodos(GrafoDirecionado grafo, int verticeInicial)
        {
            if (!grafo.ContemVertice(verticeInicial))
            {
                return new ResultadoCaminho(false, new List<int>(), 0, "O vertice inicial nao existe no grafo.");
            }

            ResultadoCaminho caminhoSequencial = TentarGerarCaminhoSequencial(grafo, verticeInicial);

            if (caminhoSequencial.Possivel)
            {
                return caminhoSequencial;
            }

            HashSet<int> visitados = new HashSet<int>();
            List<int> caminhoCompleto = new List<int> { verticeInicial };
            int custoTotal = 0;
            int atual = verticeInicial;

            visitados.Add(verticeInicial);

            while (visitados.Count < grafo.ObterQuantidadeVertices())
            {
                ResultadoDijkstra resultadoDijkstra = _dijkstra.Calcular(grafo, atual);
                int proximo = EncontrarProximoVertice(grafo, atual, visitados, resultadoDijkstra);

                if (proximo == -1)
                {
                    string mensagem = $"A partir do vertice {atual}, nao foi encontrado um proximo passo que permita visitar todos os vertices restantes.";
                    return new ResultadoCaminho(false, caminhoCompleto, custoTotal, mensagem);
                }

                List<int> trecho = resultadoDijkstra.MontarCaminho(atual, proximo);

                for (int i = 1; i < trecho.Count; i++)
                {
                    caminhoCompleto.Add(trecho[i]);
                    visitados.Add(trecho[i]);
                }

                custoTotal += resultadoDijkstra.Distancias[proximo];
                atual = proximo;
            }

            return new ResultadoCaminho(true, caminhoCompleto, custoTotal, "Caminho gerado com sucesso.");
        }

        private ResultadoCaminho TentarGerarCaminhoSequencial(GrafoDirecionado grafo, int verticeInicial)
        {
            List<int> vertices = grafo.ObterVertices();
            vertices.Sort();

            if (vertices.Count == 0 || vertices[0] != verticeInicial)
            {
                return new ResultadoCaminho(false, new List<int>(), 0, "Nao foi possivel usar caminho sequencial.");
            }

            List<int> caminho = new List<int>();
            int custoTotal = 0;

            caminho.Add(verticeInicial);

            for (int i = 1; i < vertices.Count; i++)
            {
                int origem = vertices[i - 1];
                int destino = vertices[i];
                int peso = grafo.ObterPesoAresta(origem, destino);

                if (peso == -1)
                {
                    return new ResultadoCaminho(false, new List<int>(), 0, "Nao foi possivel usar caminho sequencial.");
                }

                caminho.Add(destino);
                custoTotal += peso;
            }

            return new ResultadoCaminho(true, caminho, custoTotal, "Caminho gerado com sucesso.");
        }

        private int EncontrarProximoVertice(
            GrafoDirecionado grafo,
            int atual,
            HashSet<int> visitados,
            ResultadoDijkstra resultadoDijkstra)
        {
            int melhorVertice = -1;
            int melhorDistancia = int.MaxValue;

            foreach (int vertice in grafo.ObterVertices())
            {
                if (visitados.Contains(vertice))
                {
                    continue;
                }

                if (!resultadoDijkstra.ExisteCaminhoAte(vertice))
                {
                    continue;
                }

                int distancia = resultadoDijkstra.Distancias[vertice];

                if (distancia < melhorDistancia && AindaPermiteContinuar(grafo, atual, vertice, visitados, resultadoDijkstra))
                {
                    melhorDistancia = distancia;
                    melhorVertice = vertice;
                }
            }

            return melhorVertice;
        }

        private bool AindaPermiteContinuar(
            GrafoDirecionado grafo,
            int atual,
            int candidato,
            HashSet<int> visitados,
            ResultadoDijkstra resultadoAtual)
        {
            HashSet<int> visitadosDepoisDoTrecho = new HashSet<int>(visitados);
            List<int> trecho = resultadoAtual.MontarCaminho(atual, candidato);

            foreach (int vertice in trecho)
            {
                visitadosDepoisDoTrecho.Add(vertice);
            }

            ResultadoDijkstra resultadoCandidato = _dijkstra.Calcular(grafo, candidato);

            foreach (int vertice in grafo.ObterVertices())
            {
                if (!visitadosDepoisDoTrecho.Contains(vertice) && !resultadoCandidato.ExisteCaminhoAte(vertice))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
