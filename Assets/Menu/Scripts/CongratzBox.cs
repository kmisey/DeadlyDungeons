using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CongratzBox : MonoBehaviour {

    Sound sound;
    public Animator anim;
    public Text goodjob;
    public Text dungeonDescription;
    public Text scoreText;

    public string[] compliments;

    private float starttime;

    public static CongratzBox Instance { get; set; }

    void Awake() {
        Instance = this;
        starttime = Time.time;
        Debug.Log(starttime);
    }

    // Use this for initialization
    void Start () {
	}


    public void close() {
        sound.play(sound.click);
        gameObject.SetActive(false);
    }

    void createCompliment() {
        goodjob.text = compliments[Random.Range(0, compliments.Length)];
    }

    #region Waves Congratz

    public void openCongratzBoxW() {
        sound = Sound.Instance;
        sound.stopMusic();
        gameObject.SetActive(true);
        createCompliment();
        sound.play(sound.reward);
        anim.SetTrigger("open");
    }

    #endregion

    #region Dungeon Congratz
    public void openCongratzBoxD() {
        sound = Sound.Instance;
        sound.stopMusic();
        gameObject.SetActive(true);
        createCompliment();
        setScoreTextD();
        sound.play(sound.reward);
        anim.SetTrigger("open");
    }

    void setScoreTextD() {
        int score = (int)(Time.time - starttime);
        if (ZPlayerPrefs.GetInt("sp_DungeonSize") == 1) {
            dungeonDescription.text = "Small Dungeon Complete!";
            if (ZPlayerPrefs.GetInt("small_hs") < score) {
                ZPlayerPrefs.SetInt("small_hs", (int)score);
                Player.Instance.addExperience(5 * 10 + (score));
            }

            scoreText.text = score + "\n" + ZPlayerPrefs.GetInt("small_hs");
        }
        else if (ZPlayerPrefs.GetInt("sp_DungeonSize") == 2) {
            dungeonDescription.text = "Medium Dungeon Complete!";
            if (ZPlayerPrefs.GetInt("medium_hs") < score) {
                ZPlayerPrefs.SetInt("medium_hs", (int)score);
                Player.Instance.addExperience(10 * 10 + (score));
            }
            scoreText.text = score + "\n" + ZPlayerPrefs.GetInt("med_hs");
        }
        else if (ZPlayerPrefs.GetInt("sp_DungeonSize") == 3) {
            dungeonDescription.text = "Large Dungeon Complete!";
            if (ZPlayerPrefs.GetInt("large_hs") < score) {
                ZPlayerPrefs.SetInt("large_hs", (int)score);
                Player.Instance.addExperience(15 * 10 + (score));
            }
            scoreText.text = score + "\n" + ZPlayerPrefs.GetInt("large_hs");
        }
    }

    #endregion
}
