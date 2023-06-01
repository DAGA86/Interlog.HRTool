using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.Data.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }


        public ICollection<Department> Departments { get; set; } = new List<Department>();
    }
}
