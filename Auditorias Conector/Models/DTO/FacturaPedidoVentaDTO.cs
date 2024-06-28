namespace Auditorias_Conector.Models.DTO
{
    public class FacturaPedidoVentaDTO
    {
        public string IdentificacionExterna { get; set; }
        public bool EsPorEmpresa { get; set; }
        public Filial filial { get; set; }
        public Empresa Empresa { get; set; }
        public Persona Persona { get; set; }
        public string Domicilio { get; set; }
        public string EmailContacto { get; set; }
        public string Observaciones { get; set; }
        public CategoriaFiscal CategoriaFiscal { get; set; }
        public string NumeroOrdenCompra { get; set; }
        public DateTime fechaFacturacion { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public string CuitFacturacion { get; set; }
        //public int numeroOrdenFacturacion { get; set; }
        public string ProductoCodigo { get; set; }
        public List<OrdenesFacturacion> OrdenesFacturacion { get; set; }
        public List<Producto> Productos { get; set; }
    }
}
