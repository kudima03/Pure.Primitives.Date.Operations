# Changelog

All notable changes to Pure.Primitives.Date.Operations are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [1.0.0] — 2026-06-17

### Fixed

- `AfterCondition`, `BeforeCondition`, `NotAfterCondition`, and `NotBeforeCondition`
  used an inverted comparison operator and could return the opposite of the
  correct result. They now evaluate chronological ordering correctly:
  `AfterCondition` requires each date to be strictly after the previous one,
  `BeforeCondition` requires each date to be strictly before the previous one,
  and `NotAfterCondition` / `NotBeforeCondition` require the non-strict
  (non-increasing / non-decreasing) equivalents.

---

## [0.3.1] — 2025-11-24

- Maintenance release: dependency and build updates.

---

## [0.3.0] — 2025-11-22

### Changed

- The package now multi-targets `net7.0`, `net8.0`, `net9.0`, and `net10.0`
  (previously `net9.0` only).

---

## [0.2.0] — 2025-11-02

### Added

- `DayNumber` — computes the total number of days since `0001-01-01` for an
  `IDate` (backed by `DateOnly.DayNumber`).
- `BoolValue` on `AfterCondition`, `BeforeCondition`, `EqualCondition`,
  `NotAfterCondition`, `NotBeforeCondition`, and `NotEqualCondition`, and
  `NumberValue` on `DayNumber`, are now public properties instead of
  explicit interface implementations, so callers can read them directly
  without casting to `IBool` / `INumber<uint>`.

### Changed

- The package is now declared AOT-compatible (`IsAotCompatible`), enabling
  trimming and Native AOT publishing in consuming applications.

---

## [0.1.0] — 2025-06-16

Initial release.

### Added

- `EqualCondition` — evaluates whether a set of dates are all equal.
- `NotEqualCondition` — evaluates whether a set of dates are not all equal.
- `AfterCondition` — evaluates whether a sequence of dates is in strictly
  ascending (chronologically after) order.
- `BeforeCondition` — evaluates whether a sequence of dates is in strictly
  descending (chronologically before) order.
- `NotAfterCondition` — evaluates whether a sequence of dates is in
  non-increasing order.
- `NotBeforeCondition` — evaluates whether a sequence of dates is in
  non-decreasing order.
