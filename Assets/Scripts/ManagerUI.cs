using UnityEngine;
using TMPro;

public class ManagerUI : MonoBehaviour
{
    public static ManagerUI MUI;

    public GameObject main;
    public GameObject menuPlay;
    public GameObject menuMatches;
    public GameObject playing;
    public GameObject playingInfo;
    public GameObject playingOptions;
    public GameObject playingUndo;
    public GameObject playingTimeout;
    public GameObject playingSpiritTimeout;
    public GameObject playingEventPoint;
    public GameObject playingEvent;
    public GameObject matchProfile;
    public GameObject warning;
    public TextMeshProUGUI textWarning;

    private GameObject currentLayout;

    private void Awake()
    {
        MUI = this;
    }

    private void Start()
    {
        currentLayout = main;
    }

    public void OpenLayout(GameObject go)
    {
        currentLayout.SetActive(false);
        currentLayout = go;
        go.SetActive(true);
    }

    public void Warning(string _message)
    {
        textWarning.text = _message;
        warning.SetActive(true);
    }
}
