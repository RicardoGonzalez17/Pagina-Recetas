using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaginaRecetas.Startup))]
namespace PaginaRecetas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
