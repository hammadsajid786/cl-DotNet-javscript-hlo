using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Get([FromQuery(Name = "param")]string searchParams)
        {
            return Ok(categoryService.GetCategoryList(searchParams));
        }

        [HttpGet]
        [Route("/api/categories/short-list")]
        [AllowAnonymous]
        public ActionResult<SelectListModel> GetCategoryListForSelectList()
        {
            return Ok(categoryService.GetCategoryListShort());
        }

        [HttpGet("{CategoryId}")]
        public ActionResult Get(int CategoryId)
        {
            return Ok(categoryService.GetCategoryById(CategoryId));
        }

        [HttpPost]
        public ActionResult Post([FromBody] WebCategoryModel CategoryModel)
        {
            categoryService.SaveCategory(CategoryModel);
            return Ok();
        }

        [HttpPut("{CategoryId}")]
        public ActionResult Put(int CategoryId, [FromBody] WebCategoryModel CategoryModel)
        {
            categoryService.UpdateCategory(CategoryId, CategoryModel);
            return Ok();
        }

        [HttpGet("{CategoryId}/ChangeStatus")]
        public IActionResult ChangeStatus(int CategoryId)
        {
            categoryService.ChangeStatus(CategoryId);
            return Ok();
        }
    }
}