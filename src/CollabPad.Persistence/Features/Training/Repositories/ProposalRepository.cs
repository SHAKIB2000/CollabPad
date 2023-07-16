using CollabPad.Application.Features.Training.Repositories;
using CollabPad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CollabPad.Persistence.Features.Training.Repositories
{
    public class ProposalRepository : Repository<Proposal, Guid>, IProposalRepository
    {
        public ProposalRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
        public async Task<(IList<Proposal> records, int total, int totalDisplay)> 
            GetTableDataAsync(Expression<Func<Proposal, bool>> expression, string orderBy, int pageIndex, int pageSize)
        {
            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }

        public bool IsDuplicatePerson(string title, Guid? id)
        {
            int? ExistingProposalCount = null;

            if (id.HasValue)
                ExistingProposalCount = GetCount(x => x.Title == title && x.Id != id.Value);
            else
                ExistingProposalCount = GetCount(x => x.Title == title);

            return ExistingProposalCount > 0;
        }
    }
}
