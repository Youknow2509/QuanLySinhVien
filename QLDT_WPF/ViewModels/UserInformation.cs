using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT_WPF.ViewModels
{
    public class UserInformation
    {
        public string? IdUser { get; set; }
        public string? IdClaim { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        public string? FullName { get; set; }
        public byte[]? Image { get; set; }
    }
}
