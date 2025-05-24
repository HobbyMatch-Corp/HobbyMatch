using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.Validation
{
	public interface IValidationService
	{
		public IEnumerable<string> PasswordStrength(string pw);
	}
}
