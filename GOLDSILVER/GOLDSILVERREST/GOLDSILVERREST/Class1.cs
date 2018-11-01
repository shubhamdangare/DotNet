using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GOLDSILVERREST
{
    public enum httpVerb{
        GET,
        POST

    }
    public class Metal
    {
        public int Price { get; set; }
        public String Name { get; set; }
    }


    class Class1
    {
       public  int Goldprice;
       public  int SilverPrice;
        public string endpoint { get; set; }
        public httpVerb httpMethod { get; set; }

        public Class1()
        {

            endpoint = string.Empty;
            httpMethod = httpVerb.GET;

        }

        public String makeRequest()
        {
            WebClient web = new WebClient();
            string response = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = httpMethod.ToString();

            var json = web.DownloadData(endpoint);
            var json1 = web.DownloadString(endpoint);

            using ( HttpWebResponse respose = (HttpWebResponse)request.GetResponse())
            {
                if(respose.StatusCode != HttpStatusCode.OK)
                {

                    throw new ApplicationException("error code" + respose.StatusCode.ToString());
                }

                ////parese data here 
                using(Stream res = respose.GetResponseStream())
                {

                    if(res != null)
                    {
                        using(StreamReader read = new StreamReader(res))
                        {
                            response = read.ReadToEnd();

                        }

                    }
                }
            }
            List<Metal> items = JsonConvert.DeserializeObject<List<Metal>>(response);

            bool flag = true;
            foreach (var item in items)
            {
                if (flag == true)
                {
                    Goldprice = Convert.ToInt32(item.Price);
                    flag = false;
                }
                else
                {

                    SilverPrice = Convert.ToInt32(item.Price);
                }
            }

            return response;

        }





    }
}
