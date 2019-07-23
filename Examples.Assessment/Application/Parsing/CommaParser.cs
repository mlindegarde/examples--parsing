using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Examples.Assessment.Domain.Models;

namespace Examples.Assessment.Application.Parsing
{
    public class CommaParser : IParser
    {
        #region Member Variables
        private readonly Regex _pattern =
            new Regex(
                @"^(?<company>[^,]+),(?<name>[^,]+),(?<phone>[^,]+),(?<years>[^,]+),(?<email>[^,]+)$",
                RegexOptions.Compiled);
        #endregion

        #region IParser Implementation
        public bool CanParse(string sampleLine)
        {
            return _pattern.IsMatch(sampleLine);
        }

        public List<Customer> Parse(List<string> input)
        {
            return
                input
                    .Select(l => _pattern.Match(l))
                    .Select(
                        m =>
                            new Customer
                            {
                                CompanyName = m.Groups["company"].Value,
                                YearsInBusiness = GetYearsInBusiness(m.Groups["years"].Value),
                                ContactName = m.Groups["name"].Value,
                                ContactEmail = m.Groups["email"].Value,
                                ContactPhone = m.Groups["phone"].Value
                            })
                    .ToList();

        }
        #endregion

        #region Utility Methods
        private int GetYearsInBusiness(string input)
        {
            return Int32.Parse(input);
        }
        #endregion
    }
}
