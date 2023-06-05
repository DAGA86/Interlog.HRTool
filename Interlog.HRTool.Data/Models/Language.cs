using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interlog.HRTool.Data.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Culture { get; set; }

        public virtual ICollection<Translation> Translations { get; set; } = new HashSet<Translation>();
    }
}
