namespace HobbyMatch.Model.Exceptions.AuthExceptions;

public class LoginFailedException(string email) : Exception($"Invalid email: {email} or password");