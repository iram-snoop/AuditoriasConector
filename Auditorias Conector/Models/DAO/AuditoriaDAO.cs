using Auditorias_Conector.Models.DAO;
using System.ComponentModel.DataAnnotations.Schema;

public class AuditoriaDAO : EntidadDB
{
    public string JsonResult { get; set; }
}

public class Filial
{
    public int Id { get; set; }
    public int IdFilial { get; set; }
    public string Descripcion { get; set; }
    public bool EsFilial { get; set; }
    public string Domicilio { get; set; }
    public string Email { get; set; }
}

public class Empresa
{
    public int Id { get; set; }
    public int IdEmpresa { get; set; }
    public string Cuit { get; set; }
    public string RazonSocial { get; set; }
    public string Email { get; set; }
    public bool EsExtranjero { get; set; }
    public bool Enabled { get; set; }
}

public class FilialVigente
{
    public int Id { get; set; }
    public int IdFilial { get; set; }
    public string Descripcion { get; set; }
    public bool EsFilial { get; set; }
    public string Domicilio { get; set; }
    public string Email { get; set; }
}

public class Persona
{
    public int Id { get; set; }
    public int IdPersona { get; set; }
    public string Cuit { get; set; }
    public string Nombre { get; set; }
    public string IdentificadorNumero { get; set; }
    public string IdentificadorTipo { get; set; }
    public int IdentificadorTipoId { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Telefono { get; set; }
    public bool Enabled { get; set; }
    public bool EsIram { get; set; }
    public bool EsExtranjero { get; set; }

    [NotMapped]
    public FilialVigente FilialVigente { get; set; }
    public string CentroCosto { get; set; }
    public string ModifyBy { get; set; }
    public DateTime ModifyDate { get; set; }
    public int OportunidadId { get; set; }
}

public class CategoriaFiscal
{
    public int Id { get; set; }
    public int IdCategoriaFiscal { get; set; }
    public string Nombre { get; set; }
    public DateTime ModifyDate { get; set; }
}

public class MetodoPago
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}

public class Curso
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Codigo { get; set; }
    public int Precio { get; set; }
}

public class Participante
{
    public int Id { get; set; }
    public int IdPersona { get; set; }
    public string Cuit { get; set; }
    public string Nombre { get; set; }
    public string IdentificadorNumero { get; set; }
    public string IdentificadorTipo { get; set; }
    public int IdentificadorTipoId { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Telefono { get; set; }
    public bool Enabled { get; set; }
    public bool EsIram { get; set; }
    public bool EsExtranjero { get; set; }

    [NotMapped]
    public FilialVigente FilialVigente { get; set; }
    public string CentroCosto { get; set; }
    public string ModifyBy { get; set; }
    public DateTime ModifyDate { get; set; }
    public int OportunidadId { get; set; }
}

public class Producto
{
    public string Concepto { get; set; }
    public float Cantidad { get; set; }
    public float Monto { get; set; }
    public string ProductoCodigo { get; set; }
}

public class OrdenFacturacion
{
    [NotMapped]
    public Curso Curso { get; set; }
    public DateTime Fecha { get; set; }
    public int Precio { get; set; }
    public int MontoDescuento { get; set; }
    public bool EsIncompany { get; set; }
    public string ProductoCodigo { get; set; }

    [NotMapped]
    public List<Participante> Participantes { get; set; }

    [NotMapped]
    public List<Producto> Productos { get; set; }
}

public class JsonResultData
{
    public string IdFormacion { get; set; }
    public bool EsPorEmpresa { get; set; }

    [NotMapped]
    public Filial Filial { get; set; }

    [NotMapped]
    public Empresa Empresa { get; set; }

    [NotMapped]
    public Persona Persona { get; set; }
    public string Domicilio { get; set; }
    public string EmailContacto { get; set; }

    [NotMapped]
    public CategoriaFiscal CategoriaFiscal { get; set; }
    public string NumeroOrdenCompra { get; set; }
    public DateTime FechaFacturacion { get; set; }

    [NotMapped]
    public MetodoPago MetodoPago { get; set; }
    public string CuitFacturacion { get; set; }
    public string ProductoCodigo { get; set; }

    [NotMapped]
    public List<OrdenFacturacion> OrdenesFacturacion { get; set; }

    [NotMapped]
    public List<Producto> Productos { get; set; }
}
