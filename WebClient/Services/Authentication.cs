using Microsoft.AspNetCore.Mvc;
using System;
using WebClient.DTO;
using WebClient.DTO.User;

namespace TimetableSystem.Services
{
    public class Authentication
    {
        public static bool IsAdmin(GetUserDTO u)
        {
            int adminRole = 1;
            return u != null && u.RoleId == adminRole;
        }

        public static bool IsTeacher(GetUserDTO u)
        {
            int teacherRole = 2;
            return u != null && u.RoleId == teacherRole;
        }

        public static RedirectToPageResult AccessDenied()
        {
            return new RedirectToPageResult("/AccessDenied");
        }
    }
}
