using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class FundRepository : IFundRepository
    {
        private readonly FinanceAppContext _context;
        public FundRepository(FinanceAppContext context)
        {
            _context = context;
        }

        public async Task<Fund> GetFundByIdAsync(int id)
        {
            return await _context.Funds.FindAsync(id);
        }

        public async Task<List<Fund>> GetFundsAsync()
        {
            return await _context.Funds.ToListAsync();
        }
    }
}
