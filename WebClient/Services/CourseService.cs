using WebClient.DTO.Course;
using WebClient.DTO.Session;

namespace WebClient.Services
{
    public class CourseService
    {
        public static List<CourseDTO> GetCourses()
        {
            List<CourseDTO> List = new List<CourseDTO>();
            HttpClient client = new HttpClient();
            string url = $"http://localhost:5100/api/Course/GetAllCourse\r\n";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List = response.Content.ReadFromJsonAsync<List<CourseDTO>>().Result;
            }

            return List;
        }
    }
}
