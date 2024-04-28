using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Models;
using backend_core.Repository;

namespace backend_core.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task Save();
    }
}