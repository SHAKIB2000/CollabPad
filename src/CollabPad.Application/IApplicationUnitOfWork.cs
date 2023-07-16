using CollabPad.Application.Features.Training.Repositories;
using CollabPad.Domain.UnitOfWorks;
using System;
using System.Linq;

namespace CollabPad.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IProposalRepository Proposals { get; }
    }
}
