namespace Wilfred_Valverde_Huaman_UPCH.Entities
{
    public class VistaPersona
    {
        public List<Persona> listaPersona { get; set; }
    }

    public class Persona
    {
        public int IdPersona { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
    }
}
