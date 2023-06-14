using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.WebApp.Models.Profile
{
    public class ProfileDeleteViewModel
    {
        public int Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }
    }
}
