namespace Grafos
{
    internal class Aresta
    {
        public int Origem { get; }
        public int Destino { get; }
        public int Peso { get; }

        public Aresta(int origem, int destino, int peso)
        {
            Origem = origem;
            Destino = destino;
            Peso = peso;
        }
    }
}
