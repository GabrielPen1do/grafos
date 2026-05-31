namespace Grafos
{
    internal class GrafoDirecionado
    {
        private readonly Dictionary<int, List<Aresta>> _adjacencias = new Dictionary<int, List<Aresta>>();

        public List<int> ObterVertices()
        {
            return new List<int>(_adjacencias.Keys);
        }

        public int ObterQuantidadeVertices()
        {
            return _adjacencias.Count;
        }

        public void AdicionarVertice(int vertice)
        {
            if (!_adjacencias.ContainsKey(vertice))
            {
                _adjacencias[vertice] = new List<Aresta>();
            }
        }

        public void AdicionarAresta(int origem, int destino, int peso)
        {
            if (peso < 0)
            {
                throw new FormatException("O algoritmo de Dijkstra nao aceita pesos negativos.");
            }

            AdicionarVertice(origem);
            AdicionarVertice(destino);

            _adjacencias[origem].Add(new Aresta(origem, destino, peso));
        }

        public bool ContemVertice(int vertice)
        {
            return _adjacencias.ContainsKey(vertice);
        }

        public List<Aresta> ObterVizinhos(int vertice)
        {
            if (!_adjacencias.ContainsKey(vertice))
            {
                return new List<Aresta>();
            }

            return _adjacencias[vertice];
        }

        public int ObterPesoAresta(int origem, int destino)
        {
            foreach (Aresta aresta in ObterVizinhos(origem))
            {
                if (aresta.Destino == destino)
                {
                    return aresta.Peso;
                }
            }

            return -1;
        }
    }
}
