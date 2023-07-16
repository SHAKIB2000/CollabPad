using Autofac;
using System.ComponentModel.DataAnnotations;
using CollabPad.Application.Features.Training.Services;
using CollabPad.Domain.Entities;

namespace CollabPad.Web.Areas.Admin.Models
{
    public class ProposalUpdateModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Area { get; set; }
        [Required]
        public DateTime Creation { get; set; }
        [Required]
        public Status Status { get; set; }
        private IProposalService _ProposalService;
        public ProposalUpdateModel()
        {
        }
        public ProposalUpdateModel(IProposalService ProposalService)
        {
            _ProposalService = ProposalService;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _ProposalService = scope.Resolve<IProposalService>();
        }
        internal void UpdateProposal()
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                _ProposalService.UpdateProposal(Id, Title, Description, Area, Creation, Status);
            }
        }
        internal void Load(Guid id)
        {
            Proposal Proposal = _ProposalService.GetProposal(id);

            Id = Proposal.Id;
            Title = Proposal.Title;
            Description = Proposal.Description;
            Area = Proposal.Area;
            Creation = Proposal.Creation;
            Status = Proposal.Status;
        }
    }
}
