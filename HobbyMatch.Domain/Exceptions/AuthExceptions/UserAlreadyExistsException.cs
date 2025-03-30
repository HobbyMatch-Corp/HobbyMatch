namespace HobbyMatch.Model.Exceptions.AuthExceptions;

public class UserAlreadyExistsException(string email) : Exception($"User with email {email} already exists");