using System;
using System.Linq;

namespace CollabPad.Domain.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
