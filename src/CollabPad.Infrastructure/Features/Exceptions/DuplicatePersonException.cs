using System;
using System.Linq;

namespace CollabPad.Infrastructure.Features.Exceptions
{
    public class DuplicatePersonException : Exception
    {
        public DuplicatePersonException(string message) : base(message)
        {
        }
    }
}
