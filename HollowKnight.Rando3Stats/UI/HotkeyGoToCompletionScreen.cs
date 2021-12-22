using GlobalEnums;
using System.Collections;
using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    internal class HotkeyGoToCompletionScreen : MonoBehaviour
    {
        private const string MODIFIER_CTRL = "ctrl";
        private const string MODIFIER_SHIFT = "shift";
        private bool GetModifier(string name)
        {
            return Input.GetKey("left " + name) || Input.GetKey("right " + name);
        }

        private void Update()
        {
            if (GameManager.instance.IsGamePaused() && GetModifier(MODIFIER_CTRL) && GetModifier(MODIFIER_SHIFT) && Input.GetKeyDown(KeyCode.C))
            {
                GameManager.instance.StartCoroutine(ForceEndScreen());
                Destroy(gameObject);
            }
        }

        private static IEnumerator ForceEndScreen()
        {
            GameManager.instance.TimePasses();
            UIManager.instance.UIClosePauseMenu();
            GameManager.instance.cameraCtrl.FadeOut(CameraFadeType.LEVEL_TRANSITION);
            yield return new WaitForSecondsRealtime(0.5f);
            GameManager.instance.ChangeToScene("End_Game_Completion", "door1", 0);
            yield return new WaitWhile(() => GameManager.instance.IsInSceneTransition);
            Time.timeScale = 1f;
            GameManager.instance.FadeSceneIn();
            GameManager.instance.isPaused = false;
            // apparently possibly needed to allow the pause menu to set the timescale again, maybe not relevant because we're quitting out?
            TimeController.GenericTimeScale = 1f;

            HeroController.instance?.UnPause();
            MenuButtonList.ClearAllLastSelected();

            // reset audio to normal levels
            GameManager.instance.actorSnapshotUnpaused.TransitionTo(0f);
            GameManager.instance.ui.AudioGoToGameplay(.2f);
        }
    }
}
