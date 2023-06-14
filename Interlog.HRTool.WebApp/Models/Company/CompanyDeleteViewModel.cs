using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.WebApp.Models.Company
{
    public class CompanyDeleteViewModel
    {
        public int Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

    }
}
