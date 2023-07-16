using CollabPad.Domain.Entities;
using System;
using System.Linq;

namespace CollabPad.Application.Features.Training.Services
{
    public interface IProposalService
    {
        void CreateProposal(string title, string description, string area, DateTime creation, Status status);
        Proposal GetProposal(Guid id);
        void UpdateProposal(Guid id, string title, string description, string area, DateTime creation, Status status);
        void DeleteProposal(Guid id);
        public IList<Proposal> GetProposals();
        Task<(IList<Proposal> records, int total, int totalDisplay)> 
            GetPagedProposalsAsync(int pageIndex, int pageSize, string searchText, string orderBy);
    }
}
