namespace GerenciamentoDePedidosWebApi.Application.Dto
{
    public class PedidoDto
    {
        public decimal PedidoId { get; set; }
        public decimal ClienteId { get; set; }
        public decimal Total { get; set; }
        public DateTime DataCriacao { get; set; }

        public List<PedidoItemDto> Itens { get; set; } = new();
    }
}
