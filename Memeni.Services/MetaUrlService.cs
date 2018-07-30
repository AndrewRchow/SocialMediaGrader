using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services
{
    public class MetaUrlService : BaseService, IMetaUrlService
    {
        public MetaUrlService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public List<MetaUrl> Get()
        {
            List<MetaUrl> list = new List<MetaUrl>();
            DataProvider.ExecuteCmd("dbo.MetaOT_SelectAll"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    MetaUrl singleItem = Mapper(reader);
                    list.Add(singleItem);
                });
            return list;
        }

        public MetaUrl Get(int id)
        {
            MetaUrl singleItem = null;
            DataProvider.ExecuteCmd("dbo.MetaOT_SelectById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                }
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    singleItem = Mapper(reader);
                });
            return singleItem;
        }

        private MetaUrl Mapper(IDataReader reader)
        {
            MetaUrl singleItem = new MetaUrl();
            int startingIndex = 0;
            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.Name = reader.GetSafeString(startingIndex++);
            singleItem.Url = reader.GetSafeString(startingIndex++);
            singleItem.IsLocked = reader.GetSafeBool(startingIndex++);
            return singleItem;
        }

        public int Insert(MetaUrlAddRequest model)
        {
            int id = 0;
            DataProvider.ExecuteNonQuery("dbo.MetaOT_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Name", model.Name);
                    paramCollection.AddWithValue("@Url", model.Url);

                    SqlParameter idParamter = new SqlParameter("@Id", SqlDbType.Int);
                    idParamter.Direction = ParameterDirection.Output;

                    paramCollection.Add(idParamter);
                }
                , returnParameters: delegate (SqlParameterCollection param)
                {
                    Int32.TryParse(param["@Id"].Value.ToString(), out id);
                });
            return id;
        }

        public void Update(MetaUrlUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.MetaOT_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@Name", model.Name);
                    paramCollection.AddWithValue("@Url", model.Url);
                });
        }

        public void UpdateLock(MetaUrlUpdateLockRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.MetaOT_UpdateLock"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@IsLocked", Convert.ToByte(model.IsLocked));
                });
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.MetaOT_Delete"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                });
        }
    }
}
