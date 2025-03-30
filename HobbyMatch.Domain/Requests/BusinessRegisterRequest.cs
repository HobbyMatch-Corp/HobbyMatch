namespace HobbyMatch.Domain.Requests;

public record BusinessRegisterRequest(string Email, string Password, string TaxId, string UserName);