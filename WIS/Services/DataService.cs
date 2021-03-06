using System;
using System.Collections.Generic;
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
        private Dictionary<string, string> headers;

        public APPUSER CurrentUser { get; set; }

        private static DataService instance;
        public static DataService Instance
        {
            get{               
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
            data["phone"] = signup.phone.Replace(" ",string.Empty);
            data["identifier"] = signup.identifier;
            data["birthdate"] = signup.birthdate;

            RESTEngine.HttpPost(result =>{
                if (result == null)
                    del(false);
                else
                    del(true);
            },
            this.BaseURL + "/preregister",data,this.headers,true);
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
                catch(Exception ex){
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }
                
            },
            this.BaseURL + "/resetrequest", data, null);
        }

        public void EditPassword(string username, string newpassword,responseDelegate<bool> del)
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
                catch(Exception ex){
                    Device.BeginInvokeOnMainThread(() => {
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
                    catch(Exception ex){
                        Device.BeginInvokeOnMainThread(() => {
                            Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                        });
                    }
                    
                }
                else
                    del(null);
                
                
            }, this.BaseURL + "/login", data, null,true);
        }

        // Attendance
        public void GetAttendance(responseDelegate<List<ABSENCE>> del)
        {
            RESTEngine.HttpGet(data => {
                try
                {
                    var obj = JsonConvert.DeserializeObject<List<ABSENCE>>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    del(obj);
                }catch(Exception ex){
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }
               
                
            }, this.BaseURL + "/attendance",headers);
        }

        public void GetAttendanceHistory(responseDelegate<List<ABSENCE>> del)
        {
            RESTEngine.HttpGet(data => {
                try
                {
                    del(JsonConvert.DeserializeObject<List<ABSENCE>>(data,
                         new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }catch (Exception ex){
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }


            }, this.BaseURL + "/attendancehistory", headers);
        }

        public void GetAttendanceDetails(string ID,responseDelegate<ABSENCE> del)
        {
            RESTEngine.HttpGet(data =>
            {
                try
                {
                    del(JsonConvert.DeserializeObject<ABSENCE>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }catch (Exception ex){
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }
            }, this.BaseURL + "/attendancedetails/" + ID,headers);
        }

        
        
        // Invoice
        public void GetInvoiceList(responseDelegate<List<Invoice>> del)
        {
            RESTEngine.HttpGet(data => {
                try
                {
                    var obj = JsonConvert.DeserializeObject<List<Invoice>>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    del(obj);
                }catch(Exception ex){
                    Device.BeginInvokeOnMainThread(() =>{
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");}
                    );
                }
               
            }, this.BaseURL + "/invoice", headers);
        }

        // Invoice History
        public void GetInvoiceHistory(responseDelegate<List<Invoice>> del)
        {
            RESTEngine.HttpGet(data => {
                try
                {
                    var obj =  JsonConvert.DeserializeObject<List<Invoice>>(data,
                         new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    del(obj);
                }catch(Exception ex){
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }
                
            }, this.BaseURL + "/invoicehistory", headers);
        }

        public void GetInvoiceDetails(string invoiceID,responseDelegate<Invoice> del)
        {
            RESTEngine.HttpGet(data =>
            {
                try
                {                 
                    var obj = JsonConvert.DeserializeObject<Invoice>(data,
                         new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    del(obj);
                }catch(Exception ex){
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }
                
            }, this.BaseURL + "/invoicedetails/" + invoiceID, headers);
        }


        public void GetTest(responseDelegate<StudentSchedule> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            RESTEngine.HttpGet(data => {
                try
                {
                    del(JsonConvert.DeserializeObject<StudentSchedule>(data,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }

            }, this.BaseURL + "/test", headers);
        }

        // Student Schedule
        public void GetStudentSchedule(responseDelegate<StudentSchedule> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            RESTEngine.HttpGet(data => {
                try
                {
                    del(JsonConvert.DeserializeObject<StudentSchedule>(data,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }catch(Exception ex){
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }
                
            }, this.BaseURL + "/studentschedule", headers);
        }

        // Teacher Schedule
        public void GetTeacherSchedule(responseDelegate<TeacherSchedule> del)
        {
            
            RESTEngine.HttpGet(data => {
                try{
                    del(JsonConvert.DeserializeObject<TeacherSchedule>(data,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (Exception ex){
                    Device.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
                    });
                }

            }, this.BaseURL + "/teacherschedule", headers);
        }

        // Parent Schedule (all children schedule
        public void GetParentSchedule(responseDelegate<Dictionary<string,StudentSchedule>> del)
        {
            RESTEngine.HttpGet(data => {
                try
                {
                    del(JsonConvert.DeserializeObject<Dictionary<string, StudentSchedule>>(data,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() => {
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

        public void SendPushNotification(string title,string message, responseDelegate<bool> del)
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
    }

}
