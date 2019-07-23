using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.Assessment.Domain.Models;

namespace Examples.Assessment.Persistence
{
    #region Enumerables
    public enum SortMethod
    {
        Undefined = 0,
        CompanyName = 1,
        YearsInBusiness = 2,
        YearsInBusinessThenCompanyName = 3
    }
    #endregion

    public interface IDataStore
    {
        #region Methods
        Task<List<Customer>> GetDataAsync(string uploadPath, SortMethod sortMethod = SortMethod.Undefined);
        #endregion
    }
}
