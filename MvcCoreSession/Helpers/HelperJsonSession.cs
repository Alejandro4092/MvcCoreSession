using Newtonsoft.Json;

namespace MvcCoreSession.Helpers
{
    public class HelperJsonSession
    {
        //vamos a almacenar datos en session mediante el metodo getString,setString
        public static string SerializeObject<T>(T data)
        {
            //cnvertimos el objeto a string mediante NEWTON
            string json = JsonConvert.SerializeObject(data);
            return json;
        }
        //RECIBIMOS UN STRING Y DEVOLVER CUALQUIER OBJETO
        public static T DeserializeObject<T>(string data)
        {
            //MEDIANTE NEWTON DESELIZAMOS EL OBJETO
            T objeto = JsonConvert.DeserializeObject<T>(data);
            return objeto;
        }

    }
}
