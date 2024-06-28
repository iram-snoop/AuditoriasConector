namespace Auditorias_Conector.Models.DTO
{
    public class OrdenesFacturacion
    {
        public Curso Curso { get; set; }
        public DateTime Fecha { get; set; }
        public float Precio { get; set; }
        public float MontoDescuento { get; set; } 
        public bool EsIncompany { get; set; }
        public string ProductoCodigo { get; set; }
        public List<Persona> Participantes { get; set; }
        public List<Producto> Productos { get; set; }
    }
}
