namespace Business.Dtos;

public record GetUserResponse(
    Guid Id,
    string Email,
    string Name,
    string Surname,
    int Age
);