using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarposTestApp
{
    class ResponseInfo
    {
        public string Url { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string StatusText { get; set; }
        public string Method { get; set; }
        public string Payload { get; set; }
        public string Time { get; set; }
    }

}
