# Pure.Primitives.Date.Operations

Immutable, composable date operations for the **Pure** ecosystem — comparison conditions and utilities over `IDate` primitives.

[![.NET build & test](https://github.com/kudima03/Pure.Primitives.Date.Operations/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Primitives.Date.Operations/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.Primitives.Date.Operations/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Primitives.Date.Operations/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.Primitives.Date.Operations)](https://www.nuget.org/packages/Pure.Primitives.Date.Operations)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE.txt)

## Overview

`Pure.Primitives.Date.Operations` provides sealed record types that operate on `IDate` values from the Pure ecosystem. All types implement either `IBool` (returning a boolean result) or `INumber<T>` (returning a numeric result), and are fully AOT-compatible.

## Types

### Comparison Conditions

All comparison types accept a sequence of `IDate` values and implement `IBool`.

| Type | Description |
|------|-------------|
| `EqualCondition` | `true` if all dates in the sequence are identical |
| `NotEqualCondition` | `true` if any two dates in the sequence differ |
| `BeforeCondition` | `true` if dates are in strictly ascending chronological order |
| `AfterCondition` | `true` if dates are in strictly descending chronological order |
| `NotBeforeCondition` | `true` if dates are in non-descending order (ascending or equal) |
| `NotAfterCondition` | `true` if dates are in non-ascending order (descending or equal) |

### Utilities

| Type | Implements | Description |
|------|------------|-------------|
| `DayNumber` | `INumber<uint>` | Converts a single `IDate` to its absolute day number (via `DateOnly.DayNumber`) |

## Design Principles

- **Immutable** — all state is set at construction; `BoolValue` / `NumberValue` are pure computed properties.
- **Composable** — every type implements a Pure abstraction interface and can be used anywhere `IBool` or `INumber<T>` is accepted.
- **AOT-compatible** — no reflection; safe for Native AOT and trimming.

## Dependencies

- [`Pure.Primitives.Abstractions` 4.3.0](https://github.com/kudima03/Pure.Primitives.Abstractions/tree/4.3.0) — base interfaces for the Pure ecosystem (`IDate`, `IBool`, `INumber<T>`, and other immutable primitive abstractions)

## Target Frameworks

- .NET 7
- .NET 8
- .NET 9
- .NET 10

## Installation

```shell
dotnet add package Pure.Primitives.Date.Operations
```

## Usage

```csharp
using Pure.Primitives.Date.Operations;

// Check if a list of dates is in ascending order
IBool condition = new BeforeCondition(startDate, midDate, endDate);
if (condition.BoolValue)
{
    // dates are strictly ascending
}

// Convert a date to its absolute day number
INumber<uint> dayNum = new DayNumber(someDate);
uint day = dayNum.NumberValue;
```
