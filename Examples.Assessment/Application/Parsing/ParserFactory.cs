using System.Collections.Generic;
using System.Linq;

namespace Examples.Assessment.Application.Parsing
{
    public class ParserFactory : IParserFactory
    {
        #region Member Variables
        private readonly List<IParser> _parsers;
        #endregion

        #region Constructor
        public ParserFactory(IEnumerable<IParser> parsers)
        {
            _parsers = parsers.ToList();
        }
        #endregion

        #region Methods
        public IParser GetParser(string sampleInput)
        {
            return _parsers.SingleOrDefault(p => p.CanParse(sampleInput));
        }
        #endregion
    }
}
