using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShrineTrader : MonoBehaviour {

    public Text uiText;

    public Text erraShrines;
    public Text voziumShrines;
    public Text goldShrines;
    public Text stardustShrines;

    public string introduction;
    public string denied;
    public string accepted;
    public string accepted2;

    private int godReward = 1000;
    private int basicReward = 100;

    private int pressedCount=0;
    private bool noitems = true;
    
    private bool opened = false;

    private void OnGUI() {
        if (!opened) {
            updateInterface();
            opened = true;
        }
    }

    public void updateInterface() {
        pressedCount = 0;
        erraShrines.text = Player.Instance.ErraShrines.ToString();
        voziumShrines.text = Player.Instance.VoziumShrines.ToString();
        goldShrines.text = Player.Instance.GoldShrines.ToString();
        stardustShrines.text = Player.Instance.StardustShrines.ToString();
        int total = (Player.Instance.ErraShrines + Player.Instance.VoziumShrines + Player.Instance.GoldShrines + Player.Instance.StardustShrines);
        if (total == 0) {
            noitems = true;
        }
        else {
            noitems = false;
        }
        uiText.text = introduction;
    }

    public void pressedButton() {
        Debug.Log("pressed chat");
        pressedCount += 1;
        if (pressedCount == 0) {
            uiText.text = introduction;
        }
        else if(pressedCount == 1 && noitems) {
            uiText.text = Player.Instance.playerName + ": " + denied;
            pressedCount = -1;
        }
        else if(pressedCount == 1 && !noitems) {
            uiText.text = Player.Instance.playerName + ": " + accepted;
        }
        //Exchange shrines
        else if (pressedCount == 2 && !noitems) {
            uiText.text = accepted2;
            getReward();
            updateInterface();
            pressedCount = -1;
        }
    }

    private void removeShrinesFromPlayer() {
        Player.Instance.ErraShrines = 0;
        Player.Instance.VoziumShrines = 0;
        Player.Instance.GoldShrines = 0;
        Player.Instance.StardustShrines = 0;
    }



    private void getReward() {
        int experience = 0;
        int coins = 0;
        experience += (Player.Instance.ErraShrines * godReward);
        experience += (Player.Instance.VoziumShrines * godReward);
        experience += (Player.Instance.GoldShrines * basicReward);
        experience += (Player.Instance.StardustShrines * basicReward);
        coins += (experience * 10);
        PopupLog.Instance.showLog("+" + coins + " coins");
        Player.Instance.addExperience(experience);
        Player.Instance.Money += coins;
        removeShrinesFromPlayer();
    }
}
