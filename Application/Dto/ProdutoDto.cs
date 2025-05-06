namespace GerenciamentoDePedidosWebApi.Application.Dto
{
    public class ProdutoDto
    {
        public decimal IdProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal PrecoProduto { get; set; }
        public int Estoque { get; set; }
    }
}
