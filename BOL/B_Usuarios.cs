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
                                estado = Convert.ToBoolean(dr["estado"])
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
                                estado = Convert.ToBoolean(dr["estado"]),
                                oNivelacceso = new Nivelacceso() { idnivelacceso = Convert.ToInt32(dr["idnivelacceso"]), nombre = dr["nombre"].ToString() },
                                
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

        public int Registrar(Usuarios obj, out string Mensaje)
        {
            int idusuariogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("spu_registrar_usuario", oconexion);
                    cmd.Parameters.AddWithValue("documento", obj.documento);
                    cmd.Parameters.AddWithValue("nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("nombreusuario", obj.nombreusuario);
                    cmd.Parameters.AddWithValue("correo", obj.correo);
                    cmd.Parameters.AddWithValue("clave", obj.clave);
                    cmd.Parameters.AddWithValue("idnivelacceso", obj.oNivelacceso.idnivelacceso);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("idusuarioresultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idusuariogenerado = Convert.ToInt32(cmd.Parameters["idusuarioresultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idusuariogenerado = 0;
                Mensaje = ex.Message;
            }
            return idusuariogenerado;
        }

        public bool Editar(Usuarios obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("spu_editar_usuario", oconexion);
                    cmd.Parameters.AddWithValue("idusuario", obj.idusuario);
                    cmd.Parameters.AddWithValue("documento", obj.documento);
                    cmd.Parameters.AddWithValue("nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("nombreusuario", obj.nombreusuario);
                    cmd.Parameters.AddWithValue("correo", obj.correo);
                    cmd.Parameters.AddWithValue("clave", obj.clave);
                    cmd.Parameters.AddWithValue("idnivelacceso", obj.oNivelacceso.idnivelacceso);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("respuesta", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["respuesta"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

    }
}
