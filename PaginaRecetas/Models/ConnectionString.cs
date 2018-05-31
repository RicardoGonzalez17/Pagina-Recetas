using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Web;

namespace PaginaRecetas.Models
{
    public static class ConnectionString
    {
        private static readonly bool isTISSERVER = true;
        private static string BuildConnectionString(string svr, string usr, string psw, string dbs, string mod)
        {
            var connString = @"data source=" + svr + ";initial catalog=" + dbs + ";persist security info=True;user id=" + usr + ";password=" + psw + ";MultipleActiveResultSets=True;App=EntityFramework";
            var esb = new EntityConnectionStringBuilder
            {
                Metadata = "res://*/" + mod + ".csdl|res://*/" + mod + ".ssdl|res://*/" + mod + ".msl",
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = connString
            };
            return esb.ToString();
        }

        public static string Value {
            get
            {
                var vals =
                    new {
                        Server = isTISSERVER ? @"TISSERVER\TIS_SQL_SERVER" : @"(LocalDb)\MSSQLLocalDB",
                        User = isTISSERVER ? @"POINT_PI_DEV" : @"", // Agregar tu usuario local
                        Contrasenia = isTISSERVER ? @"pointinventoryDev" : @"", // Agregar la contraseña del usuario local
                        Database = @"BD_PaginaRecetas",
                        Modelo = @"Models.BDPaginaRecetasModelo"
                    };

                return BuildConnectionString(vals.Server, vals.User, vals.Contrasenia, vals.Database, vals.Modelo);

            }
        }
    }
}