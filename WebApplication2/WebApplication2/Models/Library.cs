using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebApplication2.Models
{
    public class Lib : Sqlf, Ilib
    {
        public override string CheckAuthorization(string TokenStr = "")
        {
            string Key = string.Empty;

            try
            {
                Key = HttpContext.Current.Request.Headers["SuppId"];
                if (Key == "" || Key == null)
                    return Key.ToUpper();
                else
                    return Key.ToUpper();
            }
            catch
            {
                return Key;
            }
        }

        public override void LogError(Exception ex)
        {
            if (!Directory.Exists("~/ErrorLog"))
                Directory.CreateDirectory("~/ErrorLog/");

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        public override void WriteLog(string message, string lFileName = "ErrorLog.txt")
        {
            if (!Directory.Exists("~/ErrorLog"))
                Directory.CreateDirectory("~/ErrorLog/");

            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/ErrorLog/" + lFileName);
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
                writer.Flush();
            }
        }

    }
    public static class HttpRequestMessageExtensions
    {

        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            string message = "Url\t:\t" + request.RequestUri.ToString();
            message += "\r\nIP Address\t:\t" + request.GetClientIpAddress();
            message += "\r\nCertificate\t:\t" + request.GetClientCertificate().ToString();
            message += "\r\nConfiguration\t:\t" + request.GetClientIpAddress().ToString();
            message += "\r\nContent\t:\t" + request.Content;
            message += "\r\nHeader\t:\t" + request.Headers;
            message += "\r\nMethod\t:\t" + request.Method;
            message += "\r\nProperties\t:\t" + request.Properties;
            message += "\r\nRequest\t:\t" + request.ToString();

            //WriteLog(message,"Errlog" + request.GetClientIpAddress());

            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            return null;
        }
    }
}