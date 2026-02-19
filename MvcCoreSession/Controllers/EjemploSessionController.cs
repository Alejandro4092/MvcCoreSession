using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Extensions;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;
using System;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        HelperSessionContextAccessor helper;
        public EjemploSessionController(HelperSessionContextAccessor helper)
        {
            this.helper = helper;
        }
        public IActionResult Index(string accion)
        {
            List<Mascota> mascotas = this.helper.GetMascotasSession();
            return View(mascotas);
        }
        public IActionResult SessionSimple(string accion)
        {
            if(accion!= null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //Guardamos datos en session
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora", DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"] = "Datos almacenados en Session";
                }else if (accion.ToLower() == "mostrar")
                {
                    //rescuperamos los datos de session
                    ViewData["NOMBRE"] = HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] = HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }
        public IActionResult SessionMascotaBytes(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Wall-E";
                    mascota.Raza = "Cleaner";
                    mascota.Edad = 18;
                    //para almacenar la mascota en session debemos convertilo a byte[]
                    byte[] data = HelperBinarySession.ObjetToByte(mascota);
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"] = "Mascota almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    //Recuperamos los datos de mascota
                    //eN bytes que tenemos ensession
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    if (data != null)
                    {
                        //CONVERTIMOS BYTES A OBJECT
                        Mascota mascota = (Mascota)
                            HelperBinarySession.ByteToObject(data);
                        // para presentarlo de forma visual,lo enviamos a viewdata
                        ViewData["MASCOTA"] = mascota;
                    }
                    else
                    {
                        ViewData["MENSAJE"] = "No hay mascota en Session";
                    }
                }
            }
            return View();
        }
        public IActionResult SessionMascotaCollectionBytes(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotasList = new List<Mascota>
                   {
                       new Mascota{Nombre="Nala",Raza="Leona",Edad=21 },
                       new Mascota{Nombre="Sebastian",Raza="Cangrejo",Edad=24 },
                       new Mascota{Nombre="Rafiki",Raza="Brujo",Edad=23 },
                       new Mascota{Nombre="Olaf",Raza="Muñeco",Edad=14 }
                   };
                    byte[] data = HelperBinarySession.ObjetToByte(mascotasList);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["MENSAJE"] = "Coleccion almacenada correctamente";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotasList = (List<Mascota>)
                        HelperBinarySession.ByteToObject(data);
                    return View(mascotasList);
                }
            }
            return View();
        }
        public IActionResult SessionMascotaJson(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION 
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Eva";
                    mascota.Raza = "Exploradora";
                    mascota.Edad = 18;
                    //QUEREMOS GUARDAR EL OBJETO MASCOTA COMO STRING 
                    //EN SESSION 
                    string mascotaJson =
        HelperJsonSession.SerializeObject<Mascota>(mascota);
                    HttpContext.Session.SetString("MASCOTAJSON", mascotaJson);
                    ViewData["MENSAJE"] = "Mascota almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    string jsonMascota =
                    HttpContext.Session.GetString("MASCOTAJSON");
                    Mascota mascota =
                    HelperJsonSession.DeserializeObject<Mascota>(jsonMascota);
                    //PARA REPRESENTARLO DE FORMA VISUAL, LO ENVIAMOS 
                    //A VIEWDATA 
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }


        public IActionResult SessionMascotaGeneric(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION 
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Fujur";
                    mascota.Raza = "Dragón";
                    mascota.Edad = 33;
                    HttpContext.Session.setObject("MASCOTAGENERIC", mascota);
                    ViewData["MENSAJE"] = "Mascota almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    //PARA REPRESENTARLO DE FORMA VISUAL, LO ENVIAMOS 
                    //A VIEWDATA 
                    Mascota mascota =
        HttpContext.Session.GetObject<Mascota>("MASCOTAGENERIC");
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }
        public IActionResult SessionMascotaCollectionGeneric(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotasList = new List<Mascota>
                   {
                       new Mascota{Nombre="Nala",Raza="Leona",Edad=21 },
                       new Mascota{Nombre="Sebastian",Raza="Cangrejo",Edad=24 },
                       new Mascota{Nombre="Rafiki",Raza="Brujo",Edad=23 },
                       new Mascota{Nombre="Olaf",Raza="Muñeco",Edad=14 }
                   };
                    HttpContext.Session.setObject("MASCOTASLIST", mascotasList);
                    ViewData["MENSAJE"] = "Coleccion almacenada correctamente";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    List<Mascota> mascotasList = HttpContext.Session.GetObject<List<Mascota>>("MASCOTASLIST");
                    return View(mascotasList);
                }
            }
            return View();
        }
    }
}
