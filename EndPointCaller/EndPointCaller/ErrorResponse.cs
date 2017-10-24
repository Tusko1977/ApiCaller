using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EndPointCaller
{
    public interface IResponse
    {
        
    }
    public class ErrorResponseResponse 
    {
        public string Value { get; set; }
    }

    public class ErrorMessage
    {
        public string Severity { get; set; }
        public string Message { get; set; }
    }

    public class ErrorObject : IResponse
    {
        
        public string headerReasonPhrase { get; set; }
        public ErrorResponseResponse Response { get; set; }
        public List<ErrorMessage> Messages { get; set; }

        public override string ToString()
        {
            return "the message in error is:-" + Messages[0].Message;
        }
    }

}
