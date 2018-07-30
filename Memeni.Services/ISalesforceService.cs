using Memeni.Models.Requests;

namespace Memeni.Services
{
    public interface ISalesforceService
    {
        string InsertFB(SalesforceAddRequest model);
        string InsertTWITT(SalesforceAddRequest model);
        string InsertWidget(SalesforceAddRequest model);
        string Register(SalesforceUpdateRequest model);
    }
}