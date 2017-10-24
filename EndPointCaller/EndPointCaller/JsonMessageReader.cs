using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EndPointCaller
{
    public class JsonMessageReader
    {
        private string _jsonMessageFile;
        public JsonMessageReader()
        {
            _jsonMessageFile = ConfigurationManager.AppSettings["JasonFileName"];

        }
        public List<string> GetJasonMessages()
        {
            Console.WriteLine("ReadingJason File" );
            var jsonMessage = new List<string>();
            using (var r = new StreamReader(_jsonMessageFile))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {
                    var jasonString = JsonConvert.SerializeObject(item);
                    jsonMessage.Add(jasonString);
                }
            }
            return jsonMessage;
        }

        public List<JsonEndPointTemplate> GetJasonMessagesObjects()
        {
            Console.WriteLine("ReadingJason File");
            var jsonMessage = new List<JsonEndPointTemplate>();
            using (var r = new StreamReader(_jsonMessageFile))
            {
                string json = r.ReadToEnd();
                var jsonEndPointObjects = JsonConvert.DeserializeObject<IEnumerable<JsonEndPointTemplate>>(json);
                if (jsonEndPointObjects.Any())
                    jsonMessage.AddRange(jsonEndPointObjects);
            }
            return jsonMessage;
        }

    }

    public class JsonEndPointTemplate
    {
        public string EndPointURI { get; set; }
        public object EndPointBody { get; set; }
    }
}
