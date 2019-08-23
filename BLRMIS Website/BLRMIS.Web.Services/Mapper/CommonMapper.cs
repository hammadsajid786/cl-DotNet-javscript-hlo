using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace BLRMIS.Web.Services.Mapper
{
   public  static class CommonMapper
    {
        public static List<SelectListModel> MapDistrictSelectList(List<LrmisWebDistrict> TSourceList)
        {
            return TSourceList.ConvertAll<SelectListModel>(i => new SelectListModel
            {
                Text = i.DistrictName,
                Value = i.Id.ToString()
            });
        }
        public static List<SelectListModel> MapDesignationList(List<LrmisWebDesignation> TSourceList)
        {
            return TSourceList.ConvertAll<SelectListModel>(i => new SelectListModel
            {
                Text = i.DesignationName,
                Value = i.DesignationId.ToString()
            });
        }
        public static List<SelectListModel> MapDepartmentList(List<LrmisWebDepartment> TSourceList)
        {
            return TSourceList.ConvertAll<SelectListModel>(i => new SelectListModel
            {
                Text = i.DepartmentName,
                Value = i.DepartmentId.ToString()
            });
        }

        public static List<SelectListModel> MapLocationList(List<LrmisWebLocation> TSourceList)
        {
            return TSourceList.ConvertAll<SelectListModel>(i => new SelectListModel
            {
                Text = i.LocationName,
                Value = i.LocationId.ToString()
            });
        }

        public static List<SelectListModel> MapRoleList(List<LrmisWebRole> TSourceList)
        {
            return TSourceList.ConvertAll<SelectListModel>(i => new SelectListModel
            {
                Text = i.RoleName,
                Value = i.RoleId.ToString()
            });
        }
        public static List<SelectListModel> MapComplaintTypeSelectList(List<LrmisWebComplaintTypes> TSourceList)
        {
            return TSourceList.ConvertAll<SelectListModel>(i => new SelectListModel
            {
                Text = i.Title,
                Value = i.Id.ToString()
            });
        }
        public static List<SelectListModel> MapComplaintStatusSelectList(List<LrmisWebComplaintStatus> TSourceList)
        {
            return TSourceList.ConvertAll<SelectListModel>(i => new SelectListModel
            {
                Text = i.ComplaintStatus,
                Value = i.ComplaintStatusId.ToString()
            });
        }

        public static List<SelectListModel> MapUserSelectList(List<LrmisWebUser> TSourceList)
        {
            return TSourceList.ConvertAll<SelectListModel>(i => new SelectListModel
            {
                Text = i.UserName,
                Value = i.UserId.ToString()
            });
        }

        public static List<SelectListModel> MapCategorySelectList(List<LrmisWebCategory> list)
        {
            return list.ConvertAll<SelectListModel>(i => new SelectListModel
            {
                Text = i.CategoryName,
                Value = i.CategoryId.ToString()
            });
        }

        public static List<SelectListModel> MapComplaintStatusList(List<LrmisWebComplaintStatus> TSourceList)
        {
            return TSourceList.ConvertAll<SelectListModel>(i => new SelectListModel
            {
                Text = i.ComplaintStatus,
                Value = i.ComplaintStatusId.ToString()
            });
        }

    }
}
