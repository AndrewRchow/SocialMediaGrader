using Memeni.Models.Requests;
using Memeni.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using Memeni.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memeni.Data.Providers;
using Memeni.Services.Interfaces;


namespace Memeni.Services
{
    public class HelpService : BaseService, IHelpService
    {
        public HelpService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public int Insert(HelpAddRequest model)
        {
            int Id = 0;
            DataProvider.ExecuteNonQuery("dbo.Help_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@DispName", model.DispName);
                    paramCollection.AddWithValue("@HelpCategoryId", model.HelpCategoryId);
                    paramCollection.AddWithValue("@Title", model.Title);
                    paramCollection.AddWithValue("@HelpMsg", model.HelpMsg);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);

                    SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    idParameter.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(idParameter);
                }
                , returnParameters: delegate (SqlParameterCollection param)
                {
                    Int32.TryParse(param["@Id"].Value.ToString(), out Id);
                });
            return Id;
        }
        public List<Help> Get()
        {
            List<Help> list = new List<Help>();
            DataProvider.ExecuteCmd("dbo.Help_SelectAll"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    Help singleItem = Mapper(reader);
                    list.Add(singleItem);
                });
            return list;
        }
        public Help Get(int id)
        {
            Help singleItem = null;
            DataProvider.ExecuteCmd("dbo.Help_SelectById"
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
        public void Update(HelpUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Help_Update"
            , inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", model.Id);
                paramCollection.AddWithValue("@DispName", model.DispName);
                paramCollection.AddWithValue("@HelpCategoryId", model.HelpCategoryId);
                paramCollection.AddWithValue("@Title", model.Title);
                paramCollection.AddWithValue("@HelpMsg", model.HelpMsg);
                paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
            });
        }
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.Help_Delete"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                });
        }

        private Help Mapper(IDataReader reader)
        {
            Help singleItem = new Help();
            int index = 0;

            singleItem.Id = reader.GetSafeInt32(index++);
            singleItem.DispName = reader.GetSafeString(index++);
            singleItem.HelpCategoryId = reader.GetSafeInt32(index++);
            singleItem.Title = reader.GetSafeString(index++);
            singleItem.HelpMsg = reader.GetSafeString(index++);
            singleItem.CreatedDate = reader.GetSafeDateTime(index++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(index++);
            singleItem.ModifiedBy = reader.GetSafeString(index++);

            return singleItem;
        }

        public List<Help> GetByUrl(string PageUrl)
        {
            List<Help> list = new List<Help>();
            DataProvider.ExecuteCmd("dbo.Help_GetByUrl"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PageUrl", PageUrl);
                }
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    Help singleItem = Mapper(reader);
                    list.Add(singleItem);
                }
                );
            return list;
        }
        public List<HelpCategories> GetCategories()
        {
            List<HelpCategories> list = new List<HelpCategories>();
            DataProvider.ExecuteCmd("dbo.HelpCategories_GetCategories"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    HelpCategories singleItem = new HelpCategories();
                    int index = 0;
                    singleItem.Id = reader.GetSafeInt32(index++);
                    singleItem.Name = reader.GetSafeString(index++);
                    singleItem.UrlPath = reader.GetSafeString(index++);
                    list.Add(singleItem);
                });
            return list;
        }
        public HelpGrid GetGrid(HelpGridRequest model)
        {
            HelpGrid singleItem = new HelpGrid();

            DataProvider.ExecuteCmd("dbo.Help_Grid"
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
                            Help h = Mapper(reader);

                            if (singleItem.data == null)
                            {
                                singleItem.data = new List<Help>();
                            }
                            singleItem.data.Add(h);
                            break;
                        default:
                            singleItem = null;
                            break;
                    }
                });

            return singleItem;
        }
    }
}
