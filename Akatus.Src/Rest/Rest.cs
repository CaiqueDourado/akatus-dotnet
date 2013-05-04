/*
 * Akatus API - C# Implementation
 * Caique Dourado
 * http://www.caiquedourado.com.br/
 * Date: 2013-02-26
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Text;
using System.Xml;

namespace Akatus
{
    public static class Rest
    {
        /*
        * Based on: Make Yahoo! Web Service REST Calls With C# - Yahoo! Developer Network
        * http://developer.yahoo.com/dotnet/howto-rest_cs.html
        * 
        */

        #region GET

        /// <summary>
        /// Makes an HTTP GET
        /// </summary>
        /// <param name="url">URL</param>
        public static string get(string url)
        {
            string result = null;

            try
            {
                //Create the web request  
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                //Get response  
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    //Get the response stream  
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    //Return data
                    result = reader.ReadToEnd();
                }  
            }
            catch (WebException ex)
            {
                //Handle Errors              
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        //Get StatusCode and StatusDescription
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = Akatus.Util.parseString(response.StatusDescription);

                        //Get the response stream  
                        StreamReader reader = new StreamReader(response.GetResponseStream());

                        //Return data
                        string errorMessage = reader.ReadToEnd();

                        //Throw Exception
                        throw new RestExcepction(errorMessage, statusCode, statusDescription);
                    }
                }
               
            }

            return result;
        }

        #endregion

        #region POST

        /// <summary>
        /// Makes an HTTP POST
        /// </summary>
        /// <param name="url">URL</param>
        public static string post(string url)
        {
            return post(url, new List<RestParameter>());
        }

        /// <summary>
        /// Makes an HTTP POST
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="parameters">Parameters</param>
        public static string post(string url, List<RestParameter> parameters)
        {
            //Build Parameters
            StringBuilder data = new StringBuilder();

            for (int x = 0; x < parameters.Count; x++)
            {
                if (x != 0)
                    data.Append("&");

                data.AppendFormat("{0}={1}", System.Web.HttpUtility.UrlEncode(parameters[x].Name), System.Web.HttpUtility.UrlEncode(Util.parseString(parameters[x].Value)));
            }

            return post(url, data.ToString());
        }

        /// <summary>
        /// Makes an HTTP POST
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="data">Data</param>
        public static string post(string url, string data)
        {
            string result = null;

            try {

                //We use the HttpUtility class from the System.Web namespace  
                Uri address = new Uri(url);  

                //Create the web request  
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;  

                //Set type to POST  
                request.Method = "POST";  
                request.ContentType = "application/x-www-form-urlencoded";  

                //Create a byte array of the data we want to send  
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(data);  

                //Set the content length in the request headers  
                request.ContentLength = byteData.Length;  

                //Write data  
                using (Stream postStream = request.GetRequestStream())  
                {  
                    postStream.Write(byteData, 0, byteData.Length);  
                }

                //Get response  
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)  
                {
                    //Status
                    int statusCode = (int)response.StatusCode;
                    string statusDescription = Util.parseString(response.StatusDescription);

                    //Get the response stream  
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    //Return data
                    result = reader.ReadToEnd();
                }

            }
            catch (WebException ex)
            {
                //Handle Errors              
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        //Get StatusCode and StatusDescription
                        int statusCode = (int)response.StatusCode;
                        string statusDescription =  Akatus.Util.parseString(response.StatusDescription);

                        //Get the response stream  
                        StreamReader reader = new StreamReader(response.GetResponseStream());

                        //Return data
                        string errorMessage = reader.ReadToEnd();

                        //Throw Exception
                        throw new RestExcepction(errorMessage, statusCode, statusDescription);
                    }
                }
            }

            return result;
        }

        #endregion

        #region PUT

        /// <summary>
        /// Makes an HTTP PUT
        /// </summary>
        /// <param name="url">URL</param>
        public static string put(string url)
        {
            return put(url, new  List<RestParameter>());
        }

        /// <summary>
        /// Makes an HTTP PUT
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="parameters">Parameters</param>
        public static string put(string url, List<RestParameter> parameters)
        {
            //Add parameter '_method=put'
            parameters.Add(new RestParameter("_method", "put"));

            //Make HTTP Post
            return post(url, parameters);
        }

        #endregion
    }
}
