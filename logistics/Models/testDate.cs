using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace logistics.Models
{
    public class testDate
    {
        public List<HttpData> data { get; set; }
    }

    public class HttpData
    {
        public string createDate { get; set; }
        public string jobName { get; set; }
        public string jobOther { get; set; }
    }
}
