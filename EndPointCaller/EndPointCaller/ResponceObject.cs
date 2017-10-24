using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPointCaller
{


    public class ApiRespone
    {

        public ApiRespone(string uniqueId, string serialNo)
        {
            SerialNo = serialNo;
            UniqueId = uniqueId;
        }

        public string UniqueId { get; private set; }
        public string SerialNo { get; private set; }

    }

    public class Response
    {
        public string Value { get; set; }
    }

    public class ResponceObject : IResponse
    {
        public ApiRespone Data { get; set; }
        public Response Response { get; set; }
    }


}
