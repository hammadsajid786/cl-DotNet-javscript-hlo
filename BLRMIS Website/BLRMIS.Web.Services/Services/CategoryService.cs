using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using BLRMIS.Web.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLRMIS.Web.Services.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        IRepository<LrmisWebCategory> categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            categoryRepository = unitOfWork.GetRepository<LrmisWebCategory>();
        }

        public ListResponseModel<WebCategoryModel> GetCategoryList(string searchParams)
        {
            Expression<Func<LrmisWebCategory, bool>> predicate = null;
            Func<IQueryable<LrmisWebCategory>, IOrderedQueryable<LrmisWebCategory>> sortexp = null;
            int index = 0;
            int size = 0;
            IQueryableExtensions.GetFilters<LrmisWebCategory>(searchParams, ref predicate, ref sortexp, ref index, ref size);
            var result = categoryRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size);
            List<WebCategoryModel> liWebCategoryModel = EntityMapper.Mapper.Map<List<WebCategoryModel>>(result.Items);

            ListResponseModel<WebCategoryModel> listResponseModel = new ListResponseModel<WebCategoryModel>();
            listResponseModel.Records = liWebCategoryModel;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;
            return listResponseModel;
        }

        public WebCategoryModel GetCategoryById(int CategoryId)
        {
            return EntityMapper.Mapper.Map<WebCategoryModel>(categoryRepository.GetById(CategoryId));
        }

        public void SaveCategory(WebCategoryModel CategoryModel)
        {
            CategoryModel.CreatedBy = 4;
            categoryRepository.Insert(EntityMapper.Mapper.Map<LrmisWebCategory>(CategoryModel));
            categoryRepository.Save();
        }

        public void UpdateCategory(int CategoryId, WebCategoryModel CategoryModel)
        {
            LrmisWebCategory _category = categoryRepository.GetById(CategoryId);

            WebCategoryModel tempCategory = new WebCategoryModel();
            tempCategory.CreatedDate = _category.CreatedDate;
            tempCategory.CreatedBy = _category.CreatedBy;
            if (_category.CategoryId > 0)
            {
                categoryRepository.SetState(_category, Microsoft.EntityFrameworkCore.EntityState.Detached);
                var category = EntityMapper.Mapper.Map<LrmisWebCategory>(CategoryModel);
                category.CreatedBy = tempCategory.CreatedBy;
                category.CreatedDate = tempCategory.CreatedDate;
                category.ModifiedDate = DateTime.Now;
                CategoryModel.ModifiedBy = 4;
                categoryRepository.Update(category);
                categoryRepository.Save();
            }
        }
        public IEnumerable<SelectListModel> GetCategoryListShort()
        {
            return CommonMapper.MapCategorySelectList(categoryRepository.GetAll(i=>i.Active == true));
        }
        public void ChangeStatus(int CategoryId)
        {
            LrmisWebCategory category = categoryRepository.GetById(CategoryId);
            category.Active = !category.Active;
            categoryRepository.Update(category);
            categoryRepository.Save();
        }
    }
}
