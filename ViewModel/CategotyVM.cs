using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace bookStore.ViewModel
{
    public class CategotyVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "please enter the categoty name")]
        [MaxLength(30 , ErrorMessage ="the name must not be more that 30 character")]
        [Remote(action: "CheckName", controller: "Categories", ErrorMessage = "hellow every body")] public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
