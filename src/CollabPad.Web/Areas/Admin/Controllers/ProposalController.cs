using Autofac;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using CollabPad.Infrastructure;
using CollabPad.Web.Areas.Admin.Models;
using CollabPad.Web.Models;
using CollabPad.Web.Utilities;

namespace CollabPad.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProposalController : Controller
    {
        ILifetimeScope _scope;
        ILogger<ProposalController> _logger;
        public ProposalController(ILifetimeScope scope, ILogger<ProposalController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Proposals()
        {
            var model = _scope.Resolve<ProposalListModel>();
            return View(model);
        }
        public async Task<JsonResult> GetProposals()
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<ProposalListModel>();
            var data = await model.GetPagedProposalsAsync(dataTablesModel);
            return Json(data);
        }
        public IActionResult Create()
        {
            var model = _scope.Resolve<ProposalCreateModel>();

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(ProposalCreateModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateProposal();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Added a Proposal Record.",
                        ResponseType = ResponseTypes.Success
                    });
                    return RedirectToAction("Proposals");
                }
                catch (DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        ResponseType = ResponseTypes.Error
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server Error");
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Proposal record can't create.",
                        ResponseType = ResponseTypes.Error
                    });
                }
            }

            return View(model);
        }
        public IActionResult Update(Guid id)
        {
            var model = _scope.Resolve<ProposalUpdateModel>();
            model.Load(id);

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(ProposalUpdateModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    model.UpdateProposal();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Updated the Proposal Record.",
                        ResponseType = ResponseTypes.Success
                    });
                    return RedirectToAction("Proposals");
                }
                catch (DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        ResponseType = ResponseTypes.Success
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server Error");
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Proposal record can't update.",
                        ResponseType = ResponseTypes.Error
                    });
                }
            }

            return View(model);
        }
        public IActionResult Delete(Guid id)
        {
            var model = _scope.Resolve<ProposalListModel>();

            try
            {
                model.DeleteProposal(id);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully Deleted the Proposal record.",
                    ResponseType = ResponseTypes.Success
                });
                return RedirectToAction("Proposals");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Server Error");
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Proposal record can't delete.",
                    ResponseType = ResponseTypes.Error
                });
            }
            return View(model);
        }
    }
}
