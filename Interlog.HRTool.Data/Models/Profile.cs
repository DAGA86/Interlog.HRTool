using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.Data.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }


        public ICollection<Employee> Employees { get; } = new List<Employee>();
        public ICollection<EmployeeProfile> EmployeeProfiles { get; } = new List<EmployeeProfile>();
    }
}
