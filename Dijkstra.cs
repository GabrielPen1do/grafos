namespace Grafos
{
    internal class Dijkstra
    {
        public ResultadoDijkstra Calcular(GrafoDirecionado grafo, int origem)
        {
            Dictionary<int, int> distancias = new Dictionary<int, int>();
            Dictionary<int, int> anteriores = new Dictionary<int, int>();
            List<int> naoVisitados = new List<int>();

            foreach (int vertice in grafo.ObterVertices())
            {
                distancias[vertice] = int.MaxValue;
                anteriores[vertice] = -1;
                naoVisitados.Add(vertice);
            }

            distancias[origem] = 0;

            while (naoVisitados.Count > 0)
            {
                int atual = EncontrarVerticeComMenorDistancia(naoVisitados, distancias);

                if (atual == -1)
                {
                    break;
                }

                naoVisitados.Remove(atual);

                foreach (Aresta aresta in grafo.ObterVizinhos(atual))
                {
                    if (distancias[atual] == int.MaxValue)
                    {
                        continue;
                    }

                    int novaDistancia = distancias[atual] + aresta.Peso;

                    if (novaDistancia < distancias[aresta.Destino])
                    {
                        distancias[aresta.Destino] = novaDistancia;
                        anteriores[aresta.Destino] = atual;
                    }
                }
            }

            return new ResultadoDijkstra(distancias, anteriores);
        }

        private int EncontrarVerticeComMenorDistancia(List<int> vertices, Dictionary<int, int> distancias)
        {
            int menorVertice = -1;
            int menorDistancia = int.MaxValue;

            foreach (int vertice in vertices)
            {
                if (distancias[vertice] < menorDistancia)
                {
                    menorDistancia = distancias[vertice];
                    menorVertice = vertice;
                }
            }

            return menorVertice;
        }
    }
}
