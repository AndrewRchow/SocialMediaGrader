using Memeni.Data.Providers;
using Memeni.Models.Requests;
using Memeni.Services.com.salesforce.na59;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services
{// An Enterprise WSDL was added as a service reference, needs to be regenerated to make any changes.
    public class SalesforceService : BaseService, ISalesforceService
    {
        private IConfigService _configService;

        public SalesforceService(IDataProvider dataProvider, IConfigService configService) : base(dataProvider)
        {
            _configService = configService;
        }

        public string InsertFB(SalesforceAddRequest model)
        {
            //LOGIN TO SALESFORCE
            //Prevent using TLS 1.0 which is outdated
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string userName = _configService.getConfigValusAsString("SalesforceUserName");
            string password = _configService.getConfigValusAsString("SalesforcePassword");
            SforceService SfdcBinding = null;
            LoginResult CurrentLoginResult = null;
            SfdcBinding = new SforceService();
            try
            {
                CurrentLoginResult = SfdcBinding.login(userName, password);
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                SfdcBinding = null;
                throw (e);
            }
            catch (Exception e)
            {
                SfdcBinding = null;
                throw (e);
            }
            SfdcBinding.Url = CurrentLoginResult.serverUrl;
            SfdcBinding.SessionHeaderValue = new SessionHeader();
            SfdcBinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;

            //Check if email already exists
            QueryResult queryResult = null;
            String SOQL = "select Id from Lead where Email = '" + model.Email + "'";
            queryResult = SfdcBinding.query(SOQL);
            //If email exists, update with facebook url
            if (queryResult.size > 0)
            {
                //UPDATE -- to (spunkydrewster002's) salesforce Leads
                Lead lead = (Lead)queryResult.records[0];
                string Id = lead.Id;
                Lead updateLead = new Lead();
                updateLead.Id = Id;
                updateLead.Email = model.Email;
                updateLead.Facebook_Page__c = model.Website;
                SaveResult[] saveResults = SfdcBinding.update(new sObject[] { updateLead });
                string result = "";
                if (saveResults[0].success)
                {
                    result = "The update of Lead ID " + saveResults[0].id + " was succesful";
                    return result;
                }
                else
                {
                    result = "There was an error updating the Lead. The error returned was " + saveResults[0].errors[0].message;
                    return result;
                }
            }
            //If email doesn't exist, insert facebook url
            else
            {
                //POST - to (spunkydrewster002's) salesforce Leads
                Lead sfdcLead = new Lead();
                sfdcLead.Email = model.Email;
                sfdcLead.Facebook_Page__c = model.Website;
                sfdcLead.Campaign_Source__c = model.AdSource;
                sfdcLead.Campaign_Medium__c = model.AdMedium;
                sfdcLead.Campaign_Name__c = model.AdName;
                sfdcLead.Campaign_Term__c = model.AdTerm;
                sfdcLead.Campaign_Content__c = model.AdContent;
                sfdcLead.Campaign_ID__c = model.AdId;

                //Fields required for lead but wont be submitted on homepage, so placeholders are used.
                string firstName = "Social";
                string lastName = "Media";
                string companyName = "Grader User";
                sfdcLead.FirstName = firstName;
                sfdcLead.LastName = lastName;
                sfdcLead.Company = companyName;
                SaveResult[] saveResults = SfdcBinding.create(new sObject[] { sfdcLead });
                if (saveResults[0].success)
                {
                    string resultId = "";
                    resultId = saveResults[0].id;
                    return resultId;
                }
                else
                {
                    string result = "";
                    result = saveResults[0].errors[0].message;
                    return result;
                }
            }
        }

        public string InsertTWITT(SalesforceAddRequest model)
        {
            //LOGIN TO SALESFORCE
            //Prevent using TLS 1.0 which is outdated
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string userName = _configService.getConfigValusAsString("SalesforceUserName");
            string password = _configService.getConfigValusAsString("SalesforcePassword");
            SforceService SfdcBinding = null;
            LoginResult CurrentLoginResult = null;
            SfdcBinding = new SforceService();
            try
            {
                CurrentLoginResult = SfdcBinding.login(userName, password);
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                SfdcBinding = null;
                throw (e);
            }
            catch (Exception e)
            {
                SfdcBinding = null;
                throw (e);
            }
            SfdcBinding.Url = CurrentLoginResult.serverUrl;
            SfdcBinding.SessionHeaderValue = new SessionHeader();
            SfdcBinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;

            //Check if email already exists
            QueryResult queryResult = null;
            String SOQL = "select Id from Lead where Email = '" + model.Email + "'";
            queryResult = SfdcBinding.query(SOQL);
            //If email exists, update with twitter url
            if (queryResult.size > 0)
            {
                //UPDATE -- to (spunkydrewster002's) salesforce Leads
                Lead lead = (Lead)queryResult.records[0];
                string Id = lead.Id;
                Lead updateLead = new Lead();
                updateLead.Id = Id;
                updateLead.Email = model.Email;
                updateLead.Twitter_Page__c = model.Website;
                SaveResult[] saveResults = SfdcBinding.update(new sObject[] { updateLead });
                string result = "";
                if (saveResults[0].success)
                {
                    result = "The update of Lead ID " + saveResults[0].id + " was succesful";
                    return result;
                }
                else
                {
                    result = "There was an error updating the Lead. The error returned was " + saveResults[0].errors[0].message;
                    return result;
                }
            }
            //If email doesn't exist, insert twitter url
            else
            {
                //POST - to (spunkydrewster002's) salesforce Leads
                Lead sfdcLead = new Lead();
                sfdcLead.Email = model.Email;
                sfdcLead.Twitter_Page__c = model.Website;
                sfdcLead.Campaign_Source__c = model.AdSource;
                sfdcLead.Campaign_Medium__c = model.AdMedium;
                sfdcLead.Campaign_Name__c = model.AdName;
                sfdcLead.Campaign_Term__c = model.AdTerm;
                sfdcLead.Campaign_Content__c = model.AdContent;
                sfdcLead.Campaign_ID__c = model.AdId;
                //Fields required for lead but wont be submitted on homepage, so placeholders are used.
                string firstName = "Social";
                string lastName = "Media";
                string companyName = "Grader User";
                sfdcLead.FirstName = firstName;
                sfdcLead.LastName = lastName;
                sfdcLead.Company = companyName;
                SaveResult[] saveResults = SfdcBinding.create(new sObject[] { sfdcLead });
                if (saveResults[0].success)
                {
                    string resultId = "";
                    resultId = saveResults[0].id;
                    return resultId;
                }
                else
                {
                    string result = "";
                    result = saveResults[0].errors[0].message;
                    return result;
                }
            }
        }

        public string InsertWidget(SalesforceAddRequest model)
        {
            //LOGIN TO SALESFORCE
            //Prevent using TLS 1.0 which is outdated
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string userName = _configService.getConfigValusAsString("SalesforceUsername");
            string password = _configService.getConfigValusAsString("SalesforcePassword");
            SforceService SfdcBinding = null;
            LoginResult CurrentLoginResult = null;
            SfdcBinding = new SforceService();
            try
            {
                CurrentLoginResult = SfdcBinding.login(userName, password);
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                SfdcBinding = null;
                throw (e);
            }
            catch (Exception e)
            {
                SfdcBinding = null;
                throw (e);
            }
            SfdcBinding.Url = CurrentLoginResult.serverUrl;
            SfdcBinding.SessionHeaderValue = new SessionHeader();
            SfdcBinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;

            //POST - to (spunkydrewster002's) salesforce Leads
            Lead sfdcLead = new Lead();
            sfdcLead.Website = model.Website;
            sfdcLead.Campaign_Source__c = model.AdSource;
            sfdcLead.Campaign_Medium__c = model.AdMedium;
            sfdcLead.Campaign_Name__c = model.AdName;
            sfdcLead.Campaign_Term__c = model.AdTerm;
            sfdcLead.Campaign_Content__c = model.AdContent;
            sfdcLead.Campaign_ID__c = model.AdId;
            //Fields required for lead but wont be submitted on homepage, so placeholders are used.
            string firstName = "Unknown";
            string lastName = "User";
            string companyName = "WidgetUser";
            sfdcLead.FirstName = firstName;
            sfdcLead.LastName = lastName;
            sfdcLead.Company = companyName;
            SaveResult[] saveResults = SfdcBinding.create(new sObject[] { sfdcLead });
            if (saveResults[0].success)
            {
                string resultId = "";
                resultId = saveResults[0].id;
                return resultId;
            }
            else
            {
                string result = "";
                result = saveResults[0].errors[0].message;
                return result;
            }
        }

        public string Register(SalesforceUpdateRequest model)
        {
            //LOGIN TO SALESFORCE
            //Prevent using TLS 1.0 which is outdated
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string userName = _configService.getConfigValusAsString("SalesforceUserName");
            string password = _configService.getConfigValusAsString("SalesforcePassword");
            SforceService SfdcBinding = null;
            LoginResult CurrentLoginResult = null;
            SfdcBinding = new SforceService();
            try
            {
                CurrentLoginResult = SfdcBinding.login(userName, password);
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {  // This is likley to be caused by bad username or password
                SfdcBinding = null;
                throw (e);
            }
            catch (Exception e)
            {  // This is something else, probably comminication
                SfdcBinding = null;
                throw (e);
            }
            //Change the binding to the new endpoint
            SfdcBinding.Url = CurrentLoginResult.serverUrl;
            //Create a new session header object and set the session id to that returned by the login
            SfdcBinding.SessionHeaderValue = new SessionHeader();
            SfdcBinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;

            //UPDATE -- to (spunkydrewster002's) salesforce Leads
            QueryResult queryResult = null;
            string emailToUpdate = model.Email;
            String SOQL = "select Id from Lead where Email = '" + emailToUpdate + "'";
            queryResult = SfdcBinding.query(SOQL);

            //If email exists, update with first/last name
            if (queryResult.size > 0)
            {
                //UPDATE -- to (spunkydrewster002's) salesforce Leads
                Lead lead = (Lead)queryResult.records[0];
                string Id = lead.Id;
                Lead updateLead = new Lead();
                updateLead.Id = Id;
                updateLead.Email = model.Email;
                updateLead.FirstName = model.FirstName;
                updateLead.LastName = model.LastName;
                SaveResult[] saveResults = SfdcBinding.update(new sObject[] { updateLead });
                string result = "";
                if (saveResults[0].success)
                {
                    result = "The update of Lead ID " + saveResults[0].id + " was succesful";
                    return result;
                }
                else
                {
                    result = "There was an error updating the Lead. The error returned was " + saveResults[0].errors[0].message;
                    return result;
                }
            }
            //If email doesn't exist, insert email with first/last name
            else
            {
                //POST - to (spunkydrewster002's) salesforce Leads
                Lead sfdcLead = new Lead();
                sfdcLead.Email = model.Email;
                //Fields required for lead but wont be submitted on homepage, so placeholders are used.
                sfdcLead.FirstName = model.FirstName;
                sfdcLead.LastName = model.LastName;
                string companyName = "Grader Registered User";
                sfdcLead.Company = companyName;
                SaveResult[] saveResults = SfdcBinding.create(new sObject[] { sfdcLead });
                if (saveResults[0].success)
                {
                    string resultId = "";
                    resultId = saveResults[0].id;
                    return resultId;
                }
                else
                {
                    string result = "";
                    result = saveResults[0].errors[0].message;
                    return result;
                }
            }
        }
    }

}