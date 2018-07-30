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
    public class AdminUserService : BaseService, IAdminUserService
    {
        public AdminUserService(IDataProvider dataProvider) : base(dataProvider) { }

        public List<Users> GetAll()
        {
            List<Users> list = new List<Users>();
            this.DataProvider.ExecuteCmd("dbo.UserRoles_GetAll"
               , inputParamMapper: null
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   Users singleItem = Mapper(reader);
                   list.Add(singleItem);
               });
            return list;
        }
        public Users GetById(int id)
        {
            Users singleItem = null;

            this.DataProvider.ExecuteCmd("dbo.UserRoles_GetByID"
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
        public void Update(UsersUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.UserBase_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@Role", model.Role);
                    paramCollection.AddWithValue("@EmailConfirmed", model.EmailConfirmed);
                    paramCollection.AddWithValue("@Lock", model.Lock);
                }
            );
        }
        public void LockUser(UsersUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.UserBase_LockUser"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@Lock", model.Lock);
                }
            );
        }
        public void ConfirmEmail(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.UserBase_ConfirmEmail"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                }
            );
        }
        public void AdminAccess(UsersUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.UserRoles_AdminAccess"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@Role", model.Role);
                }
            );
        }
        private Users Mapper(IDataReader reader)
        {
            Users singleItem = new Users();
            int startingIndex = 0; //startingOrdinal

            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.Email = reader.GetSafeString(startingIndex++);
            singleItem.Role = reader.GetSafeString(startingIndex++);
            singleItem.EmailConfirmed = reader.GetSafeBool(startingIndex++);
            singleItem.Lock = reader.GetSafeBool(startingIndex++);
            singleItem.DateCreated = reader.GetSafeDateTime(startingIndex++);
            return singleItem;
        }
        public UsersGrid GetGrid(UsersGridRequest model)
        {
            UsersGrid singleItem = new UsersGrid();

            DataProvider.ExecuteCmd("dbo.UserBase_Grid"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@DisplayLength", model.DisplayLength);
                    paramCollection.AddWithValue("@DisplayStart", model.DisplayStart);
                    paramCollection.AddWithValue("@SortCol", model.SortCol);
                    paramCollection.AddWithValue("@SortDir", model.SortDir);
                    paramCollection.AddWithValue("@Search", model.Search);
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
                            Users u = Mapper(reader);
                            if (singleItem.data == null)
                            {
                                singleItem.data = new List<Users>();
                            }
                            singleItem.data.Add(u);
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
