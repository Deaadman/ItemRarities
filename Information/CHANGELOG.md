# Changelog

Welcome to the changelog for this modification. This document provides a detailed insight into the history of every update made to this project. This changelog keeps you informed about the latest additions, bug fixes and enhancements which each release.

If you want to see upcoming features, please refer to the [**Roadmap**](ROADMAP.md).

| Versions: |
| - |
| [v1.0.0](#v100) |
| [v1.0.0-rc.X](#v100-rc.1x) |
| [v1.0.0-rc.0](#v100-rc.0) |

---

## v1.0.0:

> Released on the **??th of September 2023**.

No patch notes as of currently.

---

## v1.0.0-rc.X:

> Upcoming Release...

### Added
- Added `#region`'s within the harmony patches, for easier readability.

### Updated
- Updated `Rarity.ERROR` to `Rarity.INVALIDRARITY` and then to `Rarity.INVALID`.
- Updated the `itemRarities` dictionary to read a `.json` file for easier readability and editability.

### Changed
- The `.json` file is now embedded into the `.dll`.

### Removed / Deprecated
- Removed all `[Obsolete]` methods.
- `OnApplicationStart` method is deprecated, switched to `OnInitializeMelon`.

### Fixed
- Fixed the path to the `.json` file being an absolute fixed path, meaning you could only access it if you had the `.json` file on your computer in that exact location.

### Acknowledgements
- [**Digitalzombie**](https://github.com/DigitalzombieTLD/) - For identifying the absolute fixed `.json` path error.
- [**The Illusion**](https://github.com/Arkhorse) - For their knowledge on `[Obsolete]` methods, and how they should never be used.
- [**Fuar**](https://github.com/Fuar11) - For their observation on the deprecated `OnApplicationStart` method being used.

---

## v1.0.0-rc.0:

> Released on the **11th of September 2023**.

### Added
- Added the ability to add items through their `GEAR_` name, such as `GEAR_Rifle` and `GEAR_Jeans`.
- Added the first batch of `GEAR_` items to the `Rarities.json` file.

### Updated
- Updated `Rarity.ERROR` to `Rarity.INVALIDRARITY` and then to `Rarity.INVALID`.
- Updated the `itemRarities` dictionary to read a `.json` file for easier readability and editability.

### Changed
- Changed some of the methods from `Update` for improved performance.

### Removed / Deprecated
- Items are no longer found using strings of text, such as `Hunting Rifle` and `HUNTING RIFLE`.
- Removed all `Logger.Log()` console logs, which were used for debugging.
- Commented out unused pieces of code. May be revisited in the future.

### Fixed
- Fixed both the `Panel_Crafting` and `Panel_ActionsRadial` to properly display and hide the rarity label. 
