using WebClient.DTO.Session;
using WebClient.DTO.User;

namespace WebClient.Services
{
    public class SessionService
    {
        public static List<GetSessionDTO> GetSessionByTeacher(int? teacherId)
        {
            List<GetSessionDTO> ListSs = new List<GetSessionDTO>();
            HttpClient client = new HttpClient();
            string url = $"http://localhost:5100/api/Session/GetSessionByTeacher/{teacherId}\r\n";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ListSs = response.Content.ReadFromJsonAsync<List<GetSessionDTO>>().Result;
            }

            return ListSs;
        }

        public static List<GetSessionDTO> GetSessionByStudent(int? studentId)
        {
            List<GetSessionDTO> ListSs = new List<GetSessionDTO>();
            HttpClient client = new HttpClient();
            string url = $"http://localhost:5100/api/Session/GetSessionByStudent/{studentId}\r\n";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ListSs = response.Content.ReadFromJsonAsync<List<GetSessionDTO>>().Result;
            }

            return ListSs;
        }


    }
}
