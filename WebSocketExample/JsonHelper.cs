using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WebSocketExample
{
    public class JsonHelper
    {
        public static T DeserializeJson<T>(string str)
        {
            JavaScriptSerializer j = new JavaScriptSerializer();

            try
            {
                return j.Deserialize<T>(str);
            }
            catch
            {
                return default(T);
            }
        }

        public static string SerializeJson<T>(T t)
        {
            JavaScriptSerializer j = new JavaScriptSerializer();

            try
            {
                return j.Serialize(t);
            }
            catch
            {
                return null;
            }
        }
    }
}