using Core.Manager.UserManager;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DLHK_API.Security
{
	public class CustomAuthProvider : OAuthAuthorizationServerProvider
	{
		public override Task MatchEndpoint(OAuthMatchEndpointContext context)
		{
			if (context.IsTokenEndpoint && context.Request.Method == "OPTIONS")
			{
				context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
				context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization" });
				context.RequestCompleted();

				return Task.FromResult(0);
			}

			return base.MatchEndpoint(context);
		}

		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			await Task.Run(() => context.Validated());
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			bool isDataValid = ValidateUser(context.UserName, context.Password);
			var nameClaim = new NameForClaim();

			if (!isDataValid)
			{
				context.SetError("invalid_grant", "The username or password is incorrect.");
				return;
			}
			
			using (var manager = new UserAdapter())
			{
				var userLogged = manager.Query.Value.TransformUsername(context.UserName);
				nameClaim.Name = userLogged.PersonName;
				nameClaim.Photo = userLogged.Photo;
				nameClaim.RegionName = userLogged.RegionName;
				nameClaim.RoleName = userLogged.RoleName;
				nameClaim.ZoneName = userLogged.ZoneName;
				nameClaim.UserId = userLogged.EmployeeId;
				nameClaim.Shift = userLogged.Shift;
			}

			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			identity.AddClaim(new Claim(ClaimTypes.Role, nameClaim.RoleName));
			identity.AddClaim(new Claim(ClaimTypes.Name, nameClaim.Name));
			identity.AddClaim(new Claim("Zone", nameClaim.RoleName.ToLower().Equals("koor wilayah") || 
				nameClaim.RoleName.ToLower().Equals("admin")
				? "-" : nameClaim.ZoneName));
			identity.AddClaim(new Claim("Region", nameClaim.RoleName.ToLower().Equals("admin") ? "-" : nameClaim.RegionName));
			identity.AddClaim(new Claim("UserId", nameClaim.UserId.ToString()));
			identity.AddClaim(new Claim("Photo", nameClaim.Photo == null ? "" : Convert.ToBase64String(nameClaim.Photo)));
			identity.AddClaim(new Claim("Shift", nameClaim.Shift));

			await Task.Run(() => context.Validated(identity));
		}

		private struct NameForClaim
		{
			public string Name { get; set; }
			public string RoleName { get; set; }
			public string ZoneName { get; set; }
			public string RegionName { get; set; }
			public long? UserId { get; set; }
			public byte[] Photo { get; set; }
			public string Shift { get; set; }
		}

		private bool ValidateUser(string username, string password)
		{
			using (var manager = new UserAdapter())
			{
				var query = manager.Query.Value.Get();
				var result = query.FirstOrDefault(x => x.Username.Equals(username) && x.Password.Equals(password));

				return result != null;
			}
		}
	}
}