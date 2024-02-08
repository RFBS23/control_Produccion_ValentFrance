using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class B_Usuarios
    {
        public List<Usuarios> IniciarSesion()
        {
            List<Usuarios> lista = new List<Usuarios>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.idusuario, u.documento, u.nombres, u.apellidos, u.nombreusuario, u.correo, u.clave, u.estado, nv.idnivelacceso, nv.nombre from usuarios u");
                    query.AppendLine("inner join nivelesaccesos nv on nv.idnivelacceso = u.idnivelacceso");
                    query.AppendLine("where u.estado = 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuarios()
                            {
                                idusuario = Convert.ToInt32(dr["idusuario"]),
                                documento = dr["documento"].ToString(),
                                nombres = dr["nombres"].ToString(),
                                apellidos = dr["apellidos"].ToString(),
                                nombreusuario = dr["nombreusuario"].ToString(),
                                correo = dr["correo"].ToString(),
                                clave = dr["clave"].ToString(),
                                oNivelacceso = new Nivelacceso() { idnivelacceso = Convert.ToInt32(dr["idnivelacceso"]), nombre = dr["nombre"].ToString() },
                                estado = Convert.ToInt32(dr["estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Usuarios>();
                }
            }
            return lista;
        }

        public List<Usuarios> Listar()
        {
            List<Usuarios> lista = new List<Usuarios>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.idusuario, u.documento, u.nombres, u.apellidos, u.nombreusuario, u.correo, u.clave, u.estado, nv.idnivelacceso, nv.nombre from usuarios u");
                    query.AppendLine("inner join nivelesaccesos nv on nv.idnivelacceso = u.idnivelacceso");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuarios()
                            {
                                idusuario = Convert.ToInt32(dr["idusuario"]),
                                documento = dr["documento"].ToString(),
                                nombres = dr["nombres"].ToString(),
                                apellidos = dr["apellidos"].ToString(),
                                nombreusuario = dr["nombreusuario"].ToString(),
                                correo = dr["correo"].ToString(),
                                clave = dr["clave"].ToString(),
                                oNivelacceso = new Nivelacceso() { idnivelacceso = Convert.ToInt32(dr["idnivelacceso"]), nombre = dr["nombre"].ToString() },
                                estado = Convert.ToInt32(dr["estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Usuarios>();
                }
            }
            return lista;
        }

    }
}
