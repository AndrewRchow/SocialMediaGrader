using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services.Interfaces
{
    public interface IWidgetService
    {
        string GetFbProfile(string token, string site);
        string GetPicture(string token, string id);
    }
}
