using System.Collections.Generic;
using Examples.Assessment.Domain.Models;

namespace Examples.Assessment.Application.Parsing
{
    public interface IParser
    {
        #region Methods
        bool CanParse(string sampleLine);
        List<Customer> Parse(List<string> input);
        #endregion
    }
}
