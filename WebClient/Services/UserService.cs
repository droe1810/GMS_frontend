using WebClient.DTO.Session;
using WebClient.DTO.User;

namespace WebClient.Services
{
    public class UserService
    {
        public static GetUserDTO login(string username, string password)
        {
            GetUserDTO u = new GetUserDTO();
            HttpClient client = new HttpClient();
            string url = $"http://localhost:5100/api/User/GetUser/{username}/{password}\r\n";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                u = response.Content.ReadFromJsonAsync<GetUserDTO>().Result;
            }

            return u;
        }

        public static GetUserDTO GetStudent(string username)
        {
            GetUserDTO u = new GetUserDTO();
            HttpClient client = new HttpClient();
            string url = $"http://localhost:5100/api/User/GetStudent/{username}\r\n";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                u = response.Content.ReadFromJsonAsync<GetUserDTO>().Result;
            }

            return u;
        }

        public static List<GetUserDTO> GetStudentInSession(int sessionId)
        {
            List<GetUserDTO> listU = new List<GetUserDTO>();
            HttpClient client = new HttpClient();
            string url = $"http://localhost:5100/api/User/GetStudentInSession/{sessionId}\r\n";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listU = response.Content.ReadFromJsonAsync<List<GetUserDTO>>().Result;
            }

            return listU;
        }

        public static List<GetUserDTO> GetStudentInCourse(int courseId)
        {
            List<GetUserDTO> listU = new List<GetUserDTO>();
            HttpClient client = new HttpClient();
            string url = $"http://localhost:5100/api/User/GetStudentByCourseId/{courseId}\r\n";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listU = response.Content.ReadFromJsonAsync<List<GetUserDTO>>().Result;
            }

            return listU;
        }

        public static AccountBalanceDTO DeductFunds(int studentId)
        {
            AccountBalanceDTO result = new AccountBalanceDTO();
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:5100/api/User/DeductFunds/{studentId}";

                // Tạo yêu cầu PATCH
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, url);

                HttpResponseMessage response = client.SendAsync(request).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = response.Content.ReadFromJsonAsync<AccountBalanceDTO>().Result;
                }
               
            }

            return result;
        }

        public static AccountBalanceDTO ReFund(int studentId)
        {
            AccountBalanceDTO result = new AccountBalanceDTO();
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:5100/api/User/Refund/{studentId}";

                // Tạo yêu cầu PATCH
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, url);

                HttpResponseMessage response = client.SendAsync(request).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = response.Content.ReadFromJsonAsync<AccountBalanceDTO>().Result;
                }

            }

            return result;
        }

        public static AccountBalanceDTO Recharge(int studentId, int value)
        {
            AccountBalanceDTO result = new AccountBalanceDTO();
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:5100/api/User/Recharge/{studentId}/{value}";

                // Tạo yêu cầu PATCH
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, url);

                HttpResponseMessage response = client.SendAsync(request).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = response.Content.ReadFromJsonAsync<AccountBalanceDTO>().Result;
                }

            }

            return result;
        }


    }
}
