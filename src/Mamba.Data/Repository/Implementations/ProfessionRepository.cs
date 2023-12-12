using Mamba.Core.Models;
using Mamba.Core.Repository.Interfaces;
using Mamba.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Data.Repository.Implementations
{
    public class ProfessionRepository : GenericRepository<Profession>, IProfessionRepository
    {
        public ProfessionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
