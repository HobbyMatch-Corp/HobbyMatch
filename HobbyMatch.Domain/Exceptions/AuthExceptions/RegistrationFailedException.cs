namespace HobbyMatch.Model.Exceptions.AuthExceptions;

public class RegistrationFailedException(IEnumerable<string> errors) :
    Exception($"Registration failed with following errors: {String.Join(Environment.NewLine, errors)}");