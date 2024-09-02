using SnapObjects.Data;
using SnapObjects.Data.SqlServer;

namespace FicharApi
{
    public class DefaultDataContext : SqlServerDataContext
    {
        public DefaultDataContext(string connectionString)
            : this(new SqlServerDataContextOptions<DefaultDataContext>(connectionString))
        {

        }

        public DefaultDataContext(IDataContextOptions<DefaultDataContext> options)
            : base(options)
        {
            options.TrimSpaces = true;
            options.DelimitIdentifier = false;
        }

        public DefaultDataContext(IDataContextOptions options)
            : base(options)
        {

        }
    }
}
