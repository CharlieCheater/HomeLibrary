using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.DAL.Domain.Interfaces
{
    public interface IHaveId : IHaveId<int> { }
    public interface IHaveId<TKey> where TKey : struct
    {
        TKey Id { get; set; }
    }
}
