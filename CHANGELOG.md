# Changelog

All notable changes to Flux.Abstractions will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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
