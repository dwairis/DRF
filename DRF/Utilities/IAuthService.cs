using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DRF.Utilities
{
	public interface IAuthService
	{
		UserModel GetCurrentSession { get; }
	}
	public class AuthService : IAuthService
	{
		private readonly IConfiguration configuration;
		private readonly IHttpContextAccessor httpContextAccessor;
		public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
		{
			this.configuration = configuration;
			this.httpContextAccessor = httpContextAccessor;
		}
		public UserModel GetCurrentSession
		{
			get
			{
				UserModel model = new UserModel();
				if (httpContextAccessor.HttpContext.User != null && httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
				{
					IEnumerable<Claim> claims = httpContextAccessor.HttpContext.User.Identities.ToList()[0].Claims;
					foreach (var i in claims)
					{
						switch (i.Type)
						{

							case "Email":
								model.Email = i.Value;
								break;
							case "FullName":
								model.FullName = i.Value;
								break;
							case "UserRole":
								model.Role = i.Value;
								switch (model.Role)
								{
									case "T":
										model.RoleName = "Technical";
										break;
									case "B":
										model.RoleName = "Budget Officer";
										break;
									case "A":
										model.RoleName = "Admin";
										break;
									case "C":
										model.RoleName = "Contractor";
										break;
									case "S":
										model.RoleName = "Administrator";
										break;
								}

								break;
							case "UserId":
								model.UserId = Guid.Parse(i.Value);
								break;
							case "ContractorId":
								model.ContractorId = int.Parse(i.Value);
								break;

							case "Locations":
								model.Locations = i.Value;
								var strLocation = i.Value.Split(";");
								if (strLocation != null && strLocation.Length > 0)
								{
									model.LocationsArr = Array.ConvertAll(strLocation, s => int.Parse(s));

								}
								else
								{
									model.LocationsArr = new int[0];
								}
								break;
						}


					}
				}
				return model;
			}
		}
	}
}
