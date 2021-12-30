using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HangilFA.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the HangilFAUser class
    public class HangilFAUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(100)")]
        public string FullName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
