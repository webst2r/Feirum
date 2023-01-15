using Feirum.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Feirum.Models
{
    public class CreateFairViewModel
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
    }
}
