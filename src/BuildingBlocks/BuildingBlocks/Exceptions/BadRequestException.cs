namespace BuildingBlocks.Exceptions;

internal class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }

    public string? Details { get; }

    public BadRequestException(string message, string details) : base(message)
    {
        Details = details;
    }
}

