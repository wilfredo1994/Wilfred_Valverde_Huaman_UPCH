using Wilfred_Valverde_Huaman_UPCH.Data;
using Wilfred_Valverde_Huaman_UPCH.Entities;

namespace Wilfred_Valverde_Huaman_UPCH.Logic
{
    public class PersonaLogic
    {
        private PersonaData personaData;
        public PersonaLogic()
        {
            personaData = new PersonaData();
        }

        public List<Persona> GetPersonas()
        {
            List<Persona> persona = new List<Persona>();
            persona = personaData.GetPersonas();
            return persona;
        }

        public List<Persona> GetPersona(int IdPersona)
        {
            List<Persona> persona = new List<Persona>();
            persona = personaData.GetPersona(IdPersona);
            return persona;
        }

        public int RegistrarPersona(Persona persona)
        {
            int newUserId = 0;
            newUserId = personaData.RegistrarPersona(persona);
            return newUserId;
        }
        public int ActualizarPersona(Persona persona)
        {
            int newUserId = 0;
            newUserId = personaData.ActualizarPersona(persona);
            return newUserId;
        }

        public int EliminarPersona(int IdPersona)
        {
            int newUserId = 0;
            newUserId = personaData.EliminarPersona(IdPersona);
            return newUserId;
        }
    }
}
