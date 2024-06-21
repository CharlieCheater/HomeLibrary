using HomeLibrary.MVC.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeLibrary.MVC.Models
{
    public class AddBookViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public string Title { get; set; }
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public string Description { get; set; }
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        [AllowHtml]
        public string TableContents { get; set; }
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public string Author { get; set; }
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public string Publisher { get; set; }
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public int PublicationYear { get; set; }
    }
}