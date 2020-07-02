using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_ADO_Net.Models;

namespace WebAPI_ADO_Net.Repositories.Interfaces
{
    public interface IApplicationService
    {
        Task<ApplicationData> GetApplicationAsync(string appName, string appPassword);
    }
}
