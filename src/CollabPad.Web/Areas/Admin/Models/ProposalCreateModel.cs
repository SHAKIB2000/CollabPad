using Autofac;
using System.ComponentModel.DataAnnotations;
using CollabPad.Application.Features.Training.Services;
using CollabPad.Domain.Entities;

namespace CollabPad.Web.Areas.Admin.Models
{
    public class ProposalCreateModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Area { get; set; }
        [Required]
        public DateTime Creation = DateTime.Now;
        [Required]
        public Status Status { get; set; }
        private IProposalService _ProposalService;
        public ProposalCreateModel()
        {
        }
        public ProposalCreateModel(IProposalService ProposalService)
        {
            _ProposalService = ProposalService;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _ProposalService = scope.Resolve<IProposalService>();
        }
        internal void CreateProposal()
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                _ProposalService.CreateProposal(Title, Description, Area, Creation, Status);
            }
        }
    }
}
