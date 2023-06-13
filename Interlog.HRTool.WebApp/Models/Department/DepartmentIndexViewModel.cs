using Interlog.HRTool.WebApp.Models.Company;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Interlog.HRTool.WebApp.Models.Department
{
    public class DepartmentIndexViewModel
    {
        public List<DepartmentViewModel> Departments { get; set; }
        public DepartmentViewModel Department { get; set; }
        public CompanyViewModel Company { get; set; }
        public SelectList Companies { get; set; }
    }
}
