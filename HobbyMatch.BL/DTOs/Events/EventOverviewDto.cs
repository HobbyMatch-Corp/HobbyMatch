using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Events;

public record EventOverviewDto(
    int Id,
    string Name,
    DateTime StartTime,
    DateTime EndTime,
    LocationNullable Location,
    float Price,
    int MaxUsers,
    ParticipantDto[]? Participants
);