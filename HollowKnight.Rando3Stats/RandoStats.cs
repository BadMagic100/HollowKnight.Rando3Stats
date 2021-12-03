using HollowKnight.Rando3Stats.Stats;
using HollowKnight.Rando3Stats.UI;
using Modding;
using SereCore;
using System;
using System.Reflection;
using UnityEngine;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats
{
    public class RandoStats : Mod
    {
        public static RandoStats? Instance { get; private set; }

        private const string END_GAME_COMPLETION = "End_Game_Completion";
        private const float LENGTH_OF_PRESS_TO_SKIP = 1.5f;

        private const float VERTICAL_SPACING = 10;
        private const float HORIZONTAL_SPACING = 15;

        private const int FONT_SIZE_H1 = 25;
        private const int FONT_SIZE_H2 = 18;
        private const int FONT_SIZE_H3 = 15;

        private float pressStartTime = 0;
        private bool holdToSkipLock = false;

        public override ModSettings GlobalSettings
        {
            get => Settings;
            set => Settings = value is RandoStatsGlobalSettings gs ? gs : Settings;
        }

        public RandoStatsGlobalSettings Settings { get; private set; } = new RandoStatsGlobalSettings();

        public override string GetVersion()
        {
            string ver = "1.0.0";
            int minAPI = 45;

            bool apiTooLow = Convert.ToInt32(ModHooks.Instance.ModVersion.Split('-')[1]) < minAPI;
            if (apiTooLow)
            {
                ver += " (Update Modding API)";
            }
            return ver;
        }

        public override void Initialize()
        {
            if (Instance != null)
            {
                Instance.LogWarn("Initialized already!");
                return;
            }

            Instance = this;

            Log("RandoStats initializing...");

#if DEBUG
            On.GameManager.BeginSceneTransition += SkipTHK;
#endif
            On.GameCompletionScreen.Start += GameCompletionScreen_Start;
            On.InputHandler.CutsceneInput += InputHandler_CutsceneInput;

            ModHooks.Instance.LanguageGetHook += GetLanguageString;

            Log("RandoStats finished initializing.");
        }

        private string GetLanguageString(string key, string sheetTitle)
        {
            if (key == "PERMA_GAME_OVER_CONTINUE" && sheetTitle == "Credits List")
            {
                return "Hold any button to continue.";
            }
            return Language.Language.GetInternal(key, sheetTitle);
        }

        private void GameCompletionScreen_Start(On.GameCompletionScreen.orig_Start orig, GameCompletionScreen self)
        {
            bool isRando = Rando.Instance.Settings.Randomizer;
            if (isRando)
            {
                // we don't need to see the recent items panel on the end screen, clear it out to make more room for stats!
                Assembly randoAsm = Assembly.GetAssembly(typeof(Rando));
                Type recents = randoAsm.GetType("RandomizerMod.RecentItems");
                MethodInfo hideRecents = recents.GetMethod("Hide", BindingFlags.Public | BindingFlags.Static);
                hideRecents.Invoke(null, null);

                holdToSkipLock = false;
                GameObject canvas = GuiManager.Instance.GetCanvasForScene("StatsCanvas");

                Log("Calculating statistics");

                IRandomizerStatistic totalLocationStat = new TotalLocationsChecked("Total");
                Layout totalLocationStatText = GetStatText(canvas, totalLocationStat);

                IRandomizerStatistic totalItemStat = new TotalItemsObtained("Total");
                Layout totalItemStatText = GetStatText(canvas, totalItemStat);

                Layout locationPoolStatGroup = new DynamicGridLayout(HORIZONTAL_SPACING, VERTICAL_SPACING, 2, HorizontalAlignment.Center);

                foreach (LocationsCheckedByPoolGroup poolStat in LocationsCheckedByPoolGroup.GetAllPoolGroups())
                {
                    if (poolStat.IsEnabled)
                    {
                        locationPoolStatGroup.Children.Add(GetStatText(canvas, poolStat));
                    }
                }
                IRandomizerStatistic geoShopLocationStat = new GeoShopChecksSeen("Geo Shops");
                locationPoolStatGroup.Children.Add(GetStatText(canvas, geoShopLocationStat));

                Layout statGrouping = new VerticalStackLayout(VERTICAL_SPACING);
                statGrouping.Children.Add(new CenteredText(canvas, "Locations Found", GuiManager.Instance.TrajanBold, FONT_SIZE_H1));
                statGrouping.Children.Add(totalLocationStatText);
                statGrouping.Children.Add(locationPoolStatGroup);
                statGrouping.Children.Add(new CenteredText(canvas, "Items Obtained", GuiManager.Instance.TrajanBold, FONT_SIZE_H1));
                statGrouping.Children.Add(totalItemStatText);

                Log("Starting layout step.");

                statGrouping.DoLayout(new Vector2(10, 20));

                CenteredRect r = new(canvas, Color.white, new(40, 40), "ProgressRect");
                new Container(r).DoLayout(new Vector2(960, 1060));
            }
            orig(self);
        }

        private void InputHandler_CutsceneInput(On.InputHandler.orig_CutsceneInput orig, InputHandler self)
        {
            string scene = GameManager.instance.GetSceneNameString();
            if (scene != END_GAME_COMPLETION)
            {
                // if we're in any other cutscene, just do the default behavior.
                orig(self);
            }
            else
            {
                if (holdToSkipLock) return;

                bool held = Input.anyKey || self.gameController.AnyButton.IsPressed;
                RectTransform? tx = GameObject.Find("ProgressRect")?.GetComponent<RectTransform>();
                // if we can't find this, something has gone really badly in the setup, revert to default behavior so we're not softlocked here
                if (tx == null)
                {
                    orig(self);
                    return;
                }

                if (held)
                {
                    if (pressStartTime <= float.Epsilon)
                    {
                        pressStartTime = Time.time;
                    }
                    else if (Time.time > pressStartTime + LENGTH_OF_PRESS_TO_SKIP)
                    {
                        // we've elapsed the designated time while held; we can now skip the cutscene.
                        // we should now further block the hold-to-skip behavior and animation until the next
                        // time we load into this scene.
                        holdToSkipLock = true;
                        GameManager.instance.SkipCutscene();
                        // fade our stuff out too
                        GameObject.Find("StatsCanvas").AddComponent<CanvasGroupLinearFade>().duration = 0.5f;
                    }
                    float progressPercentage = (Time.time - pressStartTime) / LENGTH_OF_PRESS_TO_SKIP;
                    float desiredWidth = GuiManager.ReferenceSize.x * progressPercentage;
                    float scale = desiredWidth / tx.sizeDelta.x;
                    tx.SetScaleX(scale);
                }
                else
                {
                    pressStartTime = 0;
                    tx.SetScaleX(0);
                }
            }
        }

        private Layout GetStatText(GameObject canvas, IRandomizerStatistic stat)
        {
            string header = stat.GetHeader();
            string text = stat.GetDisplay();
            Layout statStack = new VerticalStackLayout(5f, HorizontalAlignment.Center);
            statStack.Children.Add(new CenteredText(canvas, header, GuiManager.Instance.TrajanBold, FONT_SIZE_H2, "Stat_" + header));
            statStack.Children.Add(new CenteredText(canvas, text, GuiManager.Instance.TrajanNormal, FONT_SIZE_H3, "StatValue_" + header));
            return statStack;
        }

        private void SkipTHK(On.GameManager.orig_BeginSceneTransition orig, GameManager self, GameManager.SceneLoadInfo info)
        {
            if (info.SceneName == SceneNames.Room_Final_Boss_Core)
            {
                info.SceneName = "End_Game_Completion";
            }
            orig(self, info);
        }
    }
}
