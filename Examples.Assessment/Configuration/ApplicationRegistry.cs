using Examples.Assessment.Application.Parsing;
using Examples.Assessment.Persistence;
using Lamar;

namespace Examples.Assessment.Configuration
{
    public class ApplicationRegistry : ServiceRegistry
    {
        #region Constructor
        public ApplicationRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.TheCallingAssembly();
                    scanner.AssembliesAndExecutablesFromApplicationBaseDirectory(a => a.FullName.StartsWith("Examples."));
                    scanner.WithDefaultConventions();
                    scanner.AddAllTypesOf<IParser>();
                });

            For<IDataStore>().Use<InMemoryDataStore>().Singleton();
        }
        #endregion
    }
}
