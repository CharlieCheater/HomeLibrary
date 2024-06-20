
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeLibrary.MVC.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TableContents { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
    }
}