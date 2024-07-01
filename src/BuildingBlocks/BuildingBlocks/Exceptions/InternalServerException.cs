namespace BuildingBlocks.Exceptions;

internal class InternalServerException : Exception
{
    public InternalServerException(string message) : base(message) { }

    public string? Details { get; }

    public InternalServerException(string message, string details) : base(message)
    {
        Details = details;
    }
}
