<p align="center">
    <a href="#"><img src="https://raw.githubusercontent.com/Deaadman/ItemRarities/release/Images/TitleCardPatchNotes.png"></a>

---

Welcome to the patch notes for this modification. This document provides a detailed insight into the history of every update made to this project. These patch notes keep you informed about the latest additions, bug fixes and enhancements which each release. Along with current information, it also brings you insights as to upcoming possible ideas.

So please note that the upcoming ideas provided within these patch notes isn't final and is subject to change and should not be interpreted as a guarantee of implementation -and production may be halted at any time due to reasons such as life, and loss of interest.

| Versions: |
| - |
| [vX.X.X](#vxxx) |
| [v1.1.0](#v110) |
| [v1.1.0-rc.1](#v110-rc1) |
| [v1.1.0-rc.0](#v110-rc0) |
| [v1.0.0](#v100) |
| [v1.0.0-rc.1](#v100-rc1) |
| [v1.0.0-rc.0](#v100-rc0) |

---

## vX.X.X:

>**Note:** A bundle of ideas, with no guarantee of implementation.

- Compatibility
	- Support for other modifications which have custom items.
		- Create separate `.json`'s for each external mod and set rarities.
	- Implement a custom API system for easier integration.
- Customization
    - Give players the ability to change the colours of each rarity label using `ModSettings`.
    	- Some sort of display showing the differences in colour?
- Accessibility
	- Use `ModSettings` to allow users to set colourblind options from Deuteranopia, Protanopia and Tritanopia.
- Inventory
	- Add the ability to filter by rarity. From lowest `Common` to highest `Mythic`.'
	- Add a small icon in each Inventory Grid item to show the rarity without clicking on the item.
- Visual Enhancements
	- Introduce different visual effects or glows for different rarity levels.
		- For example, a pulsating glow for mythic items - or the label displaying on the screen once found.
	- When an item is hovered, display a visual for the rarity type before inspecting or picking up.
		- Can either do this through an icon or the label itself. Suggested by **RossBondReturns**
- Modification Optimization
	- Switch from a duplicated `UILabel` system to a standalone `UILabel` system.
		- This allows for easier implementation amongst other harmony patches.
		- Allows for values of the `UILabel` to be changed in one place, which updates across all.
		- May improve performance and eliminate any incompatibilities.
	- Switch to an automatic rarity-based system. Suggested by [**Digitalzombie**](https://github.com/DigitalzombieTLD/)
		- Will be less time consuming, and will support all external modifications automatically. 
		- Could do this through spawn rates of each item in certain regions using the `GearSpawner` mod. 
		- Getting values for each item may also be another possibility.
	- Complete re-write of the mod (v2.0.0)?

---

## v1.1.0:

> **Upcoming Release...**

---

## v1.1.0-rc.1:

> **Upcoming Release...**

### Added
- Added `LoadLocalizations()` and `GetLocalizedRarity()` methods, these load the localizations and get them.
- Added another `Dictionary` in order to store the `LocalizationData.json` information.
- Added `LoadLocalizations()` method to `OnInitializeMelon()` in order to actually load the localizations.
- Added `LocalizationUtilities` and `ModSettings` NuGet packages to the project in order to reference scripts from each mod.
- Created a new `Data` folder, this will store all new `.json` files for now on.

### Changed / Updated
- Renamed the `TLDVanillaGearRarities.json` to `VanillaRarities.json` for clarity.
- When no translation is found for a specific language, it now defaults to English.
- Replaced all `rarityLabel.text = LocalizationManager.Instance.GetTranslation(itemRarity.ToString(), Localization.s_Language);` to `rarityLabel.text = GetLocalizedRarity(itemRarity.ToString(), Localization.s_Language);` within `ItemRaritiesPatches.cs`.
- Moved `VanillaRarities.json` and `LocalizationData.json` into the `Data` folder.
- Updated all lines of code referencing the old `Localization` and `Rarities` folders.
- Renamed `ItemRaritiesPatches.cs` to `Patches.cs`.
- Localization now defaults to `English` if no translation is found.

### Fixed
- Fixed rarities not showing for all the uncooked items within the `Cooking` UI.
- Fixed `Non-constant fields should not be visible` warning within the new `Dictionary`.
- Fixed `'new' expression can be simplified` warning by using the target-typed `new` expression.
- Fixed `Converting null literal or possible null value to non-nullable type` warning by checking that `stream` is null before passing it to `StreamReader`.
- Fixed 2 `Possible null reference` warnings by ensuring `LocalizationData` always has a default value.
- Fixed `'new' expression can be simplified` warning in the `LoadLocalization()` method.
- Fixed `Unnecessary assignment of value to 'results'` warning by removing the blank `string` afterwards.

### Removed / Deprecated
- Removed `LocalizationManager.cs` as it now requires the `LocalizationUtilities` mod to run.
- Removed some comments and unused lines within `ItemRarites.csproj` for clarity.
- Deleted `Localization` and `Rarities` folder.

---

## v1.1.0-rc.0:

> Released on the **17th of September 2023**.

### Added
- Added a `Localization` folder and `LocalizationData.json` file which contains languages and their translations for each rarity.
	- Current supports `English` and `Turkish`.
	- If no translations is found, the labels currently don't show.
		- Might need to investigate this further, to default back to `English`.
	- *(For more translations, please open issues if you know any other languages)*.
- Created a new `LocalizationManager.cs` which contains all the code for changing languages.
- Added more comments throughout `ItemRaritiesPatches.cs` for clarity.
- Added a `Logger.LogError()` in the `LoadLocalizationData()` method which helps the user know if it failed to load localization data.
- Added `<summary>`'s before each method in `LocalizationManager.cs` for clarity.

### Changed
- Replaced all `rarityLabel.text = itemRarity.ToString();` to `rarityLabel.text = LocalizationManager.Instance.GetTranslation(itemRarity.ToString(), Localization.s_Language);`.
- Renamed `Utilities` folder to `Miscellaneous` folder.
- Changed `GetEmbeddedResource()` method to `public` from `private` so it's accessible from other classes.

### Removed / Deprecated
- Removed `using static ItemRarities.Main;` from `ItemRarities.cs` as it was no longer being used.

### Fixed
- Fixed `Non-nullable field` warning by initializing the `LocalizationManager` field directly.
- Fixed `Use compound assignment` warning by supressing it, as it's a false-positive.
- Fixed `Remove unused parameter` warning by supressing it, as if it is deleted it created a bunch of issues.

### Acknowledgements
- [**Elderly-Emre**](https://github.com/Elderly-Emre) - For opening an issue about localizations and providing the Turkish translations.

---

## v1.0.0:

> Released on the **15th of September 2023**.

### Added
- Added `Logger.LogError();` logs all throughout the code, if something isn't working correctly.
- Added a `GetColor()` method to reduce redundant code all in the `GetColorForRarity()` method.
- Added `<summary>`'s before methods to provide information for its use.
- Added code in `ItemRaritiesPatches.cs` that may be used in the future to streamline how the `UILabel` is duplicated.
- Added `None` rarity for the `GetRarity()` method if a gear item doesn't match a rarity.
- Added all `First Aid` items to the `Rarities.json` file.
- Added all `Tool` items to the `Rarities.json` file.
- Added all `Material` items to the `Rarities.json` file.
- Added all `Fire Starting` items to the `Rarities.json` file.
- Added all `Food` items to the `Rarities.json` file.
- Over 350 items have been added so far!

### Changed
- Changed many explicit type declarations with `var`.
- Changed `ContainsKey` to `TryGetValue` in the `GetRarity()` method, for a more efficient lookup in the dictionary.
- Changed `Enum.Parse()` method to `Enum.TryParse()` for the ability to add error handling.
- Simplified code throughout the `ItemRarities.cs` file.
- Changed `GetColorForRarity()` method to `GetRarityColor()`.
- If an item doesn't have a rarity within the `.json` file, it now defaults to hiding the label.
- Renamed the `.json` file from `GearRarities` to `TLDVanillaGearRarities`. This is for clarity when other `.jsons` are added.

### Removed / Deprecated
- Removed redundant `Logger.LogError();` logs in `OnInitializeMelon()` method.
- Removed `Default` rarity type.
- Removed `Invalid` rarity type.

### Fixed
- Fixed `Non-constant fields` warning by setting the dictionary to `public static readonly` from `public static`.
- Fixed 8 more `Non-constant fields` warning by encapsulating each `UILabel` variable within each harmony patch.
- Fixed 3 `'new' expression can be simplified` warnings throughout the project.
- Fixed `'using' statement can be simplified` warning under the `GetEmbeddedResource()` method.
- Fixed `Use 'switch' expression` warning in the `GetColorForRarity()` method.
- Fixed `Converting null literal or possible null value to non-nullable type` warning by adding a `?` in front of `Stream` - so it is now `Stream?`.
- Fixed `Possible null reference return` warning by replacing `return null;` with `throw  new InvalidOperationException("Resource not found: " + resourceName);` which throws an exception instead.
- Fixed 2 `Null check can be simplified` warnings throughout the project.
- Fixed `Unboxing a possibly null value` warning by checking if `rarityObj` is null before unboxing it.
- Fixed `Uncommon` rarity type not being found.

### Acknowledgements
- **RossBondReturns** - For their feedback on removing `INVALID` rarity, and to hide the `RarityLabel` if no rarity is found for that item.

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
- Fixed both the `Panel_Crafting` and `Panel_ActionsRadial` to properly display and hide the rarity label.aaaa
