using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    class DuplicateTodoLabelException : Exception
    {
        public DuplicateTodoLabelException()
        {
        }

        public DuplicateTodoLabelException(string message) : base(message)
        {
        }

        public DuplicateTodoLabelException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
