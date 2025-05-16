using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.Database.Repositories.Friends
{
	internal interface IFriendRepository
	{
		Task<bool> AddFriendToUserAsync(User user, User friend);
		Task<bool> RemoveFriendFromUserAsync(User user, User friend);
	}
}
