namespace HobbyMatch.Domain.Exceptions.AuthExceptions;

public class UserAlreadyExistsException(string email) : Exception($"User with email {email} already exists");