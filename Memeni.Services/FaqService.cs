using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Memeni.Services
{
    public class FaqService : BaseService, IFaqService
    {
        public FaqService(IDataProvider dataProvider) : base(dataProvider)
        {
        }
        public List<FaqIndexModel> Get()
        {
            List<FaqIndexModel> list = new List<FaqIndexModel>();
            this.DataProvider.ExecuteCmd("dbo.Faq_GetAllJoined"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    FaqIndexModel singleItem = MapperFaqIndexModel(reader);
                    list.Add(singleItem);
                });
            return list;
        }
        private FaqIndexModel MapperFaqIndexModel(IDataReader reader)
        {
            FaqIndexModel singleItem = new FaqIndexModel();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.Title = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.displayOrder = reader.GetSafeInt32(startingIndex++);
            singleItem.displayName = reader.GetSafeString(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            return singleItem;
        }
        public Faq GetById(int Id)
        {

            Faq singleItem = new Faq();
            DataProvider.ExecuteCmd("dbo.Faq_GetById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", Id);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   singleItem = Mapper(reader);
               });
            return singleItem;
        }
        private Faq Mapper(IDataReader reader)
        {
            Faq singleItem = new Faq();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.Title = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.DisplayOrder = reader.GetSafeInt32(startingIndex++);
            singleItem.CategoryId = reader.GetSafeInt32(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);

            return singleItem;
        }
        public int Insert(FaqAddRequest model)
        {
            int Id = 0;
            DataProvider.ExecuteNonQuery("dbo.Faq_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Title", model.Title);
                    paramCollection.AddWithValue("@Description", model.Description);
                    paramCollection.AddWithValue("@DisplayOrder", model.DisplayOrder);
                    paramCollection.AddWithValue("@CategoryId", model.CategoryId);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);

                    SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    idParameter.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(idParameter);
                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    Int32.TryParse(param["@Id"].Value.ToString(), out Id);
                });
            return Id;
        }
        public void Update(FaqUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Faq_UpdateById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", model.Id);
                   paramCollection.AddWithValue("@Title", model.Title);
                   paramCollection.AddWithValue("@Description", model.Description);
                   paramCollection.AddWithValue("@CategoryId", model.CategoryId);
                   paramCollection.AddWithValue("@DisplayOrder", model.DisplayOrder);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
               });
        }
        public void Delete(int Id)
        {
            DataProvider.ExecuteNonQuery("dbo.Faq_Delete"
         , inputParamMapper: delegate (SqlParameterCollection paramCollection)
         {
             paramCollection.AddWithValue("@Id", Id);
         });
        }
    }
}
