using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Services.Cryptography;
using Memeni.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services
{
    public class UserService : BaseService, IUserService
    {
        private IAuthenticationService _authenticationService;
        private ICryptographyService _cryptographyService;
        private const int HASH_ITERATION_COUNT = 1;
        private const int RAND_LENGTH = 15;

        //private Response response;

        public UserService(IAuthenticationService authService, ICryptographyService cryptographyService, IDataProvider dataProvider) : base(dataProvider)
        {
            _authenticationService = authService;
            _cryptographyService = cryptographyService;
        }

        //Login User
        public bool LogIn(UserRequest model)
        {
            bool isSuccessful = false;
            string salt = GetSalt(model.Email);

            if (!String.IsNullOrEmpty(salt))
            {
                string passwordHash = _cryptographyService.Hash(model.Password, salt, HASH_ITERATION_COUNT);
                IUserAuthData response = Get(model.Email, passwordHash);

                if (response != null)
                {
                    //Examples for Claims; use later if needed
                    //Claim tenant = new Claim("Tenant", "AAAA");
                    //Claim fullName = new Claim("FullName", "Memeni Bootcamp");

                    _authenticationService.LogIn(response);
                    isSuccessful = true;
                }
            }
            return isSuccessful;
        }

        //Login from fb,google,linkedin without needing password
        public bool LogInFree(UserFreeRequest model)
        {
            IUserAuthData response = GetFree(model.Email);
            _authenticationService.LogIn(response);
            bool isSuccessful = true;

            return isSuccessful;
        }

        public bool LogInTest(string email, string password)
        {
            bool isSuccessful = false;

            IUserAuthData response = new UserBase { Id = 88, Name = "Greg", Roles = new[] { "User", "Super", "Content Manager" } };

            Claim tenant = new Claim("Tenant", "AAAA");
            Claim fullName = new Claim("FullName", "Sabio Bootcamp");

            //Login Supports multiple claims
            //and multiple roles a good an quick example to set up for 1 to many relationship
            _authenticationService.LogIn(response, new Claim[] { tenant, fullName });

            return isSuccessful;
        }

        //Register User
        //Adds User data into UserBase DB and sets Anon Role to UserRoles DB
        //Adds name and phone info to respective db tables
        public int Create(RegisterUserRequest model)
        {
            int Id = 0;

            string salt = _cryptographyService.GenerateRandomString(RAND_LENGTH);
            string passwordHash = _cryptographyService.Hash(model.Password, salt, HASH_ITERATION_COUNT);

            DataProvider.ExecuteNonQuery(storedProc: "dbo.UserBase_Insert", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Email", model.Email);
                paramCollection.AddWithValue("@PasswordHash", passwordHash);
                paramCollection.AddWithValue("@Salt", salt);
                paramCollection.AddWithValue("@FirstName", model.FirstName);
                paramCollection.AddWithValue("@LastName", model.LastName);
                if (model.CountryCode == null)
                {
                    model.CountryCode = "";
                }
                paramCollection.AddWithValue("@CountryCode", model.CountryCode);
                paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumber);
                if (model.Extension == null)
                {
                    model.Extension = "";
                }
                paramCollection.AddWithValue("@Extension", model.Extension);

                SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                idParameter.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(idParameter);

            }, returnParameters: delegate (SqlParameterCollection param)
            {
                Int32.TryParse(param["@Id"].Value.ToString(), out Id);
            });
            return Id;
        }

        //Register User through Facebook Login - sets role to User
        public int RegisterFacebook(RegisterUserFacebookRequest model)
        {
            int Id = 0;

            DataProvider.ExecuteNonQuery(storedProc: "dbo.UserBaseFacebook_Insert", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Email", model.Email);
                paramCollection.AddWithValue("@EmailConfirmed", model.EmailConfirmed);
                paramCollection.AddWithValue("@Locked", model.Locked);
                paramCollection.AddWithValue("@FbId", model.FbId);
                paramCollection.AddWithValue("@FirstName", model.FirstName);
                paramCollection.AddWithValue("@LastName", model.LastName);
                paramCollection.AddWithValue("@Picture", model.Picture);

                SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                idParameter.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(idParameter);

            }, returnParameters: delegate (SqlParameterCollection param)
            {
                Int32.TryParse(param["@Id"].Value.ToString(), out Id);
            });
            return Id;
        }
        //If email is already registered, just adds facebook Id
        public int AddFacebookId(int UserId, string FbId, string Picture)
        {
            DataProvider.ExecuteNonQuery("dbo.UserBaseFacebook_AddId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", UserId);
                    paramCollection.AddWithValue("@FbId", FbId);
                    paramCollection.AddWithValue("@Picture", Picture);
                });
            int Id = UserId;
            return Id;
        }

        //Register User through Google Login - sets role to User
        public int RegisterGoogle(RegisterUserGoogleRequest model)
        {
            int Id = 0;

            DataProvider.ExecuteNonQuery(storedProc: "dbo.UserBaseGoogle_Insert", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Email", model.Email);
                paramCollection.AddWithValue("@EmailConfirmed", model.EmailConfirmed);
                paramCollection.AddWithValue("@Locked", model.Locked);
                paramCollection.AddWithValue("@GoogleId", model.GoogleId);
                paramCollection.AddWithValue("@FirstName", model.FirstName);
                paramCollection.AddWithValue("@LastName", model.LastName);

                SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                idParameter.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(idParameter);

            }, returnParameters: delegate (SqlParameterCollection param)
            {
                Int32.TryParse(param["@Id"].Value.ToString(), out Id);
            });
            return Id;
        }
        //If email is already registered, just adds google Id
        public int AddGoogleId(int UserId, string GoogleId)
        {
            DataProvider.ExecuteNonQuery("dbo.UserBaseGoogle_AddId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", UserId);
                    paramCollection.AddWithValue("@GoogleId", GoogleId);
                });
            int Id = UserId;
            return Id;
        }

        //Register User through LinkedIn Login - sets role to User
        public int RegisterLinkedIn(RegisterUserLinkedInRequest model)
        {
            int Id = 0;

            DataProvider.ExecuteNonQuery(storedProc: "dbo.UserBaseLinkedIn_Insert", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Email", model.Email);
                paramCollection.AddWithValue("@EmailConfirmed", model.EmailConfirmed);
                paramCollection.AddWithValue("@Locked", model.Locked);
                paramCollection.AddWithValue("@LinkedInId", model.LinkedInId);
                paramCollection.AddWithValue("@FirstName", model.FirstName);
                paramCollection.AddWithValue("@LastName", model.LastName);

                SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                idParameter.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(idParameter);

            }, returnParameters: delegate (SqlParameterCollection param)
            {
                Int32.TryParse(param["@Id"].Value.ToString(), out Id);
            });
            return Id;
        }
        //If email is already registered, just adds LinkedIn Id
        public int AddLinkedInId(int UserId, string LinkedInId)
        {
            DataProvider.ExecuteNonQuery("dbo.UserBaseLinkedIn_AddId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", UserId);
                    paramCollection.AddWithValue("@LinkedInId", LinkedInId);
                });
            int Id = UserId;
            return Id;
        }

        //Set email verification code
        public void CreateCode(int id, string guid)
        {
            DataProvider.ExecuteNonQuery(storedProc: "dbo.EmailVerification_Insert", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@UserId", id);
                paramCollection.AddWithValue("@Code", guid);
            });
        }

        //GETs login User's Info for PW confirmation and setting User Role(s)
        private IUserAuthData Get(string email, string passwordHash)
        {
            UserBase singleItem = new UserBase();
            string matchPassword = "";
            string roleOfUser = "";
            List<string> UserRolesList = new List<string>();

            DataProvider.ExecuteCmd("dbo.UserRoles_SelectByEmail"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Email", email);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   singleItem.Id = reader.GetSafeInt32(1);
                   matchPassword = reader.GetSafeString(2);
                   singleItem.Name = email;
                   roleOfUser = reader.GetSafeString(4);

                   if (roleOfUser == "Admin")
                   {
                       UserRolesList.Add("User");
                       UserRolesList.Add("Admin");
                   }
                   else
                   {
                       UserRolesList.Add(roleOfUser);
                   }

                   singleItem.Roles = UserRolesList.ToArray();

               });
            if (passwordHash == matchPassword)
            {
                return singleItem;
            }
            else
            {
                return null;
            }
        }

        //GETs Login User's info from fb,google,linkedin login
        private IUserAuthData GetFree(string email)
        {
            UserBase singleItem = new UserBase();
            string roleOfUser = "";
            List<string> UserRolesList = new List<string>();

            DataProvider.ExecuteCmd("dbo.UserRoles_SelectByEmail"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Email", email);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   singleItem.Id = reader.GetSafeInt32(1);
                   singleItem.Name = email;
                   roleOfUser = reader.GetSafeString(4);

                   if (roleOfUser == "Admin")
                   {
                       UserRolesList.Add("User");
                       UserRolesList.Add("Admin");
                   }
                   else
                   {
                       UserRolesList.Add(roleOfUser);
                   }
                   singleItem.Roles = UserRolesList.ToArray();
               });
            return singleItem;
        }

        //GETs Salt of the User with User Email
        private string GetSalt(string email)
        {
            string salt = "";
            DataProvider.ExecuteCmd("dbo.UserBase_SelectByEmail"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Email", email);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   salt = reader.GetSafeString(3);
               });
            return salt;
        }
        public int GetByEmail(string email)
        {
            int id = 0;

            this.DataProvider.ExecuteCmd("dbo.UserBase_SelectByEmail"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Email", email);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   id = reader.GetSafeInt32(0);
               });
            return id;
        }
        public string GetGuidByEmail(int Id)
        {
            string guid = "";
            this.DataProvider.ExecuteCmd("dbo.EmailVerification_SelectByID"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", Id);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   guid = reader.GetSafeString(1);
               });
            return guid;
        }

        public string ResetPwEntry(string email)
        {
            string resetCode = "";
            this.DataProvider.ExecuteCmd("dbo.ResetPw_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Email", email);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   resetCode = reader.GetSafeString(0);
               });
            return resetCode;
        }
        public void ResetPwConfirm(ResetPwRequest model)
        {
            string salt = _cryptographyService.GenerateRandomString(RAND_LENGTH);
            string passwordHash = _cryptographyService.Hash(model.Password, salt, HASH_ITERATION_COUNT);

            this.DataProvider.ExecuteNonQuery("dbo.ResetPw_Update"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ResetCode", model.Code);
                   paramCollection.AddWithValue("@Password", passwordHash);
                   paramCollection.AddWithValue("@Salt", salt);
               });
        }
    }
}