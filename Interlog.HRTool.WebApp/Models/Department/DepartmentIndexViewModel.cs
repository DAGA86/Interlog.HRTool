using Interlog.HRTool.WebApp.Models.Company;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.WebApp.Models.Department
{
    public class DepartmentIndexViewModel
    {
        public List<DepartmentViewModel> Departments { get; set; }
        public DepartmentViewModel Department { get; set; }
        public SelectList Companies { get; set; }
    }
}
