using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using logistics.Models;
using HttpSender;

namespace logistics.Controllers
{
    public class LogisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询物流,返回Json
        /// </summary>
        /// <param name="Number">快递单号</param>
        /// <returns></returns>
        public async Task<MessageModel<testDate>> queryInformation(string Number = "")
        {
            string res = string.Empty;
            string msg = string.Empty;
            bool success = true;
            var requestList = new testDate();
            try
            {
                if (string.IsNullOrEmpty(Number))
                {
                    return new MessageModel<testDate>()
                    {
                        msg = "单号为空",
                        success = false
                    };
                }
                //将快递单号作为参数请求是那个快递
                var Response = Sender.Get($"http://www.kuaidi100.com/autonumber/autoComNum?text={Number}");
                Express express = JsonConvert.DeserializeObject<Express>(Response);
                string code = string.Empty;
                if (express.auto.Any())
                    code = express.auto[0].comCode;
                else
                    goto action;

                /* //参数
                 String param = "{\"com\":\"\",\"num\":\"\",\"from\":\"\",\"to\":\"\"}";
                 //反序列化
                 Param p = JsonConvert.DeserializeObject<Param>(param);*/
                //string param = string.Empty;
                Param p = new Param();
                p.com = code;
                p.num = Number;
                //序列化
                var param = JsonConvert.SerializeObject(p);
                String customer = "";
                String key = "";
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] InBytes = Encoding.GetEncoding("UTF-8").GetBytes(param + key + customer);
                byte[] OutBytes = md5.ComputeHash(InBytes);
                string OutString = "";
                for (int i = 0; i < OutBytes.Length; i++)
                {
                    OutString += OutBytes[i].ToString("x2");
                }
                String sign = OutString.ToUpper();

                res = Sender.Post("http://poll.kuaidi100.com/poll/query.do", $"customer={customer}&sign={sign}&param={param}");
                msg = "物流信息已获取";
                success = true;
                var list = JsonConvert.DeserializeObject<ResponseData>(res);
                if (list.data.Any())
                {
                    var rList = new List<HttpData>();
                    list.data.ForEach((m) =>
                    {
                        rList.Add(new HttpData { createDate = m.time, jobName = m.ftime, jobOther = m.context });
                    });
                    requestList = new testDate() { data = rList };
                }
            }
            catch (Exception)
            {
                msg = "单号有误";
                success = false;
            }
        action: return new MessageModel<testDate>
        {
            msg = msg,
            success = success,
            response = requestList
        };
        }
    }
}