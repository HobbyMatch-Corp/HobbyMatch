
namespace HobbyMatch.Domain.Entities
{
    public class UserFriendship
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }

        public User? User { get; set; }
        public User? Friend { get; set; }
    }
}
