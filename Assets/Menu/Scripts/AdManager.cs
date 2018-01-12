using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;
using UnityEngine.EventSystems;

public class AdManager : MonoBehaviour {

    private EventSystem currentEventSystem;

	public static AdManager Instance { set; get; }

    private void Start() {
        Instance = this;
    }
    private string type;

    public void ShowAd(string type) {
            if (Advertisement.IsReady()) {
                this.type = type;
                Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
                currentEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            }
    }

    public void ShowAd() {
        if (ZPlayerPrefs.GetInt("noAds") == 1) {
            if (Advertisement.IsReady()) {
                Advertisement.Show("video");
                currentEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            }
        }
    }

    private void HandleAdResult(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                if (type.Equals("keyreward")) {
                    Player.Instance.Keys += 1;
                    Debug.Log("give key");
                }
                break;
            case ShowResult.Skipped:
                Debug.Log("skip");
                break;
            case ShowResult.Failed:
                Debug.Log("fal");
                break;
        }
    }
}
