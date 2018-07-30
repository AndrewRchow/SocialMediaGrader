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
    public class ConfigDataService : BaseService, IConfigDataService
    {
        public ConfigDataService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public List<ConfigType> Get()
        {
            List<ConfigType> list = new List<ConfigType>();
            this.DataProvider.ExecuteCmd("dbo.ConfigType_SelectAll"
               , inputParamMapper: null
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   ConfigType singleItem = Mapper(reader);
                   list.Add(singleItem);
               });

            return list;
        }

        public ConfigType Get(int Id)
        {
            ConfigType singleItem = null;
            DataProvider.ExecuteCmd("dbo.ConfigType_SelectByID"
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

        private ConfigType Mapper(IDataReader reader)
        {
            ConfigType singleItem = new ConfigType();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.DisplayName = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(5);
            return singleItem;
        }

        public int Insert(ConfigDataAddRequest model)
        {
            int Id = 0;
            DataProvider.ExecuteNonQuery("dbo.ConfigType_Insert"
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

        public void Update(ConfigDataUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.ConfigType_Update"
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
                DataProvider.ExecuteNonQuery("dbo.ConfigType_Delete"
             , inputParamMapper: delegate (SqlParameterCollection paramCollection)
             {
                 paramCollection.AddWithValue("@Id", Id);
             });
            }
        }
    }
}