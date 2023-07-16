using CollabPad.Application.Features.Training.Services;
using CollabPad.Domain.Entities;
using CollabPad.Infrastructure;

namespace CollabPad.Web.Areas.Admin.Models
{
    public class ProposalListModel
    {
        private readonly IProposalService _ProposalService;
        public ProposalListModel()
        {

        }
        public ProposalListModel(IProposalService ProposalService)
        {
            _ProposalService = ProposalService;
        }
        public IList<Proposal> GetAllProposals()
        {
            return _ProposalService.GetProposals();
        }
        internal void DeleteProposal(Guid id)
        {
            _ProposalService.DeleteProposal(id);
        }
        public async Task<object> GetPagedProposalsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _ProposalService.GetPagedProposalsAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.SearchText,
                dataTablesUtility.GetSortText(new string[]
                {
                    "Title", "Description", "Area", "Creation", "Status"
                }));
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                {
                    record.Title,
                    record.Description,
                    record.Area,
                    record.Creation.ToString(),
                    record.Status.ToString(),
                    record.Id.ToString()
                }).ToArray()
            };
        }
    }
}
