using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.Services.Interfaces
{
    public interface IHandleGeneratorService
    {
        Task<string> GenerateUniqueAnonymousHandleAsync();
    }
}
