# Changelog

All notable changes to Flux.Abstractions will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.26.0] - 2026-09-23

### Added
- **`TextCompletionTruncatedException` — an `ITextCompletionService` can say its output was cut off.** `CompleteAsync`
  returns text only, so a completion that stopped at `MaxTokens` was indistinguishable from a finished one and a stage
  that stores or rewrites content with it adopted the partial answer. Implementations that can observe the provider's
  completion reason throw this instead when asked (it carries `MaxTokens` when known); others return text as before. The type is
  not sealed so a library that already names the condition can derive from it. Additive — no signature changes.
- **`TextCompletionOptions.ThrowOnTruncation` — callers opt in to that exception.** Default `false`: truncated text is
  returned exactly as before, so a call that deliberately asks for a handful of tokens (a verdict, a label) is not
  broken. A stage that stores or replaces content with the result sets it and catches the exception. Implementations
  that cannot observe the completion reason ignore it.

## [0.25.0] - 2026-09-15

### Added
- `TextCompletionOptions.ResponseSchema` — the JSON Schema (as JSON text) a response must satisfy. A caller that
  knows the shape it will parse can now pass it through `ITextCompletionService`; `ResponseFormat = "json"` could
  only ask for "some object". Implementations over providers with schema-constrained output enforce it; others may
  ignore it and fall back to `ResponseFormat`. Additive — nothing changes for callers that do not set it.

## [0.24.0] - 2026-08-02

### Changed
- **This package now ships from its own repository and versions independently.** It previously
  built inside FluxIndex and inherited that package's version number. Because FluxIndex also
  consumes the modules that consume this contract, the dependency graph contained a cycle, and
  the practical effect was that a consumer could only ever reference a contract release older
  than the one it was being built against. Pinning them to old releases was what kept the graph
  buildable. Splitting the contract out removes the cycle: consumers can now track the current
  contract, and this package no longer takes a version bump every time FluxIndex ships.

- **No API changes.** The types, namespace, package id, and members are identical to `0.23.0`.
  Upgrading is a version bump with no code change. The version line continues from where it
  was, so `0.24.0` supersedes `0.23.0` as usual.

### Added
- CI asserts the published package declares no dependencies. Every package in the ecosystem
  sits above this one, so a dependency added here would become a version floor for all of
  them — worth failing the build over rather than catching in review.

---

Releases up to and including `0.23.0` were published from the FluxIndex repository; their
notes live in that project's changelog.
