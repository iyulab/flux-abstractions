namespace Flux.Abstractions;

/// <summary>
/// Thrown by an <see cref="ITextCompletionService"/> when the model stopped because it reached the output token limit,
/// so the text it produced is cut off.
/// </summary>
/// <remarks>
/// A completion that ran out of output budget looks like any other string to the caller. An implementation that can
/// observe the provider's completion reason (for example <c>finish_reason = "length"</c>) throws this instead of
/// returning the truncated text when the caller set <see cref="TextCompletionOptions.ThrowOnTruncation"/>, so a stage
/// that stores or replaces content with the result (summaries, rewrites, extractions) can keep its input rather than
/// adopt a partial answer. Without the option, and in implementations that cannot observe the reason, the text is
/// returned as before. Not sealed: a library that already names this condition in its own contract can derive
/// from it, so one <c>catch</c> covers both.
/// </remarks>
public class TextCompletionTruncatedException : Exception
{
    private const string DefaultMessage = "The model stopped at the output token limit; the response is truncated.";

    /// <summary>Creates the exception with the default message.</summary>
    public TextCompletionTruncatedException() : base(DefaultMessage) { }

    /// <summary>Creates the exception with a message.</summary>
    public TextCompletionTruncatedException(string? message) : base(message ?? DefaultMessage) { }

    /// <summary>Creates the exception with a message and an inner exception.</summary>
    public TextCompletionTruncatedException(string? message, Exception? innerException)
        : base(message ?? DefaultMessage, innerException) { }

    /// <summary>Creates the exception for a call that asked for <paramref name="maxTokens"/> output tokens.</summary>
    public TextCompletionTruncatedException(int maxTokens)
        : base($"The model stopped at the output token limit ({maxTokens} tokens); the response is truncated.")
    {
        MaxTokens = maxTokens;
    }

    /// <summary>
    /// Creates the exception for a call that asked for <paramref name="maxTokens"/> output tokens, wrapping the error that
    /// reported it (for example when one layer translates another's truncation into its own type).
    /// </summary>
    public TextCompletionTruncatedException(int maxTokens, Exception? innerException)
        : base($"The model stopped at the output token limit ({maxTokens} tokens); the response is truncated.", innerException)
    {
        MaxTokens = maxTokens;
    }

    /// <summary>The output token limit the call ran into, when known.</summary>
    public int? MaxTokens { get; }
}
