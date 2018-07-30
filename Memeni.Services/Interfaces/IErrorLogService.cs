using System;
using System.Collections.Generic;
using Memeni.Models.Domain;
using Memeni.Models.Requests;

namespace Memeni.Services.Interfaces
{
    public interface IErrorLogService
    {
        void Delete(int id);
        List<ErrorLog> GetAll();
        ErrorLog Get(int id);
        void Put(ErrorLogUpdateRequest model);
        int Add(ErrorLogAddRequest model);
        void Schedule(ErrorLogAddRequest model);
        ErrorLogGrid GetGrid(ErrorLogGridRequest model);
        void Truncate();
        void MultiDelete(int[] id);
        void LogError(Exception ex);
        List<ErrorLogSeverity> GetSeverity();
        void SeverityCount();
    }
}