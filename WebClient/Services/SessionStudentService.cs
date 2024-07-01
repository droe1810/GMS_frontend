using WebClient.DTO.SessionStudent;
using WebClient.DTO.StudentGrade;

namespace WebClient.Services
{
    public class SessionStudentService
    {
        public static decimal GetAvgGrade(int courseId, int studentId)
        {
            decimal avg = 0;
            HttpClient client = new HttpClient();
            string url = $"http://localhost:5100/api/SessionStudent/GetAvgGrade/{courseId}/{studentId}\r\n";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                avg = response.Content.ReadFromJsonAsync<decimal>().Result;
            }

            return avg;
        }


        public static GetStatusDTO GetStatus(int courseId, int studentId)
        {
            GetStatusDTO result = new GetStatusDTO();
            HttpClient client = new HttpClient();
            string url = $"http://localhost:5100/api/SessionStudent/GetStatus/{courseId}/{studentId}\r\n";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = response.Content.ReadFromJsonAsync<GetStatusDTO>().Result;
            }

            return result;
        }
    }
}
