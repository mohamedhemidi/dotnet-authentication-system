using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Data;
using backend_core.Interfaces;
using backend_core.Models;

namespace backend_core.Repository
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {

        private ApplicationDBContext _db;
        public SkillRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}