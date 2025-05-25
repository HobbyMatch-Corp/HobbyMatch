using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.Validation
{
	public class ValidationService : IValidationService
	{
		public IEnumerable<string> PasswordStrength(string pw)
		{
			if (string.IsNullOrWhiteSpace(pw))
			{
				yield return "Password is required!";
				yield break;
			}
			if (pw.Length < 8)
				yield return "Password must be at least of length 8";
			if (!Regex.IsMatch(pw, @"[A-Z]"))
				yield return "Password must contain at least one capital letter";
			if (!Regex.IsMatch(pw, @"[a-z]"))
				yield return "Password must contain at least one lowercase letter";
			if (!Regex.IsMatch(pw, @"[0-9]"))
				yield return "Password must contain at least one digit";
		}
	}
}
