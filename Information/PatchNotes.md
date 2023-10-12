<p align="center">
    <a href="#"><img src="https://raw.githubusercontent.com/Deaadman/ItemRarities/release/Images/TitleCardPatchNotes.png"></a>

---

Welcome to the patch notes for this modification. This document provides a detailed insight into the history of every update made to this project. These patch notes keep you informed about the latest additions, bug fixes and enhancements which each release. Along with current information, it also brings you insights as to upcoming possible ideas.

So please note that the upcoming ideas provided within these patch notes isn't final and is subject to change and should not be interpreted as a guarantee of implementation -and production may be halted at any time due to reasons such as life, and loss of interest.

| Versions: |
| - |
| [vX.X.X](#vxxx) |
| [v1.1.2](#v112) |
| [v1.1.1](#v111) |
| [v1.1.0 - The Accessibility Update](#v110---the-accessibility-update) |
| [v1.0.0 - Initial Launch](#v100---initial-launch) |

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
- Inventory
	- Add the ability to filter by rarity. From lowest `Common` to highest `Mythic`.'
	- Add a small icon in each Inventory Grid item to show the rarity without clicking on the item.
- Visual Enhancements
	- Introduce different visual effects or glows for different rarity levels.
		- For example, a pulsating glow for mythic items - or the label displaying on the screen once found.
	- When an item is hovered, display a visual for the rarity type before inspecting or picking up.
		- Can either do this through an icon or the label itself. Suggested by **RossBondReturns**
- Modification Rewrite (v2.0.0)
	- Switch from a duplicated `UILabel` system to a standalone `UILabel` system.
		- This allows for easier implementation amongst other harmony patches.
		- Allows for values of the `UILabel` to be changed in one place, which updates across all.
		- May improve performance and eliminate any incompatibilities.
		- Will help the mod be future-proof to all upcoming updates.
	- Switch to an automatic rarity-based system. Suggested by [**Digitalzombie**](https://github.com/DigitalzombieTLD/)
		- Will be less time consuming, and will support all external modifications automatically. 
		- Could do this through spawn rates of each item in certain regions using the `GearSpawner` mod. 
		- Creating methods which get values for specific items like clothing, food may be a possibility.

---

## v1.1.2:

> Released on the **12th of October 2023**.

### Highlights / Key Changes:
- Merged pull request [**#4**](https://github.com/Deaadman/ItemRarities/pull/4), potentially fixing many issues, specifically issue [**#7**](https://github.com/Deaadman/ItemRarities/issues/7).
- Updated for The Long Dark v2.24.

### Added:
- Added additional `null` checks to the `PanelActionsRadial` harmony patch.
- Added `Logger.LogStarter();` method to the `OnInitializeMelon();` method.

### Changed / Updated:
- Changed the `AssemblyVersion` to `AssemblyInformationalVersion` within the `AssemblyInfo.cs` file, this switches the versioning to utilize [Semantic Versioning](https://semver.org/) patterns.
- Changed the `__instance.m_HUD.GetPanel();` to `InterfaceManager.GetPanel<Panel_HUD>();` as it's a more direct approach.
- Changed the location of the `HashSet` from the `Panel_ActionsRadial` patch to the `ItemRarities.cs` file.
<br></br>
- Updated multiple patches to always instantiate `GearItems` first then immediately check if they are null. Helps reduce issues and unexpected behaviours.
- Updated the `EmbeddedResourceLoad` behaviour to dispose of the resource once done.
- Updated the `GetOriginalColor();` method to handle if no value is retrieved from the dictionary.
- Updated the text within the `Logger.LogStarter();` method.

### Fixed:
- Potentially fixed the issue `'None' key not present within the dictionary` - Issue [**#7**](https://github.com/Deaadman/ItemRarities/issues/7).

### Acknowledgements:
- [**The Illusion**](https://github.com/Arkhorse) - For opening pull request [**#4**](https://github.com/Deaadman/ItemRarities/pull/4) and fixing many issues.

---

## v1.1.1:

> Released on the **27th of September 2023**.

### Highlights / Key Changes:
- Now supports Italian! Thanks to [**LettereUniche**](https://github.com/LettereUniche) for providing translations.
- Translations now default to the text '**No Translation Found**'.
- Updated for The Long Dark v2.23.

### Added:
- Added Italian translations to the `LocalizationData.json`.
- Added a new section within the `LoadLocalizations()` method to parse the localization data.
- Added more `Logger.LogError()`'s throughout the Localization methods.
- Added `#regions` throughout `ItemRarities.cs` for clean up.

### Changed / Updated:
- Changed the `LoadLocalizations()` method to include the `LoadJsonLocalization()` method from `LocalizationUtilities`.
- Changed the `GetRarityLocalization()` method to `GetTranslation()`.
- Changed the translation defaulting to `English`, now it defaults to `No Translation Found`.
<br></br>
- Updated the `TLD.Il2CppAssemblies` NuGet package from 2.17.0 to 2.23.0
- Updated all the `rarityLabel.text`'s to now use the new method name `GetTranslation()`.

### Fixed:
- Fixed this mod not actually using `LocalizationUtilities`.

### Acknowledgements:
- [**LettereUniche**](https://github.com/LettereUniche) - For opening an issue and providing the Italian translations.

---

## v1.1.0 - The Accessibility Update:

> Released on the **26th of September 2023**.

### Highlights / Key Changes:
- Now supports multiple languages! (If you would like a language to be added, please open an [issue](https://github.com/Deaadman/ItemRarities/issues) and provide the translations)
- New accessibility options for colorblind people.
- Now requires `ModSettings` and `Localization Utilities` as dependencies.
- The language defaults to `English` if no translation is found.
- Adjusted the default rarity colors.
- Fixed rarities not showing for items underneath the Cooking UI.

### Added:
- Added a `Localization` folder and `LocalizationData.json` file which contains languages and their translations for each rarity.
	- Current supports `English` and `Turkish`.
	- If no translations is found, the labels default to `English`.
	- *(For more translations, please open issues if you know any other languages)*.
- Added more comments throughout `ItemRaritiesPatches.cs` for clarity.
- Added `LoadLocalizations()` and `GetLocalizedRarity()` methods, these load the localizations and get them.
- Added 2 `Dictionary`'s in order to store the `LocalizationData.json` and `ColorblindMode` information.
- Added `LoadLocalizations()` and `Settings.OnLoad()` method to `OnInitializeMelon()` in order to load the localizations and settings.
- Added `LocalizationUtilities` and `ModSettings` NuGet packages to the project to reference scripts from each mod.
- Created a new `Data` folder, this will store all new `.json` files for now on.
- Added a new `Settings.cs` file underneath the `Miscellaneous` folder to allow players to set options.
- Added 2 accessibility options, Colour Blind Mode and Colour Blind Strength within `Settings.cs`.
- Added colour blind options from `Deuteranope`, `Protanope` and `Tritanope` - with `None` still being an option.
- Added a new `ColorblindMode` enum within `Enums.cs`.
- Added new hex codes for each colorblindness mode.
- Added 2 new methods called `GetOriginalColor()` and `GetColorblindAdjustedColor()` for the transition between colours.
- Added more XML documentation to the newly added methods.

### Changed / Updated:
- Changed the name of the `Utilities` folder to `Miscellaneous`.
- Changed the `GetEmbeddedResource()` method to `public` from `private` so it's accessible from other classes.
- Changed the name of `TLDVanillaGearRarities.json` to `VanillaRarities.json`.
- Changed the name of `ItemRaritiesPatches.cs` to `Patches.cs`.
- Changed the build solution from `Debug` to `Release`.
<br></br>
- Updated all the `rarityLabel.text = itemRarity.ToString();`'s to `rarityLabel.text = GetLocalizedRarity(itemRarity.ToString(), Localization.s_Language);`'s within `ItemRaritiesPatches.cs`.
- Updated the location of`VanillaRarities.json` and `LocalizationData.json` into the `Data` folder.
- Updated all lines of code referencing the old `Localization` and `Rarities` folders.
- Updated the existing hex codes to fit in better with the new colourblind colours.

### Fixed:
- Fixed `Use compound assignment` warning by supressing it, as it's a false-positive.
- Fixed `Remove unused parameter` warning by supressing it, as if it is deleted it created a bunch of issues.
- Fixed rarities not showing for all the uncooked items within the `Cooking` UI.
- Fixed `Non-constant fields should not be visible` warning within the new `Dictionary`.
- Fixed `'new' expression can be simplified` warning by using the target-typed `new` expression.
- Fixed `Converting null literal or possible null value to non-nullable type` warning by checking that `stream` is null before passing it to `StreamReader`.
- Fixed 2 `Possible null reference` warnings by ensuring `LocalizationData` always has a default value.
- Fixed `'new' expression can be simplified` warning in the `LoadLocalization()` method.
- Fixed `Unnecessary assignment of value to 'results'` warning by removing the blank `string` afterwards.
- Fixed another `Converting null literal or possible null value to non-nullable type` warning by adding a nullable condition.
- Fixed `make field readonly` warning by putting `readonly` in front of the colorblind mode dictionary.

### Removed:
- Removed `using static ItemRarities.Main;` from `ItemRarities.cs` as it was no longer being used.
- Removed `LocalizationManager.cs` as it now requires the `LocalizationUtilities` mod to run.
- Removed some comments and unused lines within `ItemRarites.csproj` for clarity.
- Deleted `Localization` and `Rarities` folder.
- Deleted the `HandleUnrecognizedRarity()` method as the `GetColorblindAdjustedColor()` method essentially replaces it.

### Acknowledgements:
- [**Elderly-Emre**](https://github.com/Elderly-Emre) - For opening an issue about localizations and providing the Turkish translations.

---

## v1.0.0 - Initial Launch:

> Released on the **15th of September 2023**.

### Highlights / Key Changes:
- Supports all Vanilla and DLC items.
- Publicly available to download.
- Displays rarities of items across multiple UI components.

### Added:
- Added the ability to add items through their `GEAR_` name, such as `GEAR_Rifle` and `GEAR_Jeans`.
- Added `#region`'s, `//` and `<summary>` comments throughout the project for easier readability and cleanup.
- Added `Logger.LogError();` logs all throughout the code, which logs if something isn't working correctly.
- Added a `GetColor()` method to reduce redundant code all in the `GetColorForRarity()` method.
- Added template code in `ItemRaritiesPatches.cs` that may be used in the future to streamline how the `UILabel` is duplicated.
- Added `None` rarity for the `GetRarity()` method if a gear item doesn't match a rarity.
- Added all `First Aid`, `Tools`, `Materials`, `Fire Starting` and `Food` items to the `Rarities.json` file.

### Changed / Updated:
- Changed some of the methods from `Update` for improved performance.
- Changed it so items no longer use strings of text, such as `Hunting Rifle` and `HUNTING RIFLE`.
- Changed many explicit type declarations with `var`.
- Changed `ContainsKey` to `TryGetValue` in the `GetRarity()` method, for a more efficient lookup in the dictionary.
- Changed `Enum.Parse()` method to `Enum.TryParse()` for the ability to add error handling.
- Changed `GetColorForRarity()` method to `GetRarityColor()`.
- Changed the rarity to default to hiding if an item doesn't have a rarity within the `.json` file.
- Changed the `.json` file from `GearRarities` to `TLDVanillaGearRarities`. This is for clarity when other `.jsons` are added.
<br></br>
- Updated the `.json` file to now be embedded into the `.dll`.
- Updated `GetEmbeddedResource()` method to use a more concise using statement for readability.
- Updated the `itemRarities` dictionary to read a `.json` file for easier readability and editability.
- Updated code throughout the `ItemRarities.cs` file making it more simple.
- Updated the `OnApplicationStart` method as its deprecated, switched to `OnInitializeMelon`.

### Removed:
- Removed all `[Obsolete]` methods.
- Removed the`Utilities.cs` file.
- Removed the `Settings` folder and `Settings.cs` file.
- Removed all `Logger.Log()` console logs, which were used for debugging.
- Removed redundant `Logger.LogError();` logs in `OnInitializeMelon()` method.
- Removed `Default` and `Invalid` rarity type.

### Fixed:
- Fixed the path to the `.json` file being a fixed path, meaning you could only access it if you had the `.json` file on your computer in that exact location.
- Fixed both the `Panel_Crafting` and `Panel_ActionsRadial` to properly display and hide the rarity label.
- Fixed 9 `Non-constant fields` warnings by encapsulating each `UILabel` variable within each harmony patch and adding `readonly` to the dictionary.
- Fixed 3 `'new' expression can be simplified` warnings throughout the project.
- Fixed `'using' statement can be simplified` warning under the `GetEmbeddedResource()` method.
- Fixed `Use 'switch' expression` warning in the `GetColorForRarity()` method.
- Fixed `Converting null literal or possible null value to non-nullable type` warning by adding a `?` in front of `Stream` - so it is now `Stream?`.
- Fixed `Possible null reference return` warning by replacing `return null;` with `throw  new InvalidOperationException("Resource not found: " + resourceName);` which throws an exception instead.
- Fixed 2 `Null check can be simplified` warnings throughout the project.
- Fixed `Unboxing a possibly null value` warning by checking if `rarityObj` is null before unboxing it.
- Fixed `Uncommon` rarity type not being found.

### Acknowledgements:
- [**Digitalzombie**](https://github.com/DigitalzombieTLD/) - For identifying the absolute fixed `.json` path error.
- [**The Illusion**](https://github.com/Arkhorse) - For their knowledge on `[Obsolete]` methods, and how they should never be used.
- [**Fuar**](https://github.com/Fuar11) - For their observation on the deprecated `OnApplicationStart` method being used.
- **RossBondReturns** - For their feedback on removing `INVALID` rarity, and to hide the `RarityLabel` if no rarity is found for that item.