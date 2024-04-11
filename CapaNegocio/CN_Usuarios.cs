using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios

    {

        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> Listar() { 
        
            return objCapaDato.Listar();
        
        
        }


        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            // condiciones a cumplir

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";

            }

            if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El Apellido del usuario no puede ser vacio";

            }

            if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo del usuario no puede ser vacio";

            }

            
            if (string.IsNullOrEmpty(Mensaje)) // si a pesar de pasar las condiciones sigue siendo vacio, no hay error
            {


                
                
                // Enviar correo.....




                string clave = "test123";
                obj.Clave = CN_Recursos.ConvertirSha256(clave); // uso el metodo en CN_Recursos y le paso la clave


                // llamado al metodo registrar de CD_Usuarios

                return objCapaDato.Registrar(obj, out Mensaje);


            }
            else
            {
                return 0; // no se creo usuario
            }



            





        }

        public bool Editar(Usuario obj, out string Mensaje)
        {


            Mensaje = string.Empty;


            // condiciones a cumplir

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";

            }

            if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El Apellido del usuario no puede ser vacio";

            }

            if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo del usuario no puede ser vacio";

            }



            if (string.IsNullOrEmpty(Mensaje)) // si a pesar de pasar las condiciones sigue siendo vacio, no hay error
            {
                return objCapaDato.Editar(obj, out Mensaje);



            }
            else
            {

                return false;

            }

            }



        public bool Eliminar(int id, out string Mensaje)
        {

            return objCapaDato.Eliminar(id, out Mensaje);

        }







    }
}
