using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tests.Threading.Shared
{
    public static class DeepClone
    {
        public static T Clone<T>(this T a)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}