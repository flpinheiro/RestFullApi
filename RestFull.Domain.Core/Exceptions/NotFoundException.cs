using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFull.Domain.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Not Found") { }

    public NotFoundException(string name) : base($"Not Found {name}") { }
}
