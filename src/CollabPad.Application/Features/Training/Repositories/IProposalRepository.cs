using CollabPad.Domain.Entities;
using CollabPad.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CollabPad.Application.Features.Training.Repositories
{
    public interface IProposalRepository : IRepositoryBase<Proposal, Guid>
    {
        Task<(IList<Proposal> records, int total, int totalDisplay)>
            GetTableDataAsync(Expression<Func<Proposal, bool>> expression, string orderBy, int pageIndex, int pageSize);
        bool IsDuplicatePerson(string title, Guid? id);
    }
}
