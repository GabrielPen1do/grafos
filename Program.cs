namespace Grafos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Trabalho de Grafos - Grupo: Grupo 1");
            Console.WriteLine("Grafo direcionado com Dijkstra");
            Console.WriteLine();

            try
            {
                string caminhoArquivo = LerTexto("Informe o caminho do arquivo CSV: ");
                int verticeInicial = LerInteiro("Informe o vertice inicial: ");

                LeitorCsvGrafo leitor = new LeitorCsvGrafo();
                GrafoDirecionado grafo = leitor.Ler(caminhoArquivo);

                GeradorCaminho gerador = new GeradorCaminho();
                ResultadoCaminho resultado = gerador.GerarCaminhoVisitandoTodos(grafo, verticeInicial);

                Console.WriteLine();

                if (!resultado.Possivel)
                {
                    Console.WriteLine("Nao foi possivel gerar um caminho que passe por todos os vertices.");
                    Console.WriteLine(resultado.Mensagem);
                    return;
                }

                Console.WriteLine("Caminho encontrado:");
                Console.WriteLine(FormatarCaminho(resultado.Caminho));
                Console.WriteLine($"Custo total: {resultado.CustoTotal}");
                Console.WriteLine($"Quantidade de vertices no caminho: {resultado.Caminho.Count}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Arquivo nao encontrado. Verifique o caminho informado.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Erro de formato: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        private static string LerTexto(string mensagem)
        {
            Console.Write(mensagem);
            string texto = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(texto))
            {
                throw new FormatException("O valor informado nao pode estar vazio.");
            }

            return texto.Trim().TrimStart('\uFEFF');
        }

        private static int LerInteiro(string mensagem)
        {
            Console.Write(mensagem);
            string texto = Console.ReadLine() ?? "";

            int valor;

            if (!int.TryParse(texto, out valor))
            {
                throw new FormatException("O vertice inicial deve ser um numero inteiro.");
            }

            return valor;
        }

        private static string FormatarCaminho(List<int> caminho)
        {
            const int limite = 80;

            if (caminho.Count <= limite)
            {
                return string.Join(" -> ", caminho);
            }

            List<int> inicio = new List<int>();
            List<int> fim = new List<int>();

            for (int i = 0; i < 40; i++)
            {
                inicio.Add(caminho[i]);
            }

            for (int i = caminho.Count - 40; i < caminho.Count; i++)
            {
                fim.Add(caminho[i]);
            }

            return string.Join(" -> ", inicio)
                + " -> ... -> "
                + string.Join(" -> ", fim);
        }
    }
}
