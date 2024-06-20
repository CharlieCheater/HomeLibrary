using HomeLibrary.Infrastructure.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.Infrastructure.Models
{
    public class Book : Auditable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TableContents { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
    }
}
