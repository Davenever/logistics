using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace logistics.Models
{
    /// <summary>
    /// 物流返回的信息类
    /// </summary>
    public class ResponseData
    {
        public string message { get; set; }
        public string nu { get; set; }
        public string ischeck { get; set; }
        public string condition { get; set; }
        public string com { get; set; }
        public string status { get; set; }
        public string state { get; set; }
        public List<data> data { get; set; }
    }

    public class data
    {
        public string time { get; set; }
        public string ftime { get; set; }
        public string context { get; set; }
    }
}
