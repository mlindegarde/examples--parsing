using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Examples.Assessment.Application.Parsing;
using Examples.Assessment.Application.Services;
using Examples.Assessment.Domain.Models;

namespace Examples.Assessment.Persistence
{
    public class InMemoryDataStore : IDataStore
    {
        #region Member Variables
        private readonly IFileManager _fileManager;
        private readonly IParserFactory _parserFactory;
        private readonly Dictionary<string,DataTable> _data;

        private readonly Dictionary<SortMethod, string> _sortMap =
            new Dictionary<SortMethod, string>
            {
                {SortMethod.Undefined, "CompanyName"},
                {SortMethod.CompanyName, "CompanyName"},
                {SortMethod.YearsInBusiness, "YearsInBusiness"},
                {SortMethod.YearsInBusinessThenCompanyName, "YearsInBusiness, CompanyName"}
            };
        #endregion

        #region Constructor
        public InMemoryDataStore(IFileManager fileManager, IParserFactory parserFactory)
        {
            _fileManager = fileManager;
            _parserFactory = parserFactory;

            _data = new Dictionary<string, DataTable>();
        }
        #endregion

        #region Methods
        public async Task<List<Customer>> GetDataAsync(string uploadsPath, SortMethod sortMethod = SortMethod.Undefined)
        {
            if(!_data.ContainsKey(uploadsPath))
                _data.Add(uploadsPath, await LoadDataAsync(uploadsPath));

            return InternalGetData().ToList();

            IEnumerable<Customer> InternalGetData()
            {
                DataView dv = 
                    new DataView(_data[uploadsPath])
                    {
                        Sort = _sortMap[sortMethod]
                    };

                foreach (DataRowView row in dv)
                {
                    yield return
                        new Customer
                        {
                            CompanyName = row["CompanyName"].ToString(),
                            YearsInBusiness = (int)row["YearsInBusiness"],
                            ContactPhone = row["ContactPhone"].ToString(),
                            ContactEmail = row["ContactEmail"].ToString(),
                            ContactName = row["ContactName"].ToString()
                        };
                }
            }
        }
        #endregion

        #region Utility Methods
        private DataTable GenerateDataTable()
        {
            DataTable dt = new DataTable("customers");

            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("YearsInBusiness", typeof(int));
            dt.Columns.Add("ContactName", typeof(string));
            dt.Columns.Add("ContactEmail", typeof(string));
            dt.Columns.Add("ContactPhone", typeof(string));

            return dt;
        }

        private async Task<DataTable> LoadDataAsync(string uploadsPath)
        {
            List<FileInfo> files = _fileManager.GetUploadedFiles(uploadsPath);
            DataTable dt = GenerateDataTable();

            List<Task<List<Customer>>> tasks =
                files
                    .Select(
                        async f =>
                        {
                            List<string> lines = await _fileManager.ReadFileAsync(f);
                            IParser parser = _parserFactory.GetParser(lines[0]);

                            return parser.Parse(lines);
                        })
                    .ToList();

            (await Task.WhenAll(tasks))
                .SelectMany(x => x)
                .ToList()
                .ForEach(c => dt.Rows.Add(c.CompanyName, c.YearsInBusiness, c.ContactName, c.ContactEmail, c.ContactPhone));

            return dt;
        }
        #endregion
    }
}
