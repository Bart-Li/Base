using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Newegg.EC.Core.DataAccess;

namespace Newegg.EC.DataAccess.Extension
{
    public class MySqlDatabase : Database
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlDatabase"/> class with a connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MySqlDatabase(string connectionString)
            : base(connectionString, MySqlClientFactory.Instance)
        {
        }

        /// <summary>
        /// Do load data set.
        /// </summary>
        /// <param name="command">Db command.</param>
        /// <param name="dataSet">Data set.</param>
        /// <param name="tableNames">Table names.</param>
        protected override void DoLoadDataSet(IDbCommand command, DataSet dataSet, string[] tableNames)
        {
            if (tableNames == null)
            {
                throw new ArgumentNullException("tableNames");
            }

            using (DbDataAdapter adapter = new MySqlDataAdapter())
            {
                ((IDbDataAdapter)adapter).SelectCommand = command;

                try
                {
                    string systemCreatedTableNameRoot = "Table";
                    for (int i = 0; i < tableNames.Length; i++)
                    {
                        string systemCreatedTableName = (i == 0)
                            ? systemCreatedTableNameRoot
                            : systemCreatedTableNameRoot + i;

                        adapter.TableMappings.Add(systemCreatedTableName, tableNames[i]);
                    }

                    adapter.Fill(dataSet);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
