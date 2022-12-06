﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BooksMvc.Models
{
    public class AddBook
    {
        [Display(Name = "Назание книги")]
        public string Name { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "Количество страниц")]
        public int Pages { get; set; }

    }
}
