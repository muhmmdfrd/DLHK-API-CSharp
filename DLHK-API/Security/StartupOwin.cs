using System;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(DLHK_API.Security.StartupOwin))]

namespace DLHK_API.Security
{
	public class StartupOwin
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureOAuth(app);
		}

		public void ConfigureOAuth(IAppBuilder app)
		{
			OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/api/login"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(3560),
				Provider = new CustomAuthProvider()
			};

			// Token Generation
			app.UseOAuthAuthorizationServer(OAuthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}
}
