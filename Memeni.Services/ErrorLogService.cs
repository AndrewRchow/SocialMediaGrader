using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.Responses;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Memeni.Services
{
    public class ErrorLogService : BaseService, IErrorLogService
    {
        public ErrorLogService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public ErrorLogService() : base()
        {
        }

        public int Add(ErrorLogAddRequest model)
        {
            int errorId = 0;
            DataProvider.ExecuteNonQuery("dbo.ErrorLog_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ErrorMessage", model.ErrorMessage);
                    paramCollection.AddWithValue("@ErrorNumber", model.ErrorNumber);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                    paramCollection.AddWithValue("@ErrorSeverity", model.ErrorSeverity);
                    paramCollection.AddWithValue("@ErrorState", model.ErrorState);
                    paramCollection.AddWithValue("@ErrorProcedure", model.ErrorProcedure);
                    paramCollection.AddWithValue("@ErrorLine", model.ErrorLine);

                    SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    idParameter.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(idParameter);
                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    Int32.TryParse(param["@Id"].Value.ToString(), out errorId);
                });
            return errorId;
        }

        public void Schedule(ErrorLogAddRequest model)
        {
            int errorId = 0;
            DataProvider.ExecuteNonQuery("dbo.ErrorLog_Insert"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ErrorMessage", model.ErrorMessage);
                    paramCollection.AddWithValue("@ErrorNumber", model.ErrorNumber);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                    paramCollection.AddWithValue("@ErrorSeverity", model.ErrorSeverity);
                    paramCollection.AddWithValue("@ErrorState", model.ErrorState);
                    paramCollection.AddWithValue("@ErrorProcedure", model.ErrorProcedure);
                    paramCollection.AddWithValue("@ErrorLine", model.ErrorLine);

                    SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    idParameter.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(idParameter);
                }, returnParameters: delegate (SqlParameterCollection param)
                    {
                        Int32.TryParse(param["@Id"].Value.ToString(), out errorId);
                    });
        }

        public ErrorLog Get(int id)
        {
            ErrorLog singleItem = null;
            DataProvider.ExecuteCmd("dbo.ErrorLog_SelectById"
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

        public List<ErrorLog> GetAll()
        {
            List<ErrorLog> list = new List<ErrorLog>();
            DataProvider.ExecuteCmd("dbo.ErrorLog_SelectAll"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    ErrorLog singleItem = Mapper(reader);                  
                    list.Add(singleItem);
                });
            return list;
        }

        private ErrorLog Mapper(IDataReader reader)
        {
            ErrorLog singleItem = new ErrorLog();
            int startingIndex = 0;

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.ErrorMessage = reader.GetSafeString(startingIndex++);
            singleItem.ErrorNumber = reader.GetSafeInt32(startingIndex++);
            singleItem.ModifiedDate = reader.GetDateTime(startingIndex++);
            singleItem.CreateDate = reader.GetDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            singleItem.ErrorSeverity = reader.GetSafeInt32(startingIndex++);
            singleItem.ErrorState = reader.GetSafeInt32(startingIndex++);
            singleItem.ErrorProcedure = reader.GetSafeString(startingIndex++);
            singleItem.ErrorLine = reader.GetSafeInt32(startingIndex++);
            return singleItem;
        }

        private ErrorLogSeverity SeverityMapper(IDataReader reader)
        {
            ErrorLogSeverity singleItem = new ErrorLogSeverity();
            int startingIndex = 0;

            singleItem.ErrorSeverity = reader.GetSafeInt32(startingIndex++);
            singleItem.ErrorCount = reader.GetSafeInt32(startingIndex++);
            return singleItem;
        }

        public void Put(ErrorLogUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.ErrorLog_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@ErrorMessage", model.ErrorMessage);
                    paramCollection.AddWithValue("@ErrorNumber", model.ErrorNumber);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                    paramCollection.AddWithValue("@ErrorSeverity", model.ErrorSeverity);
                    paramCollection.AddWithValue("@ErrorState", model.ErrorState);
                    paramCollection.AddWithValue("@ErrorProcedure", model.ErrorProcedure);
                    paramCollection.AddWithValue("@ErrorLine", model.ErrorLine);
                });
 
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.ErrorLog_Delete"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {                    
                  paramCollection.AddWithValue("@Id", id);
                });
        }

        public ErrorLogGrid GetGrid(ErrorLogGridRequest model)
        {
            ErrorLogGrid singleItem = new ErrorLogGrid();
            
            DataProvider.ExecuteCmd("dbo.ErrorLog_Grid"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@DisplayLength", model.displayLength);
                    paramCollection.AddWithValue("@DisplayStart", model.displayStart);
                    paramCollection.AddWithValue("@SortCol", model.sortCol);
                    paramCollection.AddWithValue("@SortDir", model.sortDir);
                    paramCollection.AddWithValue("@Search", model.search);
                }
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    switch (set)
                    {
                        case 0:
                            singleItem.recordsTotal = reader.GetSafeInt32(0);
                            break;
                        case 1:
                            singleItem.recordsFiltered = reader.GetSafeInt32(0);
                            break;
                        case 2:
                            ErrorLog e = Mapper(reader);
                            if(singleItem.data == null)
                            {
                                singleItem.data = new List<ErrorLog>();
                            }
                            singleItem.data.Add(e);
                            break;
                        default:
                            singleItem = null;
                            break;
                    }
                });
            return singleItem;
        }

        public void Truncate()
        {
            DataProvider.ExecuteNonQuery("dbo.ErrorLog_Truncate"
            , inputParamMapper: null);
        }

        public void MultiDelete(int[] ids)
        {
            DataProvider.ExecuteNonQuery("dbo.ErrorLog_DeleteTable"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@TableId", CreateDataTable(ids));
                });
        }

        public List<ErrorLogSeverity> GetSeverity()
        {
            List<ErrorLogSeverity> list = new List<ErrorLogSeverity>();
            DataProvider.ExecuteCmd("dbo.ErrorLog_SeverityCount"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                 {
                     ErrorLogSeverity singleItem = SeverityMapper(reader);
                     list.Add(singleItem);
                 });
            return list;
        }

        public void SeverityCount()
        {
            List<ErrorLogSeverity> list = GetSeverity();
        }

        private static DataTable CreateDataTable(int[] ids)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            foreach (var id in ids)
            {
                table.Rows.Add(id);
            }
            return table;
        }

        public void LogError(Exception ex)
        {
            ErrorLogAddRequest model = new ErrorLogAddRequest();
            model.ErrorMessage = ex.Message;
            model.ErrorNumber = ex.HResult;
            model.ErrorLine = 0;
            model.ErrorProcedure = ex.StackTrace;
            model.ErrorSeverity = 2;
            model.ErrorState = 0;

            this.LogError(model);
        }

        public void LogError(ErrorLogAddRequest model)
        {
            try
            {
                DataProvider.ExecuteNonQuery("dbo.ErrorLog_Insert"
            , inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@ErrorMessage", model.ErrorMessage);
                paramCollection.AddWithValue("@ErrorNumber", model.ErrorNumber);
                paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                paramCollection.AddWithValue("@ErrorSeverity", model.ErrorSeverity);
                paramCollection.AddWithValue("@ErrorState", model.ErrorState);
                paramCollection.AddWithValue("@ErrorProcedure", model.ErrorProcedure);
                paramCollection.AddWithValue("@ErrorLine", model.ErrorLine);
            });
            }
            catch (Exception ex)
            {
                // swallow exception at this point
                // no need to create a snowball effect on errors
                // potentially log to a text file on the drive
                System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/App_Data/application.log"),ex.ToString());
             }
        }

        public void LogMessage(ErrorLogAddRequest model)
        {
            model.ErrorSeverity = 0;
            this.LogError(model);
            throw new NotImplementedException();
        }

        public void LogWarningError(ErrorLogAddRequest model)
        {
            model.ErrorSeverity = 1;
            this.LogError(model);
            throw new NotImplementedException();
        }

        public void LogRegularError(ErrorLogAddRequest model)
        {
            model.ErrorSeverity = 2;
            this.LogError(model);
            throw new NotImplementedException();
        }

        public void LogCriticalError(ErrorLogAddRequest model)
        {
            model.ErrorSeverity = 3;
            this.LogError(model);
            throw new NotImplementedException();
        }
    }
}
