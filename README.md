# Rando3Stats
Rando3Stats is a Hollow Knight mod used with Randomizer mod. It provides detailed statistics about your obtained checks on the completion screen after the credits,
and changes that screen's behavior from "press any button" to "hold any button" to prevent accidentally skipping it.

This mod is *tested* to be compatible with the following:
* Randomizer 3.13(288) - this is the current Randomizer version. If you're not using it... why?
* Randomizer ItemSync 1.3.0

These are the latest and greatest dependencies. I strongly recommend you use them if able.

This mod *may work* with the following, but I don't make any guarantees:
* Randomizer v3.12c(884)
* Randomizer v3.12(573)
* Randomizer Multiworld 0.1.1

The older randomizer versions seem to generally be compatible with 3.13. If for some reason you are dead-set on not upgrading to the new version, give it a try and let me
know whether it works so I can update this readme. This mod is not yet tested with Multiworld. Using it with Multiworld (probably) won't crash your game or anything, but you will 
likely run into some unexpected behaviors and/or failed stat calculations.

https://github.com/homothetyhk/HollowKnight.RandomizerMod    
https://github.com/Shadudev/HollowKnight.MultiWorld

## Full Feature List

* Prevents mashing through the completion screen - replaces press to skip with a 1.5 second hold.
* Available Stats
  * Randomized Checks Obtained
    * This stat is the number of randomized locations checked that you've actually obtained. There is a
      stat for total locations overall, as well as one each per randomized pool. Some smaller pools are
      merged under larger categories (i.e. split claw and split cloak are merged into skills, Palace checks
      are merged into the corresponding overworld categories).
    * By default, each item at Geo Shops (Sly, Salubra, Iselda, Leg Eater) counts as a check. You can
      change this behavior by changing the global setting CountShopItemsIndividually to false. In this case,
      each shop pool as a whole counts as a single check, for a total of 5 (Sly (Key) is separate from Sly).
  * ItemSync - Checks obtained locally (coming soon)
    * This stat is the number of randomized locations YOU obtained
  * If you have an idea for a new stat, cutting an issue here or finding me in the HK speedrunning Discord
    will be your best ways to make it happen - if you're code-savvy, feel free to open a PR as well.

## How to install

1. Install a compatible version of Randomizer (as listed above).
2. Download the latest release of `RandoStats.zip`.
3. Unzip and copy `RandoStats.dll` to the Mods folder in your Hollow Knight install location, e.g.
   `...\Steam\steamapps\common\Hollow Knight\hollow_knight_Data\Managed\Mods`

## Known bugs / missing features

* It is occasionally possible to get screen shake on the completion screen depending on user input.
* More stats!
  * Room rando stats - transitions found (total/by area)
  * Multiworld compatibility
  * Stat extensions to integrate with ItemSync and Multiworld
  
## Acknowledgements

* The Hollow Knight Speedrun Discord, for inspiring the idea
* Phenomenol and the HK Modding Discord for helping me get started on my first HK mod and get integrated
  with Rando.
