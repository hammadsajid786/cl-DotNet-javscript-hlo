using System;
using System.Collections.Generic;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLRMIS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "content1", "content2" };
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        //[Authorize(Roles = "newpolicy")]
        public IActionResult Get(string id)
        {
            var result = _contentService.GetContent(id);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("dashboard")]
        [AllowAnonymous]
        public IActionResult GetContentForDashboard()
        {
            return Ok(_contentService.GetContentForDashboard());
        }

        [HttpPost]
        public IActionResult Post([FromBody] WebContentModel model)
        {
            if (ModelState.IsValid)
            {
                _contentService.PostContent(model);
                model = _contentService.GetContent(model.ContentPageId.ToString());

                //  ListResponseModel< WebContentModel > listResponse = new
                
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        [Route("/api/content/pages")]
        public IActionResult GetAllPages()
        {
            List<SelectListItem> pages = new List<SelectListItem>();

            var pageList = _contentService.GetAllPages();

            foreach (var page in pageList)
            {
                pages.Add(new SelectListItem() { Text = page.PageName, Value = page.PageId });
            }

            return Ok(pages);

        }

    }
}