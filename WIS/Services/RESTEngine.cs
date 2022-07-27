using System.Net;
using System.IO;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Microsoft.AppCenter.Analytics;

namespace WIS.Services
{

    public delegate void stringDelegate(string str);
    public delegate void responseDelegate<T>(T obj);
    public delegate void readyDelegate();
    public delegate void sucessDelegate(bool response);

    public class RESTEngine
    {
        public static void HttpGet(stringDelegate del, string url, Dictionary<string, string> headers = null, bool returnNullOnError = false)
        {

            Analytics.TrackEvent("GET " + url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            if (headers != null)
            {
                foreach (string key in headers.Keys)
                    request.Headers[key] = headers[key];
            }
            request.BeginGetResponse((IAsyncResult ar) =>
            {
                HttpWebRequest httpWebRequest = null;
                HttpWebResponse httpWebResponse = null;
                httpWebRequest = (HttpWebRequest)ar.AsyncState;
                try
                {
                    httpWebResponse = (HttpWebResponse)httpWebRequest.EndGetResponse(ar);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() => {

                        Application.Current.MainPage.DisplayAlert("ERROR", "error happened(" + url + ") (" + ex.Message + ")", "OK");
                        if (returnNullOnError)
                            del(null);
                    });
                    return;
                }
                string result = "";
                using (var reader = new StreamReader(httpWebResponse.GetResponseStream()))
                    result = reader.ReadToEnd();
                Dictionary<string, object> res = JsonConvert.DeserializeObject<Dictionary<string, object>>(result,
                                   new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                if (res["result"].ToString() == "KO")
                {
                    Device.BeginInvokeOnMainThread(() => {
                        if (res.ContainsKey("message"))
                            Application.Current.MainPage.DisplayAlert("ERROR", res["message"].ToString(), "OK");
                        else
                            Application.Current.MainPage.DisplayAlert("ERROR", "API call error (" + url + ")", "OK");
                        if (returnNullOnError)
                            del(null);
                    });
                }
                else
                {
                    if (res.ContainsKey("data"))
                        del(res["data"].ToString());
                    else
                        del("");
                }

            }, request);
        }



        public static void HttpPost(stringDelegate del, string url, Object objectToSend, Dictionary<string, string> headers = null, bool returnNullOnError = false)
        {
            Analytics.TrackEvent("POST " + url, (Dictionary<string, string>)objectToSend);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            if (headers != null)
            {
                foreach (string key in headers.Keys)
                    request.Headers[key] = headers[key];
            }


            request.BeginGetRequestStream((IAsyncResult ar) =>
            {
                try
                {
                    HttpWebRequest req1 = (HttpWebRequest)ar.AsyncState;
                    Stream writeStream = request.EndGetRequestStream(ar);
                    string str = JsonConvert.SerializeObject(objectToSend,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    byte[] bytes = Encoding.UTF8.GetBytes(str);
                    writeStream.Write(bytes, 0, bytes.Length);
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
                                if (res["result"].ToString() == "KO")
                                {
                                    Device.BeginInvokeOnMainThread(() => {
                                        if (res.ContainsKey("message"))
                                            Application.Current.MainPage.DisplayAlert("ERROR", res["message"].ToString(), "OK");
                                        else
                                            Application.Current.MainPage.DisplayAlert("ERROR", "API call error (" + url + ")", "OK");
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

        public static void HttpPut(stringDelegate del, string url, Object objectToSend, Dictionary<string, string> headers = null, bool returnNullOnError = false)
        {
            Analytics.TrackEvent("PUT " + url, (Dictionary<string, string>)objectToSend);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/json";

            if (headers != null)
            {
                foreach (string key in headers.Keys)
                    request.Headers[key] = headers[key];
            }
            request.BeginGetRequestStream((IAsyncResult ar) =>
            {
                try
                {
                    HttpWebRequest req1 = (HttpWebRequest)ar.AsyncState;
                    Stream writeStream = request.EndGetRequestStream(ar);
                    string str = JsonConvert.SerializeObject(objectToSend,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    byte[] bytes = Encoding.UTF8.GetBytes(str);
                    writeStream.Write(bytes, 0, bytes.Length);
                    writeStream.Flush();
                    writeStream.Dispose();
                    request.BeginGetResponse((IAsyncResult ar2) =>
                    {
                        HttpWebResponse response2 = null;
                        try
                        {
                            HttpWebRequest req2 = (HttpWebRequest)ar2.AsyncState;
                            response2 = (HttpWebResponse)request.EndGetResponse(ar2);
                        }
                        catch (Exception ex)
                        {
                            Device.BeginInvokeOnMainThread(() => {
                                Application.Current.MainPage.DisplayAlert("ERROR", "error happened(" + url + ") (" + ex.Message + ")", "OK");
                                if (returnNullOnError)
                                    del(null);
                            });
                            return;
                        }
                        using (StreamReader httpWebStreamReader = new StreamReader(response2.GetResponseStream()))
                        {
                            string resp = httpWebStreamReader.ReadToEnd();
                            Dictionary<string, object> res = JsonConvert.DeserializeObject<Dictionary<string, object>>(resp,
                                   new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                            if (res["result"].ToString() == "KO")
                            {
                                Device.BeginInvokeOnMainThread(() => {
                                    if (res.ContainsKey("message"))
                                        Application.Current.MainPage.DisplayAlert("ERROR", res["message"].ToString(), "OK");
                                    else
                                        Application.Current.MainPage.DisplayAlert("ERROR", "API call error (" + url + ")", "OK");
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

        public static void HttpDeleteWithData(stringDelegate del, string url, Object objectToSend, Dictionary<string, string> headers = null, bool returnNullOnError = false)
        {
            Analytics.TrackEvent("DELETE " + url, (Dictionary<string, string>)objectToSend);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "DELETE";
            request.ContentType = "application/json";

            if (headers != null)
            {
                foreach (string key in headers.Keys)
                    request.Headers[key] = headers[key];
            }

            request.BeginGetRequestStream((IAsyncResult ar) =>
            {
                try
                {
                    HttpWebRequest req1 = (HttpWebRequest)ar.AsyncState;
                    Stream writeStream = request.EndGetRequestStream(ar);
                    string str = JsonConvert.SerializeObject(objectToSend,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    byte[] bytes = Encoding.UTF8.GetBytes(str);
                    writeStream.Write(bytes, 0, bytes.Length);
                    writeStream.Flush();
                    writeStream.Dispose();

                    request.BeginGetResponse((IAsyncResult ar2) =>
                    {
                        HttpWebRequest req2 = (HttpWebRequest)ar2.AsyncState;

                        HttpWebResponse response2 = null;
                        try
                        {
                            response2 = (HttpWebResponse)request.EndGetResponse(ar2);
                        }
                        catch (Exception ex)
                        {
                            Device.BeginInvokeOnMainThread(() => {
                                Application.Current.MainPage.DisplayAlert("ERROR", "error happened(" + url + ") (" + ex.Message + ")", "OK");
                                if (returnNullOnError)
                                    del(null);
                            });
                            return;
                        }
                        using (StreamReader httpWebStreamReader = new StreamReader(response2.GetResponseStream()))
                        {
                            string resp = httpWebStreamReader.ReadToEnd();
                            Dictionary<string, object> res = JsonConvert.DeserializeObject<Dictionary<string, object>>(resp,
                                   new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                            if (res["result"].ToString() == "KO")
                            {
                                Device.BeginInvokeOnMainThread(() => {
                                    if (res.ContainsKey("message"))
                                        Application.Current.MainPage.DisplayAlert("ERROR", res["message"].ToString(), "OK");
                                    else
                                        Application.Current.MainPage.DisplayAlert("ERROR", "API call error (" + url + ")", "OK");
                                });
                            }
                            else
                            {
                                if (res.ContainsKey("data"))
                                    del(res["data"].ToString());
                                else
                                    del("");

                            }
                        }
                    }, req1);
                }
                catch (Exception)
                {
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", "server unreachable (" + url + ")", "OK");
                    });
                }
            }, request);
        }

        public static void HttpDelete(stringDelegate del, string url, Dictionary<string, string> headers = null, bool returnNullOnError = false)
        {
            Analytics.TrackEvent("DELETE " + url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "DELETE";
            if (headers != null)
            {
                foreach (string key in headers.Keys)
                    request.Headers[key] = headers[key];
            }


            request.BeginGetResponse((IAsyncResult ar) =>
            {
                HttpWebRequest httpWebRequest = null;
                HttpWebResponse httpWebResponse = null;
                httpWebRequest = (HttpWebRequest)ar.AsyncState;

                try
                {
                    httpWebResponse = (HttpWebResponse)httpWebRequest.EndGetResponse(ar);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", "error happened(" + url + ") (" + ex.Message + ")", "OK");
                    });
                    return;
                }
                string result = "";
                using (var reader = new StreamReader(httpWebResponse.GetResponseStream()))
                    result = reader.ReadToEnd();
                Dictionary<string, object> res = JsonConvert.DeserializeObject<Dictionary<string, object>>(result,
                                   new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                if (res["result"].ToString() == "KO")
                {

                    Device.BeginInvokeOnMainThread(() => {
                        if (res.ContainsKey("message"))
                            Application.Current.MainPage.DisplayAlert("ERROR", res["message"].ToString(), "OK");
                        else
                            Application.Current.MainPage.DisplayAlert("ERROR", "API call error (" + url + ")", "OK");
                        if (returnNullOnError)
                            del(null);
                    });

                }
                else
                {
                    if (res.ContainsKey("data"))
                        del(res["data"].ToString());
                    else
                        del("");
                }


            }, request);
        }

       

    }
}
