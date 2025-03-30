namespace HobbyMatch.Domain.Exceptions.AuthExceptions;

public class RefreshTokenException(string message) : Exception(message);