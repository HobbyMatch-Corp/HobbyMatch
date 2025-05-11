namespace HobbyMatch.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; }
    public int UserId { get; set; }
    public Organizer OrganizerId { get; set; }
}
