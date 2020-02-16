using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace logistics.Models
{
    public class MessageModel<T>
    {
        public bool success { get; set; } = false;
        public string msg { get; set; } = "请求异常";
        public T response { get; set; }
    }
}
