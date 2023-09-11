# Changelog

Welcome to the changelog for this modification. This document provides a detailed insight into the history of every updates made to this project. This changelog keeps you informed about the latest additions, bug fixes and enhancements which each release.

If you want to see upcoming features, please refer to the [**Roadmap**]().

| Versions: |
| - |
| [v1.0.0](#v100) |
| [v1.0.0-rc1](#v100-rc1) |
| [v1.0.0-rc0](#v100-rc0) |

---

## v1.0.0:

> Released on the **??th of September 2023**.

No patch notes as of currently.

---

## v1.0.0-rc1:

### Added
- Added the ability to add items through their `GEAR_` name, such as `GEAR_Rifle` and `GEAR_Jeans`.

### Updated
- Updated `Rarity.ERROR` to `Rarity.INVALIDRARITY` and then to `Rarity.INVALID`.
- Updated the `itemRarities` dictionary to read a `.json` file for easier readability and editability.

### Changed
- Changed some of the methods from `Update` for improved performance.

### Removed / Deprecated
- Items are no longer found using strings of text, such as `Hunting Rifle` and `HUNTING RIFLE`.

### Fixed
- Fixed both the `Panel_Crafting` and `Panel_ActionsRadial` to properly hide and display the rarity label.

---

## v1.0.0-rc0:

### Added
- Initial commit.
