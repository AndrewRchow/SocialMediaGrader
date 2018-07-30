using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memeni.Data.Providers;
using System.Data.SqlClient;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System.Data;
using Memeni.Data;
using Memeni.Services.Interfaces;

namespace Memeni.Services
{
    public class PrivacyService : BaseService, IPrivacyService
    {
        public PrivacyService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public int Insert(PrivacyAddRequest model)
        {
            int id = 0;
            DataProvider.ExecuteNonQuery("dbo.Privacy_Insert"
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

        public List<Privacy> Get()
        {
            List<Privacy> list = new List<Privacy>();
            DataProvider.ExecuteCmd("dbo.Privacy_SelectAll"
               , inputParamMapper: null
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   Privacy singleItem = Mapper(reader);
                   list.Add(singleItem);
               });
            return list;
        }

        public Privacy Get(int id)
        {
            Privacy singleItem = null;
            DataProvider.ExecuteCmd("dbo.Privacy_SelectById"
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
       
        private Privacy Mapper(IDataReader reader)
        {
            Privacy singleItem = new Privacy();
            int startingIndex = 0;
            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.ParentId = reader.GetSafeInt32(startingIndex++);
            singleItem.Title = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.DisplayOrder = reader.GetSafeInt32(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            return singleItem;
        }

        public void Update(PrivacyUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Privacy_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@ParentId", model.ParentId);
                    paramCollection.AddWithValue("@Title", (Object)model.Title ?? DBNull.Value);
                    paramCollection.AddWithValue("@Description", (Object)model.Description ?? DBNull.Value);
                    paramCollection.AddWithValue("@DisplayOrder", model.DisplayOrder);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                });
        }

        public void Update2(List<Privacy> model)
        {
            DataProvider.ExecuteNonQuery("dbo.Privacy_UpdateMultiple"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PrivacyTableType", CreateTableType(model));
                });
        }

        private static DataTable CreateTableType(List<Privacy> model)
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

            foreach (Privacy item in model)
            {
                DataRow newRow = table.NewRow();
                newRow["Id"] = item.Id;
                newRow["ParentId"] = item.ParentId;
                newRow["Title"] = item.Title;
                newRow["Description"] = item.Description;
                newRow["DisplayOrder"] = item.DisplayOrder;
                newRow["CreatedDate"] = item.CreatedDate;
                newRow["ModifiedDate"] = item.ModifiedDate;
                newRow["ModifiedBy"] = item.ModifiedBy;

                table.Rows.Add(newRow);                              
            }
            return table;
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.Privacy_Delete"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                });
        }
    }
}
