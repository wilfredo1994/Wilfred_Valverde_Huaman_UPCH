using Wilfred_Valverde_Huaman_UPCH.Data;
using Wilfred_Valverde_Huaman_UPCH.Entities;

namespace Wilfred_Valverde_Huaman_UPCH.Logic
{
    public class TipoDocumentoLogic
    {
        private TipoDocumentoData tipoDocumentoData;
        public TipoDocumentoLogic()
        {
            tipoDocumentoData = new TipoDocumentoData();
        }

        public List<ListaTipoDocumento> GetTipoDocumentos()
        {
            List<ListaTipoDocumento> tipoDocumentos = new List<ListaTipoDocumento>();
            tipoDocumentos = tipoDocumentoData.GetTipoDocumentos();
            return tipoDocumentos;
        }

        public List<ListaTipoDocumento> GetTipoDocumento(int IdTipoDocumento)
        {
            List<ListaTipoDocumento> tipoDocumentos = new List<ListaTipoDocumento>();
            tipoDocumentos = tipoDocumentoData.GetTipoDocumento(IdTipoDocumento);
            return tipoDocumentos;
        }

        public int RegistrarTipoDocumento(TipoDocumento tipoDocumento)
        {
            int newUserId = 0;
            newUserId = tipoDocumentoData.RegistrarTipoDocumento(tipoDocumento);
            return newUserId;
        }

        public int ActualizarTipoDocumento(TipoDocumento tipoDocumento)
        {
            int newUserId = 0;
            newUserId = tipoDocumentoData.ActualizarTipoDocumento(tipoDocumento);
            return newUserId;
        }

        public int EliminarTipoDocumento(int IdTipoDocumento)
        {
            int newUserId = 0;
            newUserId = tipoDocumentoData.EliminarTipoDocumento(IdTipoDocumento);
            return newUserId;
        }
    }
}
