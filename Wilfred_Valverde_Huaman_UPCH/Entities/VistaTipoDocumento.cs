using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Wilfred_Valverde_Huaman_UPCH.Entities
{    

    public class VistaTipoDocumento
    {
      public List<ListaTipoDocumento> listaTipoDocumento { get; set; }
    }

    public class ListaTipoDocumento
    {
        public int IdTipoDocumento { get; set; }
        public string Descripcion { get; set; }
    }
}
