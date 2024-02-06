using System.Data;
using Microsoft.Data.SqlClient;
using Wilfred_Valverde_Huaman_UPCH.Config;
using Wilfred_Valverde_Huaman_UPCH.Entities;

namespace Wilfred_Valverde_Huaman_UPCH.Data
{
    public class TipoDocumentoData
    {
        public List<ListaTipoDocumento> GetTipoDocumentos()
        {
            var lista = new List<ListaTipoDocumento>();
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("GetTipoDocumentos", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var objeto = new ListaTipoDocumento();
                            objeto.IdTipoDocumento = reader.GetInt32(reader.GetOrdinal("IdTipoDocumento"));
                            objeto.Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));                           
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

        public List<ListaTipoDocumento> GetTipoDocumento(int IdTipoDocumento)
        {
            var lista = new List<ListaTipoDocumento>();
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("GetTipoDocumento", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdTipoDocumento", IdTipoDocumento);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var objeto = new ListaTipoDocumento();
                            objeto.IdTipoDocumento = reader.GetInt32(reader.GetOrdinal("IdTipoDocumento"));
                            objeto.Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));
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

        public int RegistrarTipoDocumento(TipoDocumento tipoDocumento)
        {
            int newUserId = 0;
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("RegistrarTipoDocumento", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Descripcion", tipoDocumento.Descripcion);
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

        public int ActualizarTipoDocumento(TipoDocumento tipoDocumento)
        {
            int result = 0;
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("ActualizarTipoDocumento", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdTipoDocumento", tipoDocumento.IdTipoDocumento);
                        command.Parameters.AddWithValue("@Descripcion", tipoDocumento.Descripcion);
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

        public int EliminarTipoDocumento(int IdTipoDocumento)
        {
            int result = 0;
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("EliminarTipoDocumento", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdTipoDocumento", IdTipoDocumento);
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
