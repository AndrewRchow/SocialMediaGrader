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
    public class AnonTrackingService : BaseService, IAnonTrackingService
    {
        public AnonTrackingService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public AnonUser SelectUserByEmail(string Email)
        {
            AnonUser singleItem = null;
            DataProvider.ExecuteCmd("Anon_SelectByEmail"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Email", Email);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   singleItem = new AnonUser();
                   int startingIndex = 0; //startingOrdinal
                   singleItem.Id = reader.GetSafeInt32(startingIndex++);
                   singleItem.VisitCount = reader.GetSafeInt32(startingIndex++);
                   singleItem.DateCreated = reader.GetSafeDateTime(startingIndex++);

               });
            return singleItem;
        }

        public List<AnonUserUrls> SelectUrlsByUser(AnonUserChangeRequest model)
        {
            List<AnonUserUrls> list = new List<AnonUserUrls>();
            DataProvider.ExecuteCmd("dbo.AnonUrl_SelectByUser"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Email", model.Email);
                }
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    AnonUserUrls singleItem = new AnonUserUrls();
                    int startingIndex = 0; //startingOrdinal
                    singleItem.Id = reader.GetSafeInt32(startingIndex++);
                    singleItem.IdOfEmail = reader.GetSafeInt32(startingIndex++);
                    singleItem.Url = reader.GetSafeString(startingIndex++);
                    singleItem.TimesGraded = reader.GetSafeInt32(startingIndex++);
                    singleItem.DateCreated = reader.GetSafeDateTime(startingIndex++);
                    singleItem.DateModified = reader.GetSafeDateTime(startingIndex++);
                    list.Add(singleItem);
                });
            return list;
        }

        public string IncreaseVisitCount(AnonUserChangeRequest model)
        {
            string singleItem = "";
            this.DataProvider.ExecuteCmd("dbo.Anon_IncreaseVisitCount"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Email", model.Email);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   singleItem = reader.GetSafeString(0);
               });
            return singleItem;
        }

        public int InsertUser(AnonUserChangeRequest model)
        {
            int id = 0;
            DataProvider.ExecuteNonQuery("dbo.Anon_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Email", model.Email);
                    paramCollection.AddWithValue("@VisitCount", 1);
                    paramCollection.AddWithValue("@Url", model.Website);
                    paramCollection.AddWithValue("@TimesGraded", 1);

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

        public AnonUserUrls SelectUrlsByIdAndEmail(AnonUserChangeRequest model)
        {
            AnonUserUrls singleItem = null;
            DataProvider.ExecuteCmd("AnonUrl_SelectByIdAndEmail"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Email", model.Email);
                   paramCollection.AddWithValue("@Url", model.Website);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   singleItem = new AnonUserUrls();
                   int startingIndex = 0; //startingOrdinal
                   singleItem.Url = reader.GetSafeString(startingIndex++);
               });
            return singleItem;
        }

        public string IncreaseTimesGraded(AnonUserChangeRequest model)
        {
            string singleItem = "";
            this.DataProvider.ExecuteCmd("dbo.AnonUrl_IncreaseTimesGraded"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Email", model.Email);
                   paramCollection.AddWithValue("@Url", model.Website);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   singleItem = reader.GetSafeString(0);
               });
            return singleItem;
        }

        public int InsertUrl(AnonUserChangeRequest model)
        {
            int id = 0;
            DataProvider.ExecuteNonQuery("dbo.AnonUrl_InsertByEmail"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@USERID", 0);
                    paramCollection.AddWithValue("@Email", model.Email);
                    paramCollection.AddWithValue("@Url", model.Website);
                    paramCollection.AddWithValue("@TimesGraded", 1);

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

        public AnonTrackingGrid GetGrid(AnonTrackingGridRequest model)
        {
            AnonTrackingGrid singleItem = new AnonTrackingGrid();

            DataProvider.ExecuteCmd("dbo.AnonTracking_Grid"
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
                            AnonUser singleItem2 = new AnonUser();
                            int startingIndex = 0; //startingOrdinal
                            singleItem2.Id = reader.GetSafeInt32(startingIndex++);
                            singleItem2.Email = reader.GetSafeString(startingIndex++);
                            singleItem2.VisitCount = reader.GetSafeInt32(startingIndex++);
                            singleItem2.DateCreated = reader.GetSafeDateTime(startingIndex++);
                            singleItem2.DateModified = reader.GetSafeDateTime(startingIndex++);
                            if (singleItem.data == null)
                            {
                                singleItem.data = new List<AnonUser>();
                            }
                            singleItem.data.Add(singleItem2);
                            break;
                        default:
                            singleItem = null;
                            break;
                    }
                });
            return singleItem;
        }

        public void DeleteMultiple(int[] ids)
        {
            DataProvider.ExecuteNonQuery("dbo.AnonUser_DeleteMultiple"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", CreateDataTable(ids));
                });
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
    }
}
