using HollowKnight.Rando3Stats.Stats;
using HollowKnight.Rando3Stats.UI;
using Modding;
using SereCore;
using System;
using UnityEngine;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats
{
    public class RandoStats : Mod
    {
        public static RandoStats? Instance { get; private set; }

        private const string END_GAME_COMPLETION = "End_Game_Completion";
        private const float lengthOfPressToSkip = 1.5f;

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
                holdToSkipLock = false;
                GameObject canvas = GuiManager.Instance.GetCanvasForScene("StatsCanvas");

                Log("Calculating statistics");

                IRandomizerStatistic totalStats = new TotalChecksSeen("Total");
                Layout totalChecksStat = GetStackedLabeledText(canvas, totalStats.GetHeader(), totalStats.GetDisplay());

                Layout statGridGroup = new DynamicGridLayout(15, 10, 2, HorizontalAlignment.Center);

                foreach (ChecksSeenByPoolGroup poolStat in ChecksSeenByPoolGroup.GetAllPoolGroups())
                {
                    if (poolStat.IsEnabled)
                    {
                        statGridGroup.Children.Add(GetStackedLabeledText(canvas, poolStat.GetHeader(), poolStat.GetDisplay()));
                    }
                }
                IRandomizerStatistic shops = new GeoShopChecksSeen("Geo Shops");
                statGridGroup.Children.Add(GetStackedLabeledText(canvas, shops.GetHeader(), shops.GetDisplay()));

                Layout statGrouping = new VerticalStackLayout(10f);
                statGrouping.Children.Add(new CenteredText(canvas, "Random Checks Found", GuiManager.Instance.TrajanBold, 25));
                statGrouping.Children.Add(totalChecksStat);
                statGrouping.Children.Add(statGridGroup);

                Log("Starting layout step.");

                statGrouping.DoLayout(new Vector2(10, 20));

                CenteredRect r = new(canvas, Color.white, new(40, 40), "ProgressRect");
                r.DoMeasure();
                r.DoArrange(new(960, 1060, 0, 0));
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
                RectTransform tx = GameObject.Find("ProgressRect").GetComponent<RectTransform>();
                if (held)
                {
                    if (pressStartTime <= float.Epsilon)
                    {
                        pressStartTime = Time.time;
                    }
                    else if (Time.time > pressStartTime + lengthOfPressToSkip)
                    {
                        // we've elapsed the designated time while held; we can now skip the cutscene.
                        // we should now further block the hold-to-skip behavior and animation until the next
                        // time we load into this scene.
                        holdToSkipLock = true;
                        GameManager.instance.SkipCutscene();
                        // fade our stuff out too
                        GameObject.Find("StatsCanvas").AddComponent<CanvasGroupLinearFade>().duration = 0.5f;
                    }
                    float progressPercentage = (Time.time - pressStartTime) / lengthOfPressToSkip;
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

        private Layout GetStackedLabeledText(GameObject canvas, string header, string text)
        {
            Layout statStack = new VerticalStackLayout(5f, HorizontalAlignment.Center);
            statStack.Children.Add(new CenteredText(canvas, header, GuiManager.Instance.TrajanBold, 18, "Stat_" + header));
            statStack.Children.Add(new CenteredText(canvas, text, GuiManager.Instance.TrajanNormal, 15, "StatValue_" + header));
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
