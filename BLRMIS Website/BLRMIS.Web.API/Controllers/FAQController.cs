using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BLRMIS.Web.API.Extenstions;
using BLRMIS.Web.Common;
using BLRMIS.Web.Domain;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BLRMIS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FAQController : ControllerBase
    {
        IFAQService _fAQService;
        public FAQController(IFAQService fAQService)
        {
            this._fAQService = fAQService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllAsync(PagingModel pagingModel, string Title)
        //{
        //    dynamic actionResult;
        //    if (ModelState.IsValid)
        //    {
        //        //string Title = string.Empty;
        //        if (string.IsNullOrWhiteSpace(pagingModel.OrderBy))
        //        {
        //            pagingModel.OrderBy = nameof(FAQModel.FaqId);
        //        }

        //        if (pagingModel.Page == null && pagingModel.PageSize > 0)
        //        {
        //            actionResult = new ErrorResponseModel()
        //                .FieldValidationError(new Dictionary<string, string>
        //                {
        //                        {FieldNames.Page, ErrorMessages.PageRequired}
        //                })
        //                .ToHttpResponse();
        //        }
        //        else
        //        {
        //            var response = await _fAQService.GetAllFAQs(pagingModel, Title);

        //            actionResult = response.IsError
        //                            ? response.Error.ToHttpResponse()
        //                            : Ok(response.Records);
        //        }

        //    }
        //    else
        //    {
        //        actionResult = new ErrorResponseModel()
        //       .FieldValidationError(ModelState.AllErrors())
        //       .ToHttpResponse();
        //    }
        //    return actionResult;
        //}

        [HttpGet]
        public ActionResult<ListResponseModel<DownloadModel>> GetAll([FromQuery(Name = "SearchParams")]string SearchParams)
        {
            return Ok(_fAQService.GetAll(SearchParams));
        }

        [HttpGet]
        [Route("/api/Faq/GetAll")]
        [AllowAnonymous]
        public ActionResult<ListResponseModel<DownloadModel>> GetAllFaq(string Title, string Description)
        {
            return Ok(_fAQService.GetAllFAQs(Title,Description));
        }


        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var result = _fAQService.GetFAQ(id);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FAQModel model)
        {
            if (ModelState.IsValid)
            {
                model = await _fAQService.CreateFAQ(model);
                //model = _contentService.GetContent(model.ContentPageId?.ToString());
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] FAQModel model)
        {
            await _fAQService.UpdateFAQ(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _fAQService.DeleteFAQ(id);

            return Ok();
        }
    }
}