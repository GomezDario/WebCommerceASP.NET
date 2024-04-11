using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;



namespace CapaDatos
{
    public class CD_Usuarios

    {

        public List<Usuario> Listar()
        {

            List<Usuario> lista = new List<Usuario>();


            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo from USUARIO";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new Usuario(){
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                    Activo = Convert.ToBoolean(dr["Activo"])


                                }
                                );


                        }


                    }



                }

            }
            catch
            { 

                lista = new List<Usuario>();

                
            }



            return lista;


        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idautogenerado = 0; // almacenamos el id del usuario nuevo, aca lo guardamos

            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) // cpnexion
                {

                    SqlCommand cmd = new SqlCommand ("sp_RegistrarUsuario", oconexion); // llamo al proc almacenado en sql manager
                    
                    // llamado a todos los parametros de mi proc almacendo. obj es lo que recibo
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);


                    // parametros de salida de mi proc almacenado
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;


                    //ese comando que estamos por ejecutar es de un proc almacenado
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value); // obtenemos el valor y se almacena en idautogenerado
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString(); // almacena el valor del prc almacenado. Mensaje es el parametro de salida
                }
            }
            catch (Exception ex)
            {

                idautogenerado = 0;
                Mensaje = ex.Message;

            }

            return idautogenerado; // devuelve un entero.

        }


        //------------------------------------------------------------------------------------//

        //Editar


        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool resultado = false; // seteo como falso
            Mensaje = string.Empty; // seteo como vacio


            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) // conexion
                {

                    SqlCommand cmd = new SqlCommand("sp_EditUsuario", oconexion); // llamo al proc almacenado



                    // llamado a todos los parametros de mi proc almacendo. obj es lo que recibo
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);

                    // parametros de salida de mi proc almacenado
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;


                    //ese comando que estamos por ejecutar es de un proc almacenado
                    cmd.CommandType = CommandType.StoredProcedure;


                    oconexion.Open();

                    cmd.ExecuteNonQuery();


                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value); // obtenemos el valor y se almacena en idautogenerado
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString(); // almacena el valor del prc almacenado. Mensaje es el parametro de salida





                }




            }
            catch (Exception ex)
            {

                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;


        }


        //------------------------------------------------------------------------------------//

        //Eliminar
        
        public bool Eliminar(int id, out string Mensaje) // recibe el id del user a eliminar
        {

            bool resultado = false;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) // conexion
                {

                    // ejecuto una linea de comando donde:
                    // eliminar el primer usuario que encuentres cuyo id sea igual al id que pasa por parametro

                    SqlCommand cmd = new SqlCommand("delete top (1) from usuario where IdUsuario = @id", oconexion);

                    cmd.Parameters.AddWithValue("@id", id); // asigno el parametro valor y el id que recibo 
                    cmd.CommandType = CommandType.Text; // comando tipo texto

                    oconexion.Open(); // abro conexion

                    // ejecuto, si el total de filas afectadas es mayor a 0 entonces se elimino bien true, sino hubo error
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false; 

                }


            }
            catch (Exception ex)
            {
                // por si hay error
                resultado = false;
                Mensaje = ex.Message; // el mensaje a mostrar


            }

            return resultado; // resultado final obtenido



        }




    }
}
