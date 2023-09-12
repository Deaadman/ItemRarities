# Roadmap

Welcome to the roadmap for this modification. This document provides a detailed insights as to the possible upcoming additions and features within this modification.

For what has currently been updated, please refer to the [**Changelog**](CHANGELOG.md).

> **Disclaimer**: Please note that the information provided within this roadmap isn't final and is subject to change and should not be interpreted as a guarantee of implementation. This includes any of the information in the upcoming releases, planned but unscheduled and concept ideas sections. Production may be halted due to reasons such as life, and loss of interest.

---

### v1.X.X (Upcoming Releases):

> **Note:** These releases will primarily focus on preparing for the next major milestones or small fixes.

  - Cleanup, organise, and deleted unused files such as `Settings.cs`.

---

### Planned But Unscheduled:

> **Note:** Planned content which doesn't have a set release version or date.

  - Switch from a duplicated `UILabel` to a custom `UILabel`.
	 - Will allow for easier implementation among each harmony patch.
	 - Change parameters in one place, instead of each harmony patch.
	 - May eliminate any incompatibilities.
	 - May introduce an issue of getting the `GearItem`, to change the rarity label.
  - Hide the rarity label if no rarity is found
  - Allows developers to set rarities for items within their modifications?
	 - Through an external `.json` shipped with the mod?
	 - Or just support every modification in a future update. **(most likely option)**

---

### Concept Ideas:

> **Note:** A bundle of ideas, with no guarantee of implementation.

  - Animations?
	  - Once a mythic item is found, possibly emphasise the rarity of the item by animating the label?
	  - Similar to totem of undying from Minecraft?
  - Inventory
	  - Add the ability to filter by rarity. From lowest (common) to highest (mythic).
