using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using WIS.Models;
using Xamarin.Forms;

namespace WIS.Services
{

    public static class FormEngine
    {
        private static readonly Encoding encoding = Encoding.UTF8;

        private static Dictionary<string, string> errorCode = new Dictionary<string, string>()
        {
            { "1","Invalid Hash, Hash generated is incorrect and not following the guideline to generate the Hash." },
            { "2","Invalid Transaction ID, unsupported characters included in Transaction ID" },
            {"3","Invalid Amount format need not include decimal point for KHR transaction. example for USD 100.00 for KHR 100" },
            {"4","Duplicate Transaction ID, the transaction ID already exists in PayWay, generate new transaction" },
            {"5","Invalid Continue Success URL, (Domain must be registered in PayWay backend to use success URL)" },
            {"6","Invalid Domain Name (Request originated from non-whitelisted domain need to register domain in PayWay backend)" },
            {"7","Invalid Return Param (String must be lesser than 500 chars)" },
            {"8","" },
            {"9","Invalid Limit Amount (The amount must be smaller than value that allowed in PayWay backend)"},
            {"10","Invalid Shipping Amount"},
            {"11","PayWay Server Side Error" },
            {"12","Invalid Currency Type (Merchant is allowed only one currency - USD or KHR)" },
            {"13","Invalid Item, value for items parameters not following the guideline to generate the base64 encoded array of item list." },
            {"14","" },
            {"15","Invalid Channel Values for parameter topup_channel" },
            {"16","Invalid First Name - unsupported special characters included in value" },
            {"17","Invalid Last Name" },
            {"18","Invalid Phone Number" },
            {"19","Invalid Email Address" },
            {"20","Required purchase details when checkout" },
            {"21","Expired production key" }
        };

        public static void MultipartFormDataPost(stringDelegate del, string url, Dictionary<string, object> postParameters,bool returnNullOnError = false)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;
            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);
            string userAgent = "application";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;
            request.UserAgent = userAgent;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;

            // You could add authentication here as well if needed:
            // request.PreAuthenticate = true;
            // request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            // request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("username" + ":" + "password")));

            // Send the form data to the request.
            request.BeginGetRequestStream((IAsyncResult ar) =>
            {
                try
                {
                    HttpWebRequest req1 = (HttpWebRequest)ar.AsyncState;
                    Stream writeStream = request.EndGetRequestStream(ar);
                                       
                    writeStream.Write(formData, 0, formData.Length);
                    writeStream.Flush();
                    writeStream.Dispose();
                    request.BeginGetResponse((IAsyncResult ar2) =>
                    {
                        try
                        {
                            HttpWebRequest req2 = (HttpWebRequest)ar2.AsyncState;
                            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar2);
                            using (StreamReader httpWebStreamReader = new StreamReader(response.GetResponseStream()))
                            {
                                string resp = httpWebStreamReader.ReadToEnd();

                                Dictionary<string, object> res = JsonConvert.DeserializeObject<Dictionary<string, object>>(resp);
                                ABASTATUS status = JsonConvert.DeserializeObject<ABASTATUS>(res["status"].ToString());
                                if (status.code != "00" )                                
                                {
                                    Device.BeginInvokeOnMainThread(() => {
                                        Application.Current.MainPage.DisplayAlert("Aba Error", errorCode[status.code], "OK");                                      
                                        if (returnNullOnError)
                                            del(null);
                                    });
                                }
                                else
                                {
                                    if (res.ContainsKey("data"))
                                        del(res["data"].ToString());
                                    else
                                    {
                                        del("");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Device.BeginInvokeOnMainThread(() => {
                                Application.Current.MainPage.DisplayAlert("ERROR", "error happened(" + url + ") (" + ex.Message + ")", "OK");
                                if (returnNullOnError)
                                    del(null);
                            });
                        }
                    }, req1);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", "error happened(" + url + ") (" + ex.Message + ")", "OK");
                        if (returnNullOnError)
                            del(null);
                    });

                }
            }, request);

        }

        public static HttpWebResponse _MultipartFormDataPost(string postUrl, string userAgent, Dictionary<string, object> postParameters)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;
            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, userAgent, contentType, formData);
        }
        private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;
            request.UserAgent = userAgent;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;

            // You could add authentication here as well if needed:
            // request.PreAuthenticate = true;
            // request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            // request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("username" + ":" + "password")));

            // Send the form data to the request.
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData,0 , formData.Length);
                requestStream.Close();
            }

            return request.GetResponse() as HttpWebResponse;
        }

        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
                // Skip it on the first parameter, add it to subsequent parameters.
                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(encoding.GetBytes(header),0 , encoding.GetByteCount(header));

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileToUpload.File,0 , fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }
    }
}

