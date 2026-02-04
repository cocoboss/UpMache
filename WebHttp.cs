using NPOI.SS.Formula.Functions;
using Simple_Capacitor.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Capacitor
{
    public class WebHttp
    {
        public CookieContainer cookie=new CookieContainer();
        #region POST
        public string SendDataByPost(string url,string data)
        {
            string retString = "";
            HttpWebRequest request=(HttpWebRequest)WebRequest.Create(url);
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            if(cookie.Count==0 )
            {
                request.CookieContainer=new CookieContainer();
                cookie=request.CookieContainer;
            }else
            {
                request.CookieContainer=cookie;
            }
            try
            {
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                Stream myRequeststream = request.GetRequestStream();
                myRequeststream.Write(byteArray, 0, byteArray.Length);
                myRequeststream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream=response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream,Encoding.UTF8);
                retString = myStreamReader.ReadToEnd();
                myResponseStream.Close ();
                myStreamReader.Close ();
            }
            catch (Exception e) 
            { 
                retString = e.Message;
            }
            return retString;
        }
        #endregion
    }
}
