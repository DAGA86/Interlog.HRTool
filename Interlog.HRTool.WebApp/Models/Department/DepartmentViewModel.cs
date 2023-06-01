using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.WebApp.Models.Department
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

        public int CompanyId { get; set; }

    }
}
