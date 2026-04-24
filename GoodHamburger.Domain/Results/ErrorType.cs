namespace GoodHamburger.Domain.Results
{
    public enum ErrorType
    {
        Validation = 400,
        NotFound = 404,
        Conflicable = 409
    }
}