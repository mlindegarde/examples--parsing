using System;
using System.Collections.Generic;
using System.Text;

namespace Examples.Assessment.Application.Parsing
{
    public interface IParserFactory
    {
        #region Methods
        IParser GetParser(string sampleInput);
        #endregion
    }
}
