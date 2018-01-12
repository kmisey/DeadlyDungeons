using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

    public Text framerate;
    public Text sound;
    public Text music;

    private void Start() {
        framerate.text = "framerate: " + ZPlayerPrefs.GetInt("framerate");
        sound.text = "Sound On";
        music.text = "Music On";
    }

	public void toggleFramerate() {
        int temp = ZPlayerPrefs.GetInt("framerate");
        if (temp == 30) {
            ZPlayerPrefs.SetInt("framerate", 45);
            Application.targetFrameRate = 45;
            framerate.text = "framerate: 45";
        }
        else if (temp == 45) {
            ZPlayerPrefs.SetInt("framerate", 60);
            Application.targetFrameRate = 60;
            framerate.text = "framerate: 60";
        }
        else if (temp == 60) {
            ZPlayerPrefs.SetInt("framerate", 30);
            Application.targetFrameRate = 30;
            framerate.text = "framerate: 30";
        }
    }

    public void toggleSound() {
        Player p = Player.Instance;
        if (p.SoundOn) {
            p.SoundOn = false;
            sound.text = "Sound Off";
        }
        else {
            p.SoundOn = true;
            sound.text = "Sound On";
        }
    }

    public void toggleMusic() {
        Player p = Player.Instance;
        if (p.MusicOn) {
            p.MusicOn = false;
            music.text = "Music Off";
        }
        else {
            p.MusicOn = true;
            music.text = "Music On";
        }
    }
}
