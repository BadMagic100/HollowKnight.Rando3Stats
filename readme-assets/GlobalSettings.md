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
| `RACING_EXTENDED` | `{BuiltIn:Time} {TotalItemsObtained:Full}` |

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

Most namespaces offer the same set of stats. These common stats are listed here:

| Stat | Description | Example value |
| --- | --- | --- |
| `Percent` | The percentage value of the namespace only. | `80%` |
| `Obtained` | The quantity of stuff in this namespace you've actually gotten, i.e. the numerator. | `249` |
| `Total` | The quantity of stuff in this namespace that's been randomized, i.e. the denominator. | `310` |
| `Full` | The full stat displayed on the end screen. | `80% (249/310)`