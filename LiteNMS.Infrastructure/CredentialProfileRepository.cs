
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Entities.Models;
using LiteNMS.Infrastructure;
using LiteNMS.Infrastructure.DBContexts;

namespace LiteNMS.Repositories
{
    public class CredentialProfileRepository : BaseRepository<CredentialProfile>, ICredentialProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public CredentialProfileRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<bool> ProfileNameExistsAsync(string profileName)
        {
            return await _context.CredentialProfiles
                .AsNoTracking()
                .AnyAsync(cp => cp.ProfileName == profileName);
        }
    }
}