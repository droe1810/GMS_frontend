using System.Text;
using System.Text.Json;
using WebClient.DTO.Request;
using WebClient.DTO.Session;

namespace WebClient.Services
{
    public class RequestService
    {
        public static ResultForCreateRequestDTO CreateRequest(int studentId, int gradeId, string requestContent)
        {
            ResultForCreateRequestDTO result = new ResultForCreateRequestDTO();
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:5100/api/Request/CreateRequest/{studentId}/{gradeId}/{requestContent}\r\n";


                HttpResponseMessage response = client.PostAsync(url, null).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = response.Content.ReadFromJsonAsync<ResultForCreateRequestDTO>().Result;
                }
             
            }

            return result;
        }

        public static ResultForUpdateRequestDTO UpdateRequest(int requestId, int newValue)
        {
            HttpClient client = new HttpClient();
            ResultForUpdateRequestDTO result = null;


            string url = $"http://localhost:5100/api/Request/UpdateRequest/{requestId}/{newValue}\r\n";

            var request = new HttpRequestMessage(HttpMethod.Patch, url);

            HttpResponseMessage response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadFromJsonAsync<ResultForUpdateRequestDTO>().Result;
            }


            return result;
        }


        public static List<GetRequestDTO> GetRequestByStudent(int studentId)
        {
            List<GetRequestDTO> list = new List<GetRequestDTO>();
            HttpClient client = new HttpClient();
            string url = $"http://localhost:5100/api/Request/GetRequest/{studentId}\r\n";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                list = response.Content.ReadFromJsonAsync<List<GetRequestDTO>>().Result;
            }

            return list;
        }

        public static List<GetRequestDTO> GetAll()
        {
            List<GetRequestDTO> list = new List<GetRequestDTO>();
            HttpClient client = new HttpClient();
            string url = $"http://localhost:5100/api/Request/GetAllRequest\r\n";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                list = response.Content.ReadFromJsonAsync<List<GetRequestDTO>>().Result;
            }

            return list;
        }

    }
}
