using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Services.Interfaces;

namespace Memeni.Services
{
    public class PeopleService : BaseService, IPeopleService
    {
        public PeopleService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        //POST / Create
        public int Insert(PersonAddRequest model)
        {
            int perId = 0;

            DataProvider.ExecuteNonQuery("dbo.People_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("FirstName", model.FirstName);
                    paramCollection.AddWithValue("MiddleName", model.MiddleName);
                    paramCollection.AddWithValue("LastName", model.LastName);
                    paramCollection.AddWithValue("ModifiedBy", model.ModifiedBy);

                    SqlParameter idParameter = new SqlParameter("@UserId", System.Data.SqlDbType.Int);
                    idParameter.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(idParameter);
                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    Int32.TryParse(param["@UserId"].Value.ToString(), out perId);
                }
            );
            return perId;
        }

        //GET By Id / Read
        public Person Get(int id)
        {
            Person singleItem = null;

            DataProvider.ExecuteCmd("dbo.People_SelectById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", id);
                }
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    singleItem = Mapper(reader);
                });
            return singleItem;
        }

        //GET ALL / Read
        public List<Person> Get()
        {
            List<Person> list = new List<Person>();

            DataProvider.ExecuteCmd("dbo.People_SelectAll"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    Person singlePerson = Mapper(reader);

                    list.Add(singlePerson);
                }
            );
            return list;
        }

        private Person Mapper(IDataReader reader)
        {
            Person singlePerson = new Person();
            int startingIndex = 0;

            singlePerson.UserId = reader.GetSafeInt32(startingIndex++);
            singlePerson.FirstName = reader.GetSafeString(startingIndex++);
            singlePerson.MiddleName = reader.GetSafeString(startingIndex++);
            singlePerson.LastName = reader.GetSafeString(startingIndex++);
            singlePerson.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singlePerson.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singlePerson.ModifiedBy = reader.GetSafeString(startingIndex++);
            return singlePerson;
        }

        //PUT / Update
        public void Update(PersonUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.People_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", model.UserId);
                    paramCollection.AddWithValue("@FirstName", model.FirstName);
                    paramCollection.AddWithValue("@MiddleName", model.MiddleName);
                    paramCollection.AddWithValue("@LastName", model.LastName);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                }
            );
        }

        //DELETE 
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.People_Delete"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", id);
                }
            );
        }
    }
}
