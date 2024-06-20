using HomeLibrary.DAL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.DAL.Domain.Abstracts
{
    public abstract class Identity<TKey> : IHaveId<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
    public abstract class Identity : IHaveId
    {
        public int Id { get; set; }
    }
}
