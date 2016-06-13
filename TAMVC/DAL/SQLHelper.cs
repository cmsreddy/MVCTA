using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using TAMVC.Models;
using TAMVC.Models.DBModels;

namespace TAMVC.DAL
{
    public class SQLHelper
    {
        TAC_Team6Entities db = new TAC_Team6Entities();

        public List<CatagoryList> ReadCatagory()
        {
            List<CatagoryList> Catgorylist = new List<CatagoryList>();
            try
            {
                var allCategoryList = db.TAC_Category.AsEnumerable().ToList();
                foreach (var item in allCategoryList)
                {
                    CatagoryList obj = new CatagoryList()
                    {
                        CategoryID = Convert.ToInt32(item.CategoryId),
                        CategoryNames = Convert.ToString(item.CategoryName),
                        CategoryImagesSource = Convert.ToString(item.CategoryImage),
                    };
                    Catgorylist.Add(obj);
                }

            }
            catch (Exception ex)
            {

            }
            return Catgorylist;
        }

        public DetailsModel GetClassifiedById(int classifiedId)
        {
            TAC_Classified classified = new TAC_Classified();
            if (classifiedId != 0)
            {
                classified = db.TAC_Classified.Where(x => x.ClassifiedId == classifiedId).FirstOrDefault();
                return new DetailsModel()
                {
                    CategoryId = classified.CategoryId,
                    ClassifiedId = classified.ClassifiedId,
                    ClassifiedImage = classified.ClassifiedImage,
                    ClassifiedPrice = classified.ClassifiedPrice,
                    ClassifiedTitle = classified.ClassifiedTitle,
                    CreatedBy = classified.CreatedBy,
                    Description = classified.Description,
                    PostedDate = classified.PostedDate,
                    Summary = classified.Summary
                };
            }
            return null;
        }

        public string UserGUID(string email)
        {
            var emailId = db.TAC_User.Where(u => u.Email == email).FirstOrDefault().UserId;
            email = emailId.ToString();
            return email;
        }
    }
}