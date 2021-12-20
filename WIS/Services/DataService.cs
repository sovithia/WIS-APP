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
            //this.BaseURL = "http://localhost:3000";
            this.BaseURL = "http://119.82.252.226:3000";
            this.headers = new Dictionary<string, string>();
        }

        public void PreSignup(PRESIGNUPREQUEST signup, responseDelegate<bool> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["type"] = signup.type;
            data["phone"] = signup.phone.Replace(" ",string.Empty);
            data["identifier"] = signup.identifier;
            data["birthdate"] = signup.birthdate;

            RESTEngine.HttpPost(result =>{
                if (result == null)
                    del(false);
                else
                    del(true);
            },
            this.BaseURL + "/presignup",data,this.headers,true);
        }

        public void Signup(SIGNUPREQUEST signup, responseDelegate<bool> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["type"] = signup.type;
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
            this.BaseURL + "/signup", data, null);
        }

        public void ForgotPassword(string phone, responseDelegate<bool> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            RESTEngine.HttpPost(result =>
            {
                Dictionary<string, string> res = JsonConvert.DeserializeObject<Dictionary<string, string>>(result,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                del(res["result"] == "OK");
            },
            this.BaseURL + "/forgetpassword", data, null);
        }

        public void EditPassword(string userid, string newpassword, string token, responseDelegate<bool> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["userid"] = userid;
            data["password"] = newpassword;
            data["token"] = token;

            RESTEngine.HttpPost(result =>
            {               
                
                del(true);
            },
            this.BaseURL + "/editPassword", data, null);
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
                    APPUSER user = JsonConvert.DeserializeObject<APPUSER>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    CurrentUser = user;
                    headers["token"] = user.access_token;
                    del(user);
                }
                else
                    del(null);
                
                
            }, this.BaseURL + "/login", data, null,true);
        }

        // Attendance
        public void GetAttendance(responseDelegate<List<ABSENCE>> del)
        {
            RESTEngine.HttpGet(data => {                
               del(JsonConvert.DeserializeObject<List<ABSENCE>>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                
            }, this.BaseURL + "/attendance",headers);
        }

        public void GetAttendanceHistory(responseDelegate<List<ABSENCE>> del)
        {
            RESTEngine.HttpGet(data => {
                del(JsonConvert.DeserializeObject<List<ABSENCE>>(data,
                         new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

            }, this.BaseURL + "/attendancehistory", headers);
        }

        public void GetAttendanceDetails(string ID,responseDelegate<ABSENCE> del)
        {
            RESTEngine.HttpGet(data =>
            {
                del(JsonConvert.DeserializeObject<ABSENCE>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
            }, this.BaseURL + "/attendancedetails/" + ID,headers);
        }

        public void ValidateAttendance(string absenceID, responseDelegate<bool> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();                      
            RESTEngine.HttpPut(result =>
            {                
                del(true);
            }, this.BaseURL + "/attendance/" + absenceID, data, headers);
        }
        
        // Invoice
        public void GetInvoiceList(responseDelegate<List<Invoice>> del)
        {
            RESTEngine.HttpGet(data => {                
               del(JsonConvert.DeserializeObject<List<Invoice>>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
            }, this.BaseURL + "/invoice", headers);
        }

        // Invoice History
        public void GetInvoiceHistory(responseDelegate<List<Invoice>> del)
        {
            RESTEngine.HttpGet(data => {
                del(JsonConvert.DeserializeObject<List<Invoice>>(data,
                         new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
            }, this.BaseURL + "/invoicehistory", headers);
        }

        public void GetInvoiceDetails(string invoiceID,responseDelegate<Invoice> del)
        {
            RESTEngine.HttpGet(data =>
            {                
               del(JsonConvert.DeserializeObject<Invoice>(data,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
            }, this.BaseURL + "/invoicedetails/" + invoiceID, headers);
        }

        public void SubmitInvoice(string invoiceID, string B64Signature, string B64proof, responseDelegate<bool> del)
        {

            Dictionary<string, string> data = new Dictionary<string, string>();
            data["signature"] = B64Signature;
            data["proof"] = B64proof;
            RESTEngine.HttpPut(result => {              
                del(true);               
            }, this.BaseURL + "/invoice/" + invoiceID,data, headers);
        }
        
        public void ValidateInvoice(string invoiceID, string B64Signature,  responseDelegate<bool> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["signature"] = B64Signature;
            
            RESTEngine.HttpPut(result => {                
                del(true);
            }, this.BaseURL + "/invoice/" + invoiceID, data, headers);
        }


                

        // Student Schedule
        public void GetStudentSchedule(responseDelegate<TeacherSchedule> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            RESTEngine.HttpGet(data => {
                try
                {
                    del(JsonConvert.DeserializeObject<TeacherSchedule>(data,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }catch(Exception ex)
                {
                    int i = 2;
                }
                
            }, this.BaseURL + "/studentschedule", headers);
        }

        // Teacher Schedule
        public void GetTeacherSchedule(responseDelegate<TeacherSchedule> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            RESTEngine.HttpGet(data => {
                try
                {
                    del(JsonConvert.DeserializeObject<TeacherSchedule>(data,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (Exception ex)
                {
                    int i = 2;
                }

            }, this.BaseURL + "/teacherschedule", headers);
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
            }, this.BaseURL + "/registertoken", data, null, true);
        }
    }

}
