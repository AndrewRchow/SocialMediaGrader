using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memeni.Models.Requests;
using Memeni.Data;
using Memeni.Models.Domain;
using System.Data.SqlClient;
using System.Data;
using Memeni.Data.Providers;
using Memeni.Services.Interfaces;

namespace Memeni.Services
{
    public class SMEService : BaseService, ISMEService
    {
        public SMEService(IDataProvider dataProvider) : base(dataProvider) { }
        public int Insert(SMEAddRequest model)
        {
            int Id = 0;
            
            DataProvider.ExecuteNonQuery(storedProc: "dbo.SME_Insert", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@ModBy", model.ModifiedBy);
                paramCollection.AddWithValue("@MaxInt", model.MaxInteractionsPer1k);
                paramCollection.AddWithValue("@MinInt", model.MinInteractionsPer1k);
                paramCollection.AddWithValue("@SumInt", model.SumInteractionsPer1k);

                SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                idParameter.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(idParameter);

            }, returnParameters: delegate (SqlParameterCollection param)
            {
                Int32.TryParse(param["@Id"].Value.ToString(), out Id);
            });
            return Id;
        }
        public void Update(SMEUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.SME_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@ModBy", model.ModifiedBy);
                    paramCollection.AddWithValue("@MaxInt", model.MaxInteractionsPer1k);
                    paramCollection.AddWithValue("@MinInt", model.MinInteractionsPer1k);
                    paramCollection.AddWithValue("@SumInt", model.SumInteractionsPer1k);
                }
            );
        }
        public SME SelectById(int id)
        {
            SME singleItem = null;

            this.DataProvider.ExecuteCmd("dbo.SME_SelectByID"
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
        public List<SME> SelectAll()
        {
            List<SME> list = new List<SME>();
            this.DataProvider.ExecuteCmd("dbo.SME_SelectAll"
               , inputParamMapper: null
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   SME singleItem = Mapper(reader);
                   list.Add(singleItem);
               });
            return list;
        }
        private SME Mapper(IDataReader reader)
        {
            SME singleItem = new SME();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            singleItem.MaxInteractionsPer1k = reader.GetSafeDecimal(startingIndex++);
            singleItem.MinInteractionsPer1k = reader.GetSafeDecimal(startingIndex++);
            singleItem.SumInteractionsPer1k = reader.GetSafeDecimal(startingIndex++);
            return singleItem;
        }
        public void DeleteById(int id)
        {
            this.DataProvider.ExecuteNonQuery("dbo.SME_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);
               });          
        }
    }
}

