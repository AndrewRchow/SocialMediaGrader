using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using System.Data;
using Memeni.Data;
using Memeni.Services.Interfaces;

namespace Memeni.Services
{
    public class TncService : BaseService, ITncService
    {
        public TncService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public int Insert(TncAddRequest model)
        {
            int id = 0;
            DataProvider.ExecuteNonQuery("dbo.TnC_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ParentId", model.ParentId);
                    paramCollection.AddWithValue("@DisplayOrder", model.DisplayOrder);
                    paramCollection.AddWithValue("@Title", (Object)model.Title ?? DBNull.Value);
                    paramCollection.AddWithValue("@Description", (Object)model.Description ?? DBNull.Value);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);

                    SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    idParameter.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(idParameter);
                }
                , returnParameters: delegate (SqlParameterCollection param)
                {
                    Int32.TryParse(param["@Id"].Value.ToString(), out id);
                });
            return id;
        }

        public Tnc Get(int id)
        {
            Tnc singleItem = null;
            DataProvider.ExecuteCmd("dbo.TnC_SelectById"
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

        public List<Tnc> Get()
        {
            List<Tnc> list = new List<Tnc>();
            DataProvider.ExecuteCmd("dbo.TnC_SelectAll"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    Tnc singleItem = Mapper(reader);
                    list.Add(singleItem);
                });
            return list;
        }

        private Tnc Mapper(IDataReader reader)
        {
            Tnc singleItem = new Tnc();
            int startingIndex = 0;
            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            singleItem.ParentId = reader.GetSafeInt32(startingIndex++);
            singleItem.Title = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.DisplayOrder = reader.GetSafeInt32(startingIndex++);
            return singleItem;
        }

        public void Update(TncUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.TnC_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@ParentId", model.ParentId);
                    paramCollection.AddWithValue("@DisplayOrder", model.DisplayOrder);
                    paramCollection.AddWithValue("@Title", (Object)model.Title ?? DBNull.Value);
                    paramCollection.AddWithValue("@Description", (Object)model.Description ?? DBNull.Value);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                });
        }

        public void UpdateAll(List<Tnc> TncTable)
        {
            DataProvider.ExecuteNonQuery("dbo.TnC_UpdateTable"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@TableVariable", CreateDataTable(TncTable));
                });
        }

        private static DataTable CreateDataTable(List<Tnc> TncTable)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("ParentId", typeof(int));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("DisplayOrder", typeof(int));
            table.Columns.Add("CreatedDate", typeof(DateTime));
            table.Columns.Add("ModifiedDate", typeof(DateTime));
            table.Columns.Add("ModifiedBy", typeof(string));
            foreach (Tnc item in TncTable)
            {
                table.Rows.Add(item.Id, item.ParentId, item.Title, item.Description, item.DisplayOrder, item.CreatedDate, item.ModifiedDate, item.ModifiedBy);
            }
            return table;
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.TnC_Delete"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                });
        }
    }
}
