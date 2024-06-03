﻿using bookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace bookStore.ViewModel
{
    public class BookFormVM
    {
        public string Name { get; set; } = null!;
        [Display(Name = "Author")]
        public int AutherId { get; set; }
        public List<SelectListItem> Authers { get; set; } = new List<SelectListItem>();
        public string Publisher { get; set; } = null!;
        [Display(Name = "Publisher Date")]
        public DateTime PublishDate { get; set; } = DateTime.Now;
        [Display(Name="Book Image")]
        public IFormFile? ImageUrl { get; set; }
        public string Description { get; set; } = null!;
        [Display(Name="Book Categories")]
        public List<int> SelectedCategories { get; set; } = new List<int>();
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

    }
}
