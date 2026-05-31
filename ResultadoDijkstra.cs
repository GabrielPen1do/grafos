namespace Grafos
{
    internal class ResultadoDijkstra
    {
        public Dictionary<int, int> Distancias { get; }
        public Dictionary<int, int> Anteriores { get; }

        public ResultadoDijkstra(Dictionary<int, int> distancias, Dictionary<int, int> anteriores)
        {
            Distancias = distancias;
            Anteriores = anteriores;
        }

        public bool ExisteCaminhoAte(int destino)
        {
            return Distancias.ContainsKey(destino) && Distancias[destino] != int.MaxValue;
        }

        public List<int> MontarCaminho(int origem, int destino)
        {
            List<int> caminho = new List<int>();

            if (!ExisteCaminhoAte(destino))
            {
                return caminho;
            }

            int atual = destino;

            while (atual != -1)
            {
                caminho.Add(atual);

                if (atual == origem)
                {
                    break;
                }

                atual = Anteriores[atual];
            }

            caminho.Reverse();
            return caminho;
        }
    }
}
