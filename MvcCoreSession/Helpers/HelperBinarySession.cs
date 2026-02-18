using System.Runtime.Serialization.Formatters.Binary;

namespace MvcCoreSession.Helpers
{
    public class HelperBinarySession
    {
        //VAMOS A CREAR LOS METODOS DE TIPO static
        //porque para convertir no voy a utilizar nada de esta clase solo la funcionalidad
        public static byte[] ObjetToByte(Object objeto)
        {

            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream= new MemoryStream())
            {
                formatter.Serialize(stream, objeto);
                return stream.ToArray();
            }
        }
        //Convertimps de BYTE[] A OBJETO
        public static Object ByteToObject(byte[] data)
        {

            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Seek(0, SeekOrigin.Begin);
                Object objeto = (Object)
                    formatter.Deserialize(stream);
                return objeto;
            }
        }
    }
}
