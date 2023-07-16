using CollabPad.Application;
using CollabPad.Application.Features.Training.Services;
using CollabPad.Domain.Entities;
using CollabPad.Infrastructure.Features.Exceptions;
using System;
using System.Linq;

namespace CollabPad.Infrastructure.Features.Training.Services
{
    public class ProposalService : IProposalService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public ProposalService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateProposal(string title, string description, string area, DateTime creation, Status status)
        {
            if (_unitOfWork.Proposals.IsDuplicatePerson(title, null))
            {

                throw new DuplicatePersonException("This Proposal is already exists");
            }
            Proposal Proposal = new Proposal();
            Proposal.Title = title;
            Proposal.Description = description;
            Proposal.Area = area;
            Proposal.Creation = creation;
            Proposal.Status = status;

            _unitOfWork.Proposals.Add(Proposal);
            _unitOfWork.Save();

        }

        public void DeleteProposal(Guid id)
        {
            _unitOfWork.Proposals.Remove(id);
            _unitOfWork.Save();
        }

        public async Task<(IList<Proposal> records, int total, int totalDisplay)>
            GetPagedProposalsAsync(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            var result = await _unitOfWork.Proposals.GetTableDataAsync(x =>
            x.Title.Contains(searchText), orderBy, pageIndex, pageSize);

            return result;
        }

        public Proposal GetProposal(Guid id)
        {
            return _unitOfWork.Proposals.GetById(id);
        }

        public IList<Proposal> GetProposals()
        {
            return _unitOfWork.Proposals.GetAll();
        }

        public void UpdateProposal(Guid id, string title, string description, string area, DateTime creation, Status status)
        {
            if (_unitOfWork.Proposals.IsDuplicatePerson(title, id))
            {
                throw new DuplicatePersonException("This Proposal is already exists");
            }
            Proposal Proposal = _unitOfWork.Proposals.GetById(id);
            Proposal.Title = title;
            Proposal.Description = description;
            Proposal.Area = area;
            Proposal.Creation = creation;
            Proposal.Status = status;

            _unitOfWork.Save();
        }

    }
}
