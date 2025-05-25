namespace HobbyMatch.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; }
    public int OrganizerId { get; set; }
    public Organizer Organizer { get; set; }
}
