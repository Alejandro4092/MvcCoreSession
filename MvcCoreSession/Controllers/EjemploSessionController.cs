using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
                    //CONVERTIMOS BYTES EA OBJECT
                    Mascota mascota = (Mascota)
                        HelperBinarySession.ByteToObject(data);
                    // para presentarlo de forma visual,lo enviamos a viewdata
                    ViewData["MASCOTA"]=mascota;
                }
            }
            return View();
        }
    }
}
