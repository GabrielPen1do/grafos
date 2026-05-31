namespace Grafos
{
    internal class ResultadoCaminho
    {
        public bool Possivel { get; }
        public List<int> Caminho { get; }
        public int CustoTotal { get; }
        public string Mensagem { get; }

        public ResultadoCaminho(bool possivel, List<int> caminho, int custoTotal, string mensagem)
        {
            Possivel = possivel;
            Caminho = caminho;
            CustoTotal = custoTotal;
            Mensagem = mensagem;
        }
    }
}
