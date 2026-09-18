namespace AuthService.Dtos.Result
{
    public enum ServiceError
    {
        UnAuthorized = 0,
        NotFound,
        Validation,
        Conflict,
        ServerError,
        None

    }
}
