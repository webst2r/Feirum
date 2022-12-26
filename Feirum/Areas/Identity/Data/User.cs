using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Feirum.Models;
using Microsoft.AspNetCore.Identity;

namespace Feirum.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
// Esta é a classe que usaremos para autenticar utilizadores na nossa App.
public class User : IdentityUser
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public decimal Balance { get; set; }
    public string Address { get; set; }
}

