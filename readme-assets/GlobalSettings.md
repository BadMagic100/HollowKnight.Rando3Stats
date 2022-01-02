# Global Settings

This page describes the available global settings. Global settings are located in the same directory as
your saves in `RandoStats.GlobalSettings.json`. To generate settings for the first time, you'll
have to start and exit the game with the mod installed. When modifying settings, change the settings in
the root of the file (not the ones in StringValues/BoolValues/WhateverValues).

## `CountShopItemsIndividually`

This setting controls how Geo Shops (Sly, Salubra, Iselda, Leg Eater) are counted for location stats.
By default, this setting is true, which counts each item in a shop as a location. When this setting is
false, each shop pool as a whole counts as a single check, for a total of 5 (Sly (Key) is separate from
Sly).

## `StatConfig`

This setting is a list of configurations to choose what stats will be enabled and where they will be displayed.
The easiest way to describe what this should look like is to just look at the default. Each config object
contains several required properties:

| Property | Type | Description | Allowed values | Other notes |
| --- | --- | --- | --- |
| Stat | Text | Which stat we're referring to in this config | `ItemsObtained`, `LocationsChecked`, or `TransitionsFound` | Each stat can be listed more than once | 
| EnabledSubcategories | List | A list of subcategories to display in addition to the total | Varies by stat. Can be empty. |  |
| Position | Text | Where the stat will be placed | `TopLeft`, `BottomLeft`, `TopCenter`, `TopRight`, `BottomRight`, or `None` | When `None` is selected, the stat will still be calculated but not shown on the end screen. This is useful for the CompletionFormatString setting below. You can also just remove the stat entirely from the list but it won't be usable for completion formatting. |
| Order | Number | The order that items in the same position should be sorted | Any integer | Higher numbers will be sorted later

Subcategories vary by stat. Current subcategories are listed below:

| Stat | Available subcategories |
| --- | --- |
| ItemsObtained | `ByPool` |
| LocationsChecked | `ByPool` |
| TransitionsFound | `ByArea` |

## `CompletionFormatString`

This setting controls what gets copied to your clipboard when you Ctrl+C on the end screen. By default,
this setting is `$RACING_EXTENDED$`, which is a shortcut for the format commonly used by racers. It
shows your completion time (H:MM, M:SS, or S) and your completion percentage, including the number of
obtained randomized items. For example, it might create something like this: `1:41 80% (249/310)`.
Most people will be perfectly happy with this format, but full customization is provided here. If you
race, you should use `$RACING_SIMPLE$` or `$RACING_EXTENDED$` for standardization purposes.

### Macros

Macros are pre-defined shortcuts for format strings (i.e. valid values for this setting). You reference a
macro by enclosing its name in `$`, for example `$MACRO_NAME$`. Currently defined macros are listed below.

| Macro name | Shortcut for |
| --- | --- |
| `RACING_SIMPLE` | `{BuiltIn:Time} {BuiltIn:Percent}` |
| `RACING_EXTENDED` | `{BuiltIn:Time} {BuiltIn:Percent} ({TotalItemsObtained:Fraction})` |

### Stat placeholders

Stat placeholders are slots for stats to be filled in. Stats are usually defined with a namespace and a
name. A namespace is the type of stat, for example `TotalChecksObtained`, where a name is a common name
for a statistic such as `Percent`, which can be reused across namespaces. A stat is referenced by enclosing
it in `{}`, for example, `{Namespace:Stat}`. You can see examples of this above in the macros section.

Available namespaces:

| Namespace | Description | Available stats |
| --- | --- | --- |
| `BuiltIn` | Built-in stats that Rando/Hollow Knight provide to you even without this mod installed. | `Time`, `Percent` |
| `TotalLocationsChecked` | Stats related to the number of randomized locations you've checked. | All common stats |
| `TotalItemsObtained` | Stats related to the number of randomized items you've picked up. | All common stats |
| `ItemsObtainedByPoolGroup.<Pool>` | Stats related to the items you've obtained in a given pool. Pool names are the same as the ones shown on the end screen with spaces removed, such as `ItemsObtainedByPoolGroup.SoulTotems`. Stats for a pool will only exist if that pool is randomized. | All common stats |
| `LocationsCheckedByPoolGroup.<Pool>` | Stats related to the locations you've checked in a given pool. Pool names are the same as the ones shown on the end screen with spaces removed, such as `LocationsCheckedByPoolGroup.SoulTotems`. Stats for a pool will only exist if that pool is randomized. | All common stats |
| `TransitionsFoundByArea.<Area>` | Stats related to the transitions you've found in a given area. Area names are the same as the ones shown on the end screen with spaces and punctuation removed, such as `TransitionsFoundByArea.KingdomsEdge`. These stats will only exist in room or area rando. | All common stats |

Most namespaces offer the same set of stats. These common stats are listed here:

| Stat | Description | Example value |
| --- | --- | --- |
| `Percent` | The percentage value of the namespace only. | `80%` |
| `Obtained` | The quantity of stuff in this namespace you've actually gotten, i.e. the numerator. | `249` |
| `Total` | The quantity of stuff in this namespace that's been randomized, i.e. the denominator. | `310` |
| `Fraction` | The fraction of stuff in this namespace you've gotten. | `249/310` |
| `Full` | The full stat displayed on the end screen. | `80% (249/310)` |