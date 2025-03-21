using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Task.Core.Entities
{
    public class user:IdentityUser
    {
        [MaxLength(50)]
        public string Name {  get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
