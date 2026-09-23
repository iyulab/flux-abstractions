namespace Flux.Abstractions;

/// <summary>
/// Options for text completion generation.
/// </summary>
public class TextCompletionOptions
{
    /// <summary>
    /// Temperature for sampling (0.0 = deterministic, higher = more creative).
    /// Default: 0.7
    /// </summary>
    public float Temperature { get; init; } = 0.7f;

    /// <summary>
    /// Maximum number of tokens to generate.
    /// Default: 500
    /// </summary>
    public int MaxTokens { get; init; } = 500;

    /// <summary>
    /// Top-p nucleus sampling threshold (0.0 - 1.0).
    /// </summary>
    public float? TopP { get; init; }

    /// <summary>
    /// Frequency penalty (-2.0 to 2.0).
    /// </summary>
    public float? FrequencyPenalty { get; init; }

    /// <summary>
    /// Presence penalty (-2.0 to 2.0).
    /// </summary>
    public float? PresencePenalty { get; init; }

    /// <summary>
    /// Stop sequences that will terminate generation.
    /// </summary>
    public IReadOnlyList<string>? StopSequences { get; init; }

    /// <summary>
    /// System prompt prepended to the request.
    /// </summary>
    public string? SystemPrompt { get; init; }

    /// <summary>
    /// Response format hint (e.g., "json" for JSON mode).
    /// </summary>
    public string? ResponseFormat { get; init; }

    /// <summary>
    /// JSON Schema (as JSON text) the response must satisfy.
    /// </summary>
    /// <remarks>
    /// An implementation whose provider supports schema-constrained output enforces it (strict structured output);
    /// one that does not may ignore it and fall back to <see cref="ResponseFormat"/>. When both are set, the schema
    /// is the more specific request. Not validated here: an invalid schema is reported by the provider that reads it.
    /// </remarks>
    public string? ResponseSchema { get; init; }

    /// <summary>
    /// When <c>true</c>, an implementation that can observe the completion reason throws
    /// <see cref="TextCompletionTruncatedException"/> if the model stopped at <see cref="MaxTokens"/>, instead of returning
    /// the cut-off text. Default: <c>false</c> — truncated text is returned as before.
    /// </summary>
    /// <remarks>
    /// Opt in where a partial answer would be worse than no answer: a stage that stores or replaces content with the result
    /// (a summary, a rewrite, a hypothetical document to embed) can catch the exception and keep its input. Leave it off for
    /// calls that deliberately ask for very few tokens and read only the start of the answer (a one-word verdict, a short
    /// label), where stopping at the limit is expected. An implementation that cannot observe the completion reason ignores
    /// this option and returns the text; its documentation says so.
    /// </remarks>
    public bool ThrowOnTruncation { get; init; }
}
