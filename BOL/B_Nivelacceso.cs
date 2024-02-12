using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BOL
{
    public class B_Nivelacceso
    {
        public List<Nivelacceso> Listar()
        {
            List<Nivelacceso> lista = new List<Nivelacceso>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select idnivelacceso, nombre from nivelesaccesos");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Nivelacceso()
                            {
                                idnivelacceso = Convert.ToInt32(dr["idnivelacceso"]),
                                nombre = dr["nombre"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Nivelacceso>();
                }
            }
            return lista;
        }
    }
}
