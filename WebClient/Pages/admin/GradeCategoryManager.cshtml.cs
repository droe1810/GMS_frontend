﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimetableSystem.Services;
using WebClient.DTO.ComparisonType;
using WebClient.DTO.GradeType;
using WebClient.DTO.Role;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.admin
{
    public class GradeCategoryManagerModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GradeCategoryManagerModel()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }

        public List<GetGradeTypeDTO> ListGradeTypes { get; set; }

        public List<RoleDTO> ListRoleGraded { get; set; }

        public List<ComparisonTypeDTO> ListComparisonTypes { get; set; }

        [BindProperty]
        public CreateGradeTypeDTO GradeType { get; set; }

        public string Msg { get; set; }

        public IActionResult OnGet()
        {
            GetUserDTO user = AuthenticationHelper.GetAuthenticatedUser(_httpContextAccessor.HttpContext);

            if (user == null || !AuthenticationHelper.IsAdmin(user))
            {
                return Redirect("/AccessDenied");
            }

            try
            {
                GetData();
                return Page();
            }
            catch
            {
                return Redirect("/SeverError");
            }
        }

        public IActionResult OnPostAdd()
        {
            GetUserDTO user = AuthenticationHelper.GetAuthenticatedUser(_httpContextAccessor.HttpContext);

            if (user == null || !AuthenticationHelper.IsAdmin(user))
            {
                return Redirect("/AccessDenied");
            }

            try
            {
                bool semesterOnGoing = SemesterService.IsSemeterOnGoing();
                if (semesterOnGoing)
                {
                    Msg = "Create Fail. Semester is on going";
                    GetData();
                    return Page();
                }


                if (!ModelState.IsValid)
                {
                    return Page();
                }


                var success = GradeTypeService.CreateGradeType(GradeType);
                GetData();
                Msg = success ? "Create success" : "Create fail, duplicate name";
                return Page();
            }
            catch
            {
                return Redirect("/SeverError");
            }
        }

        public IActionResult OnPostUpdate()
        {
            GetUserDTO user = AuthenticationHelper.GetAuthenticatedUser(_httpContextAccessor.HttpContext);

            if (user == null || !AuthenticationHelper.IsAdmin(user))
            {
                return Redirect("/AccessDenied");
            }


            try
            {
                bool semesterOnGoing = SemesterService.IsSemeterOnGoing();
                if (semesterOnGoing)
                {
                    Msg = "Update Fail. Semester is on going";

                    GetData();


                    return Page();
                }


                var form = Request.Form;

                ListGradeTypes = GradeTypeService.GetALlGradeType();

                foreach (var gradeType in ListGradeTypes)
                {
                    var gradeTypeId = gradeType.Id;
                    var gradedByRole = int.Parse(form[$"gradedByRoleId_{gradeTypeId}"]);
                    var comparisonTypeId = form[$"comparasionType_{gradeTypeId}"];
                    var comparisonValue = int.Parse(form[$"comparisonValue_{gradeTypeId}"]);

                    var success = GradeTypeService.UpdateGradeType(gradeTypeId, gradedByRole, comparisonTypeId, comparisonValue);

                    if (!success)
                    {
                        Msg = "Update failed for GradeType ID: " + gradeTypeId;
                        GetData();
                        return Page();
                    }
                }

                GetData();
                Msg = "Update success";
                return Page();
            }
            catch
            {
                return RedirectToPage("/SeverError");
            }
        }
        public IActionResult OnPostDelete(int gradeTypeId)
        {
            GetUserDTO user = AuthenticationHelper.GetAuthenticatedUser(_httpContextAccessor.HttpContext);

            if (user == null || !AuthenticationHelper.IsAdmin(user))
            {
                return Redirect("/AccessDenied");
            }

            bool semesterOnGoing = SemesterService.IsSemeterOnGoing();
            if (semesterOnGoing)
            {
                Msg = "Delete Fail. Semester is on going";
                try
                {
                    GetData();
                }
                catch (Exception)
                {
                    return RedirectToPage("/SeverError");
                }

                return Page();
            }

            try
            {
                var success = GradeTypeService.DeleteGradeType(gradeTypeId);
                GetData();
                if (success)
                {
                    Msg = "Delete success";
                    return Page();
                }
                else
                {
                    Msg = "Delete fail";
                    return Page();
                }
            }
            catch
            {
                return Redirect("/SeverError");
            }
        }

        public void GetData()
        {
            ListGradeTypes = GradeTypeService.GetALlGradeType();
            ListRoleGraded = RoleService.GetRoleGraded();
            ListComparisonTypes = ComparisionTypeService.GetAll();
        }

    }
}
