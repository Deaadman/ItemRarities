# Patch Notes

Welcome to the patch notes for this modification. This document provides a detailed insight into the history of every update made to this project. These patch notes keep you informed about the latest additions, bug fixes and enhancements which each release. Along with current information, it also brings you insights as to upcoming possible ideas.

So please note that the upcoming ideas provided within these patch notes isn't final and is subject to change and should not be interpreted as a guarantee of implementation -and production may be halted at any time due to reasons such as life, and loss of interest.

| Versions: |
| - |
| [v1.X.0](#v1x0) |
| [v1.0.0-rc.2](#v100-rc.2) |
| [v1.0.0-rc.1](#v100-rc.1) |
| [v1.0.0-rc.0](#v100-rc.0) |

---

## v1.X.0:

>**Note:** A bundle of ideas, with no guarantee of implementation.

### Possible Ideas
- Switch from a duplicated `UILabel` to a custom `UILabel`.
	- Will allow for easier implementation among each harmony patch.
	- Change parameters in one place, instead of each harmony patch.
	- May eliminate any incompatibilities.
	- May introduce an issue of getting the `GearItem`, to change the rarity label.
- Hide the rarity label if no rarity is found
- Allows developers to set rarities for items within their modifications?
	- Through an external `.json` shipped with the mod?
	- Or just support every modification in a future update. **(most likely option)**
- Animations?
	 - Once a mythic item is found, possibly emphasise the rarity of the item by animating the label?
	 - Similar to totem of undying from Minecraft?
- Inventory
	 - Add the ability to filter by rarity. From lowest (common) to highest (mythic).

---

## v1.0.0-rc.2:

> **Upcoming Release...**

### Added
- Added `Logger.LogError();` logs all throughout the code, if something isn't working correctly.
- Added a `GetColor()` method to reduce redundant code all in the `GetColorForRarity()` method.
- Added `<summary>`'s before methods to provide information for its use.
- Added code in `ItemRaritiesPatches.cs` that may be used in the future to streamline how the `UILabel` is duplicated.
- Added `None` rarity for the `GetRarity()` method if a gear item doesn't match a rarity.
- Added all `First Aid` items to the `Rarities.json` file.
- Added all `Tool` items to the `Rarities.json` file.
- Added all `Material` items to the `Rarities.json` file.
- Over 200 items have been added so far!

### Changed
- Changed many explicit type declarations with `var`.
- Changed `ContainsKey` to `TryGetValue` in the `GetRarity()` method, for a more efficient lookup in the dictionary.
- Changed `Enum.Parse()` method to `Enum.TryParse()` for the ability to add error handling.
- Simplified code throughout the `ItemRarities.cs` file.
- Changed `GetColorForRarity()` method to `GetRarityColor()`.
- If an item doesn't have a rarity within the `.json` file, it now defaults to hiding the label.

### Removed / Deprecated
- Removed redundant `Logger.LogError();` logs in `OnInitializeMelon()` method.
- Removed `Default` rarity type.
- Removed `Invalid` rarity type.

### Fixed
- Fixed `Non-constant fields` warning by setting the dictionary to `public static readonly` from `public static`.
- Fixed 3 `'new' expression can be simplified` warnings throughout the project.
- Fixed `'using' statement can be simplified` warning under the `GetEmbeddedResource()` method.
- Fixed `Use 'switch' expression` warning in the `GetColorForRarity()` method.
- Fixed `Converting null literal or possible null value to non-nullable type` warning by adding a `?` in front of `Stream` - so it is now `Stream?`.
- Fixed `Possible null reference return` warning by replacing `return null;` with `throw  new InvalidOperationException("Resource not found: " + resourceName);` which throws an exception instead.
- Fixed 2 `Null check can be simplified` warnings throughout the project.
- Fixed `Unboxing a possibly null value` warning by checking if `rarityObj` is null before unboxing it.
- Fixed `Uncommon` rarity type not being found.

### Acknowledgements
- **RossBondReturns** - For their feedback on removing `INVALID` rarity, and to hide the `RarityLabel` if no `Rarity` is found for that item.

---

## v1.0.0-rc.1:

> Released on the **12th of September 2023**.

### Added
- Added `#region`'s and `//` comments within the harmony patches, for easier readability and identification.

### Updated
- Updated `GetEmbeddedResource()` method to use a more concise using statement for readability.

### Changed
- The `.json` file is now embedded into the `.dll`.

### Removed / Deprecated
- Removed all `[Obsolete]` methods.
- `OnApplicationStart` method is deprecated, switched to `OnInitializeMelon`.
- Deleted `Utilities.cs` file.
- Removed another pesky `Logger.Log()` console log.
- Deleted `Settings` and `Settings.cs` folder and file.

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
