using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Examples.Assessment.Domain.Models;

namespace Examples.Assessment.Application.Parsing
{
    public class HashParser : IParser
    {
        #region Member Variables
        private readonly Regex _pattern =
            new Regex(
                @"^(?<company>[^#]+)#(?<established>[^#]+)#(?<name>[^#]+)#(?<phone>[^#]+)$",
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
                                CompanyName = m.Groups["company"].Value.Trim(),
                                YearsInBusiness = GetYearsInBusiness(m.Groups["established"].Value.Trim()),
                                ContactName = m.Groups["name"].Value.Trim(),
                                ContactPhone = m.Groups["phone"].Value.Trim()
                            })
                    .ToList();
        }
        #endregion

        #region Utility Methods
        private int GetYearsInBusiness(string input)
        {
            return DateTime.UtcNow.Year-Int32.Parse(input);
        }
        #endregion
    }
}
