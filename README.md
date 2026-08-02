# Flux.Abstractions

Shared contracts for the Flux RAG ecosystem. Pure interfaces, **zero dependencies**.

[![NuGet](https://img.shields.io/nuget/v/Flux.Abstractions.svg)](https://www.nuget.org/packages/Flux.Abstractions/)

```bash
dotnet add package Flux.Abstractions
```

## What is in here

| Type | Purpose |
|---|---|
| `IEnrichedChunk` | A document chunk plus the metadata a retrieval layer needs to rank and cite it. |
| `ISourceMetadata` | Where a chunk came from, for traceability and filtering. |
| `ILanguageProfile` | The minimal language-specific segmentation metadata every module agrees on. |
| `ITextCompletionService` | A text-completion port, so a pipeline stage can call a model without binding to a provider. |
| `TextCompletionOptions` | Sampling options for the above. |

`IEnrichedChunk` and `ISourceMetadata` are deliberately a **superset union** of what the
individual modules produce: an implementation returns `null` or the default for fields that do
not apply to it. That keeps one contract instead of one per producer, at the cost of some
properties being empty depending on the source.

`ILanguageProfile` is intentionally minimal. Modules that need more extend it privately rather
than widening the shared contract — a producing module's richer profile stays in that module.

## Why this package has no dependencies

Every package in the ecosystem sits above this one. Anything referenced here becomes a version
floor for all of them, so the absence of dependencies is a constraint the package is designed
around, not an accident of it being small. Contributions that add a `PackageReference` here
should expect that question first.

The same reasoning explains why this is its own repository. A contract package must be able to
sit below its consumers in the dependency graph. While it shipped from inside one of the
packages that consumes it, that was not possible: the consumers could only ever reference a
release older than the one they were part of, and the graph had a cycle in it. Splitting the
contract out removes the cycle and lets consumers track the current contract.

## Versioning

This package continues the version line it published under previously — the package id is
unchanged and the sequence keeps moving forward, so upgrading is a normal version bump with no
migration step. The independent line starts at **0.24.0**.

Being a contract package, it changes rarely, and it now versions on its own cadence instead of
inheriting a release number from a package that ships far more often.

Pre-1.0: breaking changes are possible on minor bumps, and are called out in the release notes.

## License

MIT
