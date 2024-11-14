using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;

namespace UserSideModel
{
    public class ApiDataManager
    {
        //static string standartUrl = "https://localhost:44346/api/students/";
        public static async Task<StudentModel> GetStudent(int id, string baseUri)
        {
            var url = baseUri + id.ToString();
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    //StudentModel student = await response.Content.ReadAsAsync<StudentModel>();
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    var student = JsonConvert.DeserializeObject<StudentModel>(responseAsString);
                    return student;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<Uri> PostStudent(StudentModel student, string baseUri)
        {
            string url = baseUri;
            var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8,  "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var UrireturnUrl = response.Headers.Location;
                    return UrireturnUrl;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<List<StudentModel>> GetStudents(string url)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    //StudentModel student = await response.Content.ReadAsAsync<StudentModel>();
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    var student = JsonConvert.DeserializeObject<List<StudentModel>>(responseAsString);
                    return student;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<bool> DeleteQuery(int StudentId, string url)
        {
            if(StudentId < 0)
                throw new Exception("Invalid id");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync(url+StudentId))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
