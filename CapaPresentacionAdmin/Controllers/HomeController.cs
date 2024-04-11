﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;

using CapaNegocio;


namespace CapaPresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Usuarios()
        {
            return View();
        }



        [HttpGet]
        public JsonResult ListarUsuarios() { 
        
            List<Usuario> oLista = new List<Usuario>();

            oLista = new CN_Usuarios().Listar();


            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        
        
        }


        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {

            // declaron variables de resultados a retornar
            object resultado;

            string mensaje = string.Empty;

            // Logica

            if (objeto.IdUsuario == 0) // en caso de que sea 0 es un usuario nuevo a registrar, sino es editar
            {

                resultado = new CN_Usuarios().Registrar(objeto, out mensaje);



            }
            else
            {
                resultado = new CN_Usuarios().Editar(objeto, out mensaje);
            }

            //  devuelve un json 

            return Json(new {resultado = resultado, mensaje = mensaje}, JsonRequestBehavior.AllowGet);





        }


        [HttpPost]
        public JsonResult EliminarUsuario(int id) // id del usuario a eliminar
        {

            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuarios().Eliminar(id,out mensaje); // el id, y el mensaje en caso de error

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }



    }

}