namespace Wilfred_Valverde_Huaman_UPCH.ViewModels
{
    public class GlobalResponse<T>
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
        public T Data { get; set; }
    }

}
