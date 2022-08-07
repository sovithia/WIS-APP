using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using WIS.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WIS.Services
{
    public class DataService
    {
        private string BaseURL;
        private string aba_public_key = "d5e1722a2ef703c41ca0c9fbab63275fb67d5ca0";
        private string aba_merchant_id = "ec002318";
        //private string aba_baseurl = "https://checkout.payway.com.kh/api/payment-gateway/v1/payments/";
        private string aba_baseurl = "https://checkout-sandbox.payway.com.kh/api/payment-gateway/v1/payments/";
        private Dictionary<string, string> headers;

        public APPUSER CurrentUser { get; set; }
        
        private static DataService instance;
        public static DataService Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataService();
                return instance;
            }
        }

        public DataService()
        {
            string realServer = "http://sams.api.westec.com/api";
            //string realServer = "http://test.sams.api.westec.com/api";
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    this.BaseURL = "http://127.0.0.1:3333/api";
                else
                    this.BaseURL = realServer;
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    this.BaseURL = "http://10.0.2.2:3333/api";
                else
                    this.BaseURL = realServer;
            }

            this.BaseURL = realServer;
            this.headers = new Dictionary<string, string>();
        }

        public void PreRegister(PRESIGNUPREQUEST signup, responseDelegate<bool> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["usertype"] = signup.type;
            data["phone"] = signup.phone.Replace(" ", string.Empty);
            data["identifier"] = signup.identifier;
            data["birthdate"] = signup.birthdate;

            RESTEngine.HttpPost(result =>
            {
                if (result == null)
                    del(false);
                else
                    del(true);
            },
            this.BaseURL + "/preregister", data, this.headers, true);
        }

        public void Register(SIGNUPREQUEST signup, responseDelegate<bool> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["usertype"] = signup.type;
            data["phone"] = signup.phone.Replace(" ", string.Empty);
            data["identifier"] = signup.identifier;
            data["birthdate"] = signup.birthdate;
            data["password"] = signup.password;

            RESTEngine.HttpPost(result =>
            {
                if (result == null)
                    del(false);
                else
                    del(true);
            },
            this.BaseURL + "/register", data, null);
        }

        public void ForgotPassword(string phone, responseDelegate<bool> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["username"] = phone;
            RESTEngine.HttpPost(result =>
            {
                try
                {
                    del(result != null);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }

            },
            this.BaseURL + "/resetrequest", data, null);
        }

        public void EditPassword(string username, string newpassword, responseDelegate<bool> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["username"] = username;
            data["password"] = newpassword;

            RESTEngine.HttpPost(result =>
            {
                try
                {
                    Dictionary<string, string> res = JsonConvert.DeserializeObject<Dictionary<string, string>>(result,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    del(result != null);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }
            },
            this.BaseURL + "/changepassword", data, null);
        }

        public void Login(string phone, string password, responseDelegate<APPUSER> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["phone"] = phone;
            data["password"] = password;

            RESTEngine.HttpPost(data =>
            {
                if (data != null)
                {
                    Dictionary<string, object> json = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                    try
                    {
                        APPUSER user = JsonConvert.DeserializeObject<APPUSER>(json["user"].ToString(),
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        CurrentUser = user;
                        Dictionary<string, string> token = JsonConvert.DeserializeObject<Dictionary<string, string>>(json["token"].ToString());
                        headers["Authorization"] = "bearer " + token["token"];
                        del(user);
                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                        });
                    }

                }
                else
                    del(null);


            }, this.BaseURL + "/login", data, null, true);
        }

        // Attendance
        public void GetAttendance(responseDelegate<List<ABSENCE>> del)
        {
            RESTEngine.HttpGet(data =>
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject<List<ABSENCE>>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    del(obj);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }


            }, this.BaseURL + "/attendance", headers);
        }

        public void GetAttendanceHistory(responseDelegate<List<ABSENCE>> del)
        {
            RESTEngine.HttpGet(data =>
            {
                try
                {
                    del(JsonConvert.DeserializeObject<List<ABSENCE>>(data,
                         new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }


            }, this.BaseURL + "/attendancehistory", headers);
        }

        public void GetAttendanceDetails(string ID, responseDelegate<ABSENCE> del)
        {
            RESTEngine.HttpGet(data =>
            {
                try
                {
                    del(JsonConvert.DeserializeObject<ABSENCE>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }
            }, this.BaseURL + "/attendancedetails/" + ID, headers);
        }



        // Invoice
        public void GetInvoiceList(responseDelegate<List<Invoice>> del)
        {
            RESTEngine.HttpGet(data =>
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject<List<Invoice>>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    del(obj);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    }
                    );
                }

            }, this.BaseURL + "/invoice", headers);
        }

        // Invoice History
        public void GetInvoiceHistory(responseDelegate<List<Invoice>> del)
        {
            RESTEngine.HttpGet(data =>
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject<List<Invoice>>(data,
                         new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    del(obj);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }

            }, this.BaseURL + "/invoicehistory", headers);
        }

        public void GetInvoiceDetails(string invoiceID, responseDelegate<Invoice> del)
        {
            RESTEngine.HttpGet(data =>
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject<Invoice>(data,
                         new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    del(obj);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }

            }, this.BaseURL + "/invoicedetails/" + invoiceID, headers);
        }


        public void GetTest(responseDelegate<StudentSchedule> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            RESTEngine.HttpGet(data =>
            {
                try
                {
                    del(JsonConvert.DeserializeObject<StudentSchedule>(data,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }

            }, this.BaseURL + "/test", headers);
        }

        // Student Schedule
        public void GetStudentSchedule(responseDelegate<StudentSchedule> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            RESTEngine.HttpGet(data =>
            {
                try
                {
                    del(JsonConvert.DeserializeObject<StudentSchedule>(data,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }

            }, this.BaseURL + "/studentschedule", headers);
        }

        // Teacher Schedule
        public void GetTeacherSchedule(responseDelegate<TeacherSchedule> del)
        {

            RESTEngine.HttpGet(data =>
            {
                try
                {
                    del(JsonConvert.DeserializeObject<TeacherSchedule>(data,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }

            }, this.BaseURL + "/teacherschedule", headers);
        }

        // Parent Schedule (all children schedule
        public void GetParentSchedule(responseDelegate<Dictionary<string, StudentSchedule>> del)
        {
            RESTEngine.HttpGet(data =>
            {
                try
                {
                    del(JsonConvert.DeserializeObject<Dictionary<string, StudentSchedule>>(data,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }

            }, this.BaseURL + "/parentschedule", headers);
        }

        // Register Push
        public void RegisterFCMToken(string fcmtoken)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["token"] = fcmtoken;
            RESTEngine.HttpPost(data =>
            {
                if (data != null)
                    Console.WriteLine("fcm token registered");
            }, this.BaseURL + "/pushregister", data, null, true);
        }

        public void SendPushNotification(string title, string message, responseDelegate<bool> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["message"] = message;
            data["title"] = title;
            // TODO TRY TO DO IN POST
            RESTEngine.HttpGet(data =>
            {
                if (data != null)
                    del(true);
                else
                    del(false);
            }, this.BaseURL + "/pushsend?title=" + message + "&body=" + message, headers);
        }

        public static string HashToString(string message, byte[] key)
        {
            byte[] b = new HMACSHA512(key).ComputeHash(Encoding.UTF8.GetBytes(message));
            return Convert.ToBase64String(b);
        }

        public string GenerateTransactionId()
        {
            string ID = Preferences.Get("ID", "");
            string date = DateTime.Now.ToString("YYYYmmddHis");
            // ID-TIMESTAMP
            return ID + "_" + date;
        }

        public string GenerateRequesTime()
        {
            //YYYYmmddHis 20210123 23 45 59                          
            return DateTime.Now.ToString("YYYYmmddHis"); ;
        }

        public void RequestCreditCardPayment(responseDelegate<string> del, string amount, string firstname, string lastname, string phone, Invoice invoice)
        {
            // Generate post objects
            Dictionary<string, object> data = new Dictionary<string, object>();

            data["req_time"] = GenerateRequesTime();
            data["merchant_id"] = this.aba_merchant_id;
            data["tran_id"] = "TRX-" + invoice.ID;
            data["amount"] = amount;
            data["items"] = invoice.ABAItems();
            data["firstname"] = firstname;
            data["lastname"] = lastname;
            data["phone"] = phone;
            data["type"] = "purchase";
            data["payment_option"] = "cards";
            string ID = Preferences.Get("ID", "");
            string link = "westernschool://western.edu.kh?userid=" + ID + "&transactionid=" + data["tran_id"];
            string returnDeeplink = "{'ios_scheme':" + link + ",'android_scheme':'" + link + "'}";
            data["return_deeplink"] = Convert.ToBase64String(Encoding.ASCII.GetBytes(returnDeeplink));
            string toHash = data["req_time"].ToString()
                          + data["merchant_id"].ToString()
                          + data["tran_id"].ToString()
                          + data["amount"].ToString()
                          + data["items"].ToString()
                          + data["firstname"].ToString()
                          + data["lastname"].ToString()
                          + data["phone"].ToString()
                          + data["type"].ToString()
                          + data["payment_option"].ToString()
                          + data["return_deeplink"].ToString();
            ;
            data["hash"] = HashToString(toHash, Encoding.ASCII.GetBytes(this.aba_public_key));
            // Create request and receive response
            string url = this.aba_baseurl + "purchase";
            FormEngine.MultipartFormDataPost((response) =>
            {                
                del(response);
            }, url, data,true);


        }

        public void RequestABAPayment(responseDelegate<ABAPaymentRequestResponse> del, string amount,string firstname,string lastname,string phone,Invoice invoice)
        {
            // Generate post objects
            Dictionary<string, object> data = new Dictionary<string, object>();
            
            data["req_time"] = GenerateRequesTime();
            data["merchant_id"] = this.aba_merchant_id;
            data["tran_id"] = "TRX-" + invoice.ID; 
            data["amount"] = amount;
            data["items"] = invoice.ABAItems();
            data["firstname"] = firstname;
            data["lastname"] = lastname;
            data["phone"] = phone;
            data["type"] = "purchase";            
            data["payment_option"] = "abapay_deeplink";            
            string ID = Preferences.Get("ID", "");
            string link = "westernschool://western.edu.kh?userid=" + ID + "&transactionid=" + data["tran_id"];
            string returnDeeplink = "{'ios_scheme':" + link + ",'android_scheme':'" + link + "'}";                                      
            data["return_deeplink"] = Convert.ToBase64String(Encoding.ASCII.GetBytes(returnDeeplink));
            string toHash = data["req_time"].ToString()
                          + data["merchant_id"].ToString()
                          + data["tran_id"].ToString()
                          + data["amount"].ToString()
                          + data["items"].ToString()
                          + data["firstname"].ToString()
                          + data["lastname"].ToString()
                          + data["phone"].ToString()
                          + data["type"].ToString()
                          + data["payment_option"].ToString()
                          + data["return_deeplink"].ToString();
                          ;
            data["hash"] = HashToString(toHash, Encoding.ASCII.GetBytes(this.aba_public_key));
            // Create request and receive response
            string url = this.aba_baseurl + "purchase";
            FormEngine.MultipartFormDataPost((response) =>
            {                
                ABAPaymentRequestResponse responseData = JsonConvert.DeserializeObject<ABAPaymentRequestResponse>(response,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                del(responseData);
            }, url, data);

          
        }
        
        public void ABAPaymentSearch(responseDelegate<List<ABATransaction>> del,string fromDate,string toDate,string fromAmount,string toAmount, string status){
            Dictionary<string, object> data = new Dictionary<string, object>();            
            data["req_time"] = GenerateRequesTime();
            data["merchant_id"] = aba_merchant_id;
            data["from_date"] = fromDate;
            data["to_date"] = toDate;
            data["from_amount"] = fromDate;
            data["to_amount"] = toDate;
            data["status"] = (status == "ALL") ? "" : status;

            string toHash = data["req_time"].ToString()
                          + data["merchant_id"].ToString()                          
                          + data["from_date"].ToString()
                          + data["to_date"].ToString()
                          + data["from_amount"].ToString()
                          + data["to_amount"].ToString()
                          + data["status"].ToString();
            data["hash"] = HashToString(toHash, Encoding.ASCII.GetBytes(this.aba_public_key));

            string url = this.aba_baseurl + "transaction-list";
            FormEngine.MultipartFormDataPost((response) =>
            {
                if (response == null)
                    del(null);
                else
                {
                    List<ABATransaction> transactions = JsonConvert.DeserializeObject<List<ABATransaction>>(response,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    del(transactions);
                }
                
                
            }, url, data);          
        }

        public void ABATransactionCheck(responseDelegate<ABAPaymentStatus> del,Invoice invoice)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["req_time"] = GenerateRequesTime();
            data["merchant_id"] = this.aba_merchant_id;
            data["tran_id"] = "TRX-" + invoice.ID;
            string toHash = data["req_time"].ToString()
                          + data["merchant_id"].ToString()
                          + data["tran_id"].ToString();                          
            data["hash"] = HashToString(toHash, Encoding.ASCII.GetBytes(this.aba_public_key));
            string url = this.aba_baseurl + "check-transaction";
            FormEngine.MultipartFormDataPost((response) =>
            {
                ABAPaymentStatus dataresponse = JsonConvert.DeserializeObject<ABAPaymentStatus>(response,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                del(dataresponse);
            }, url, data);          
        }




        public void PaymentListToValidate(responseDelegate<List<Invoice>> del)
        {
            RESTEngine.HttpGet(data =>
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject<List<Invoice>>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    del(obj);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    }
                    );
                }

            }, this.BaseURL + "/payment", headers);
        }
        // Invoice isPaid 1 = normal paid ,2 = paid but not validated, 0 = unpaid
        public void InvoiceValidatePayment(responseDelegate<bool> del, string invoiceid)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["status"] = "1";

            RESTEngine.HttpPut((response) =>
            {
                del(true);
            },this.BaseURL + "/payment",data);

        }

        public void InvoiceSetABAPaidPayment(responseDelegate<bool> del, string invoiceid)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["status"] = "2";
            RESTEngine.HttpPut((response) =>
            {
                del(true);
            }, this.BaseURL + "/payment", data);
        }

        // Turn back to zero
        public void InvoiceDenyPayment(responseDelegate<bool> del, string invoiceid)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["status"] = "0";
            RESTEngine.HttpPut((response) =>
            {
                del(true);
            }, this.BaseURL + "/payment", data);

        }
    }

}
