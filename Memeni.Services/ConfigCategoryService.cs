using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Memeni.Services
{
    public class ConfigCategoryService : BaseService, IConfigCategoryService
    {
        public ConfigCategoryService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public List<ConfigCategory> Get()
        {
            List<ConfigCategory> list = new List<ConfigCategory>();
            this.DataProvider.ExecuteCmd("dbo.ConfigCategory_SelectAll"
               , inputParamMapper: null
               , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    ConfigCategory singleItem = Mapper(reader);
                    list.Add(singleItem);
                });

            return list;
        }

        public ConfigCategory Get(int Id)
        {
            ConfigCategory singleItem = null;
            DataProvider.ExecuteCmd("dbo.ConfigCategory_SelectByID"
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

        private ConfigCategory Mapper(IDataReader reader)
        {
            ConfigCategory singleItem = new ConfigCategory();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.DisplayName = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(5);
            return singleItem;
        }

        public int Insert(ConfigCategoryAddRequest model)
        {
            int Id = 0;
            DataProvider.ExecuteNonQuery("dbo.ConfigCategory_Insert"
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

        public void Update(ConfigCategoryUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.ConfigCategory_Update"
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
            {
                DataProvider.ExecuteNonQuery("dbo.ConfigCategory_Delete"
             , inputParamMapper: delegate (SqlParameterCollection paramCollection)
             {
                 paramCollection.AddWithValue("@Id", Id);
             });
            }
        }
    }
}