using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Memeni.Services.Interfaces;
using Memeni.Models.ViewModels;
using System.Web;

namespace Memeni.Services
{
    public class ConfigService : BaseService, IConfigService
    {
        public ConfigService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public List<ConfigIndexModel> Get()
        {
            List<ConfigIndexModel> list = new List<ConfigIndexModel>();
            this.DataProvider.ExecuteCmd("dbo.Config_SelectAll"
               , inputParamMapper: null
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   ConfigIndexModel singleItem = MapConfigIndexModel(reader);
                   list.Add(singleItem);
               });

            return list;
        }

        private ConfigIndexModel MapConfigIndexModel(IDataReader reader)
        {
            ConfigIndexModel singleItem = new ConfigIndexModel();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.ConfigName = reader.GetSafeString(startingIndex++);
            singleItem.ConfigValue = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.CategoryName = reader.GetSafeString(startingIndex++);
            singleItem.DataTypeName = reader.GetSafeString(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            return singleItem;
        }

        private Config Mapper(IDataReader reader)
        {
            Config singleItem = new Config();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.ConfigName = reader.GetSafeString(startingIndex++);
            singleItem.ConfigValue = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.ConfigTypeId = reader.GetSafeInt32(startingIndex++);
            singleItem.ConfigCategoryId = reader.GetSafeInt32(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);

            return singleItem;
        }

        private Config mapperWithDisplayName(IDataReader reader)
        {
            Config singleItem = new Config();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.ConfigName = reader.GetSafeString(startingIndex++);
            singleItem.ConfigValue = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.ConfigTypeId = reader.GetSafeInt32(startingIndex++);
            singleItem.ConfigCategoryId = reader.GetSafeInt32(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            singleItem.DisplayName = reader.GetSafeString(startingIndex++);

            return singleItem;
        }

        public Config GetById(int Id)
        {
            Config singleItem = new Config();
            DataProvider.ExecuteCmd("dbo.Config_GETALL"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", Id);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   switch (set)
                   {
                       case 0:
                           singleItem = Mapper(reader);
                           break;
                       case 1:
                           singleItem.ConfigCat = MapConfigCat(reader);
                           //create mapper for configCategory
                           break;
                       case 2:
                           singleItem.ConfigDat = MapConfigType(reader);
                           break;
                   }
               });
            return singleItem;
        }

        public void Update(ConfigUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Config_UpdateById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@ConfigName", model.ConfigName);
                    paramCollection.AddWithValue("@ConfigValue", model.ConfigValue);
                    paramCollection.AddWithValue("@Description", model.Description);
                    paramCollection.AddWithValue("@ConfigTypeId", model.ConfigTypeId);
                    paramCollection.AddWithValue("@ConfigCategoryId", model.ConfigCategoryId);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                });
            //HttpContext.Current.Cache.Remove(model.ConfigName);
        }


        public int Insert(ConfigAddRequest model)
        {
            int Id = 0;
            DataProvider.ExecuteNonQuery("dbo.Config_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ConfigName", model.ConfigName);
                   paramCollection.AddWithValue("@ConfigValue", model.ConfigValue);
                   paramCollection.AddWithValue("@Description", model.Description);
                   paramCollection.AddWithValue("@ConfigTypeId", model.ConfigTypeId);
                   paramCollection.AddWithValue("@ConfigCategoryId", model.ConfigCategoryId);
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

        private ConfigType MapConfigType(IDataReader reader)
        {
            ConfigType singleItem = new ConfigType();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.DisplayName = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            return singleItem;
        }

        private ConfigCategory MapConfigCat(IDataReader reader)
        {
            ConfigCategory singleItem = new ConfigCategory();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.DisplayName = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            return singleItem;
        }

        public void Delete(int Id)
        {
            DataProvider.ExecuteNonQuery("dbo.Config_Delete"
         , inputParamMapper: delegate (SqlParameterCollection paramCollection)
         {
             paramCollection.AddWithValue("@Id", Id);
         });
        }

        public Config GetByConfigName(string configName)
        {

            Config singleItem = new Config();

            object o = this.GetCache(configName);
            if (o == null)
            {
                DataProvider.ExecuteCmd("dbo.Config_SelectByConfigName"
                   , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                   {
                       paramCollection.AddWithValue("@configName", configName);
                   }
                   , singleRecordMapper: delegate (IDataReader reader, short set)
                   {
                       singleItem = mapperWithDisplayName(reader);

                   });
                //HttpContext.Current.Cache.Insert(configName, singleItem, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
                this.cacheInsert(configName, singleItem);
            }
            else
            {
                singleItem = (Config)o;
            }
            return singleItem;
        }

        private object GetCache(string configName)
        {
            object o = null;
            if (HttpContext.Current != null)
            {
                o = HttpContext.Current.Cache.Get(configName);
            }
            return o;
        }

        private void cacheInsert(string configName, object singleItem)
        {
            if (HttpContext.Current != null)
            {
                 HttpContext.Current.Cache.Insert(configName, singleItem, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }

        private void cacheDelete(string configName)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Cache.Remove(configName);
            }
        }
        public string getConfigValusAsString(string configName)
        {
            try
            {
                Config cfg = GetByConfigName(configName);
                return cfg.ConfigValue;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return "";
            }
        }
        public int getConfigValueAsInt(string configName)
        {
            Config cfg = GetByConfigName(configName);
            int cVal = 0;
            if (cfg.DisplayName.ToLower() == "int")
            {
                int.TryParse(cfg.ConfigValue, out cVal);
            }
            return cVal;
        }
        public bool getConfigValueAsBool(string configName)
        {
            Config cfg = GetByConfigName(configName);
            bool bval = false;
            if (cfg.DisplayName.ToLower() == "bool")
            {
                bool.TryParse(cfg.ConfigValue, out bval);
            }

            return bval;
        }
        public decimal getConfigValueAsDecimal(string configName)
        {
            Config cfg = GetByConfigName(configName);
            decimal dval = 0;
            if (cfg.DisplayName == "decimal")
            {
                decimal.TryParse(cfg.ConfigValue, out dval);
            }

            return dval;
        }

    }
}

