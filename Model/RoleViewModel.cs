using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HangilFA.Model
{
    public class RoleViewModel
    {

        [Display(Name = "Identity Key")]
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }

        public static implicit operator RoleViewModel(List<NavigationMenuViewModel> v)
        {
            throw new NotImplementedException();
        }
    }
}
