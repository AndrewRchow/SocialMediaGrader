using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services.Interfaces
{
    public interface IAnonTrackingService
    {
        AnonUser SelectUserByEmail(string Email);
        List<AnonUserUrls> SelectUrlsByUser(AnonUserChangeRequest model);
        string IncreaseVisitCount(AnonUserChangeRequest model);
        int InsertUser(AnonUserChangeRequest model);
        AnonUserUrls SelectUrlsByIdAndEmail(AnonUserChangeRequest model);
        string IncreaseTimesGraded(AnonUserChangeRequest model);
        int InsertUrl(AnonUserChangeRequest model);
        AnonTrackingGrid GetGrid(AnonTrackingGridRequest model);
        void DeleteMultiple(int[] ids);
    }
}
