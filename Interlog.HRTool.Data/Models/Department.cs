using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interlog.HRTool.Data.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

        public int CompanyId { get; set; }
               

        public Company Company { get; set; }
        public ICollection<Employee> Employees { get; set;}
    }
}
