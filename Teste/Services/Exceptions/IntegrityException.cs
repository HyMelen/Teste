using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Services.Exceptions
{
    public class IntegrityException : ApplicationException
  {
        // : base(message) [repassa a mensagem para a superclasse]
        public IntegrityException(string message) : base(message) 
        {
        }
    }
}
