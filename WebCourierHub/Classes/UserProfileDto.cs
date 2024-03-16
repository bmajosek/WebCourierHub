using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCourierHub.Models;

namespace WebCourierHub.Classes
{
    public class UserProfileDto
    {
        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public string ProfileImage { get; set; }

        public string Role { get; set; }

        public Company? Company { get; set; }

        public List<SelectListItem> Companies { get; set; }
    }
}