using Interlog.HRTool.Data.Models;
using Interlog.HRTool.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.WebApp.Models.Employee
{
    public class EmployeeIndexViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
    }
}
