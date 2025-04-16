using ER_Recovery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Infrastructure.Data.Repositories
{
    public interface IMeetingRepository
    {
        Task<List<Meeting>> GetAllMeetingsAsync();
    }
}
