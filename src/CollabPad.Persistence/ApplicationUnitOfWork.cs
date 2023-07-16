using CollabPad.Application;
using CollabPad.Application.Features.Training.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CollabPad.Persistence
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IProposalRepository Proposals { get; private set; }
        public ApplicationUnitOfWork(IApplicationDbContext dbContext, IProposalRepository proposals) : base((DbContext)dbContext)
        {
            Proposals = proposals;
        }
    }
}
