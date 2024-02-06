using Microsoft.Data.SqlClient;
using System.Data;
using Wilfred_Valverde_Huaman_UPCH.Config;
using Wilfred_Valverde_Huaman_UPCH.Entities;

namespace Wilfred_Valverde_Huaman_UPCH.Data
{
    public class PersonaData
    {
        public List<Persona> GetPersonas()
        {
            var lista = new List<Persona>();
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("GetPersonas", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var objeto = new Persona();
                            objeto.IdPersona = reader.GetInt32(reader.GetOrdinal("IdPersona"));
                            objeto.TipoDocumento = new TipoDocumento
                            {
                                IdTipoDocumento = reader.GetInt32(reader.GetOrdinal("IdTipoDocumento")),
                                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"))
                            };
                            objeto.NumeroDocumento =  reader.GetString(reader.GetOrdinal("NumeroDocumento"));
                            objeto.Nombres = reader.GetString(reader.GetOrdinal("Nombres"));
                            objeto.ApellidoPaterno = reader.GetString(reader.GetOrdinal("ApellidoPaterno"));
                            objeto.ApellidoMaterno = reader.GetString(reader.GetOrdinal("ApellidoMaterno"));
                            objeto.Telefono = reader.GetString(reader.GetOrdinal("Telefono"));
                            objeto.Correo = reader.GetString(reader.GetOrdinal("Correo"));
                            objeto.Direccion = reader.GetString(reader.GetOrdinal("Direccion"));
                            lista.Add(objeto);
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }

            return lista;
        }

        public List<Persona> GetPersona(int IdPersona)
        {
            var lista = new List<Persona>();
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("GetPersona", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdPersona", IdPersona);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var objeto = new Persona();
                            objeto.IdPersona = reader.GetInt32(reader.GetOrdinal("IdPersona"));
                            objeto.TipoDocumento = new TipoDocumento
                            {
                                IdTipoDocumento = reader.GetInt32(reader.GetOrdinal("IdTipoDocumento")),
                                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"))
                            };
                            objeto.NumeroDocumento = reader.GetString(reader.GetOrdinal("NumeroDocumento"));
                            objeto.Nombres = reader.GetString(reader.GetOrdinal("Nombres"));
                            objeto.ApellidoPaterno = reader.GetString(reader.GetOrdinal("ApellidoPaterno"));
                            objeto.ApellidoMaterno = reader.GetString(reader.GetOrdinal("ApellidoMaterno"));
                            objeto.Telefono = reader.GetString(reader.GetOrdinal("Telefono"));
                            objeto.Correo = reader.GetString(reader.GetOrdinal("Correo"));
                            objeto.Direccion = reader.GetString(reader.GetOrdinal("Direccion"));
                            lista.Add(objeto);
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }

            return lista;
        }

        public int RegistrarPersona(Persona persona)
        {
            int newUserId = 0;
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("RegistrarPersona", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdTipoDocumento", persona.TipoDocumento.IdTipoDocumento);
                        command.Parameters.AddWithValue("@NumeroDocumento", persona.NumeroDocumento);
                        command.Parameters.AddWithValue("@Nombres", persona.Nombres);
                        command.Parameters.AddWithValue("@ApellidoPaterno", persona.ApellidoPaterno);
                        command.Parameters.AddWithValue("@ApellidoMaterno", persona.ApellidoMaterno);
                        command.Parameters.AddWithValue("@Telefono", persona.Telefono);
                        command.Parameters.AddWithValue("@Correo", persona.Correo);
                        command.Parameters.AddWithValue("@Direccion", persona.Direccion);
                        command.Parameters.Add("IdProceso", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.ExecuteNonQuery();
                        newUserId = Convert.ToInt32(command.Parameters["IdProceso"].Value);

                    }
                    con.Close();
                    return newUserId;
                }
            }
            catch (Exception ex)
            {
                return newUserId;
            }
        }

        public int ActualizarPersona(Persona persona)
        {
            int result = 0;
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("ActualizarPersona", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdPersona", persona.IdPersona);
                        command.Parameters.AddWithValue("@IdTipoDocumento", persona.TipoDocumento.IdTipoDocumento);
                        command.Parameters.AddWithValue("@NumeroDocumento", persona.NumeroDocumento);
                        command.Parameters.AddWithValue("@Nombres", persona.Nombres);
                        command.Parameters.AddWithValue("@ApellidoPaterno", persona.ApellidoPaterno);
                        command.Parameters.AddWithValue("@ApellidoMaterno", persona.ApellidoMaterno);
                        command.Parameters.AddWithValue("@Telefono", persona.Telefono);
                        command.Parameters.AddWithValue("@Correo", persona.Correo);
                        command.Parameters.AddWithValue("@Direccion", persona.Direccion);
                        command.Parameters.Add("IdProceso", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.ExecuteNonQuery();
                        result = Convert.ToInt32(command.Parameters["IdProceso"].Value);
                    }
                    con.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int EliminarPersona(int IdPersona)
        {
            int result = 0;
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("EliminarPersona", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdPersona", IdPersona);
                        command.Parameters.Add("IdProceso", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.ExecuteNonQuery();
                        result = Convert.ToInt32(command.Parameters["IdProceso"].Value);
                    }
                    con.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
