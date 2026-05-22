# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

All `dotnet` commands must be run from the `./src` directory.

```bash
dotnet restore
dotnet build --no-restore -warnaserror
dotnet format --verify-no-changes             # check code style (CI enforces this)
dotnet format && csharpier format .           # auto-fix code style
dotnet test --no-restore                      # run xunit tests
dotnet pack --configuration Release -p:PackageVersion=<version> --output .
```

## Architecture

This is an **implementation NuGet library** — no abstractions are defined here, only concrete implementations of interfaces from `Pure.Primitives.Abstractions`.

**All public types are sealed records** in the `Pure.Primitives.Date.Operations` namespace:

- **`DayNumber`** — implements `INumber<uint>`; wraps a single `IDate` and exposes its absolute day number via `DateOnly.DayNumber`.
- **`EqualCondition`**, **`NotEqualCondition`** — implement `IBool`; evaluate equality across a sequence of `IDate` values.
- **`BeforeCondition`**, **`AfterCondition`** — implement `IBool`; check strict ascending / descending chronological order.
- **`NotBeforeCondition`**, **`NotAfterCondition`** — implement `IBool`; non-strict variants (allow equal adjacent dates).

All condition types accept `params IEnumerable<IDate>` and throw `ArgumentException` on an empty sequence. `GetHashCode()` and `ToString()` are explicitly overridden to throw `NotSupportedException`.

**Dependency on `Pure.Primitives.Abstractions`:** provides `IDate` (day/month/year as `INumber<ushort>`), `IBool` (`BoolValue`), and `INumber<T>` (`NumberValue`). The local sibling repo lives at `../../Pure.Primitives.Abstractions/` relative to `./src`.

**Multi-targeting:** net7.0, net8.0, net9.0, net10.0. All types must remain AOT-compatible (`IsAotCompatible = true`, `PublishAot = true`).

**Package validation:** `EnablePackageValidation = true` with `PackageValidationBaselineVersion = 0.3.1`. Breaking API changes fail the build.

**Publishing:** triggered by pushing a semver tag (`*.*.*`). The tag becomes the `PackageVersion` passed to `dotnet pack`.

**Tests:** xunit project targeting net10.0, one test class per public type. Coverage threshold: 98% failure / 99% warning.

## Code Style

Enforced via `.editorconfig` and `dotnet format --verify-no-changes` in CI:

- No `var` — always use explicit types in all contexts.
- File-scoped namespaces (`namespace Foo;` not `namespace Foo { }`).
- Expression bodies only on properties, indexers, accessors, and lambdas — not on methods or constructors.
- Always use braces, even for single-line bodies.
- Use framework type names (`Int32` → `int` is the default, but `csharp_prefer_simple_default_expression` applies).
- No `this.` qualification.
- Max line length: 90 characters.

## Commit Messages

Do not mention Claude or AI assistance in commit messages.
