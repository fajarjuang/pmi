using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMI.Application.Mvc.Controller.Extension
{
    public static class ServerExtension
    {
        public static string ReverseMapPath(this HttpServerUtilityBase Server, string path)
        {
            string appPath = Server.MapPath("~");
            return String.Format("~/{0}", path.Replace(appPath, "").Replace("\\", "/"));
        }
    }
}