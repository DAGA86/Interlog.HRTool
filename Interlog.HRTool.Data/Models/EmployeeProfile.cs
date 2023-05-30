using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.Data.Models
{
    public class EmployeeProfile
    {
        public int EmployeeId { get; set; }
        public int ProfileId { get; set; }


        public Employee Employee { get; set; } = null!;
        public Profile Profile { get; set; } = null!;

    }
}
