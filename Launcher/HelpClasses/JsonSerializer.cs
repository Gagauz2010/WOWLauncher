using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Launcher.HelpClasses
{
    class JsonSerializer<T>
    {
        private static readonly DataContractJsonSerializerSettings SerializerSettings =
            new DataContractJsonSerializerSettings()
            {
                UseSimpleDictionaryFormat = true
            };

        public static string ToString(T obj)
        {
            using (var msObj = new MemoryStream())
            {
                var js = new DataContractJsonSerializer(typeof(T), SerializerSettings);
                js.WriteObject(msObj, obj);
                msObj.Position = 0;
                using (var sr = new StreamReader(msObj))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static T FromString(string json)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                // Deserialization from JSON  
                var deserializer = new DataContractJsonSerializer(typeof(T), SerializerSettings);
                var obj = (T)deserializer.ReadObject(ms);
                return obj;
            }
        }
    }
}
