using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services
{
    public class FaqCategoryService : BaseService, IFaqCategoryService
    {
        public FaqCategoryService(IDataProvider dataProvider) : base(dataProvider)
        {
        }
        public int Insert(FaqCategoryAddRequest model)
        {
            int Id = 0;
            DataProvider.ExecuteNonQuery("dbo.FaqCategory_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@DisplayName", model.DisplayName);
                    paramCollection.AddWithValue("@Description", model.Description);
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
        public List<FaqCategory> Get()
        {
            List<FaqCategory> list = new List<FaqCategory>();
            this.DataProvider.ExecuteCmd("dbo.FaqCategory_GetAll"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    FaqCategory singleItem = Mapper(reader);
                    list.Add(singleItem);
                });
            return list;
        }
        public FaqCategory GetById(int Id)
        {

            FaqCategory singleItem = new FaqCategory();
            DataProvider.ExecuteCmd("dbo.FaqCategory_GetById"
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
        private FaqCategory Mapper(IDataReader reader)
        {
            FaqCategory singleItem = new FaqCategory();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.DisplayName = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);

            return singleItem;
        }
        public void Update(FaqCategoryUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.FaqCategory_UpdateById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", model.Id);
                   paramCollection.AddWithValue("@DisplayName", model.DisplayName);
                   paramCollection.AddWithValue("@Description", model.Description);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
               });
        }
        public void Delete(int Id)
        {
            DataProvider.ExecuteNonQuery("dbo.FaqCategory_Delete"
         , inputParamMapper: delegate (SqlParameterCollection paramCollection)
         {
             paramCollection.AddWithValue("@Id", Id);
         });
        }

    }
}
