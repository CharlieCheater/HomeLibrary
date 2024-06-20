using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.DAL.Domain.Interfaces
{

    /// <summary>
    /// Represents the creation date and last update info
    /// </summary>
    public interface IHaveCreationDate
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
