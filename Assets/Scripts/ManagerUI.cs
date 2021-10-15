using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public static ManagerUI MUI;

    public GameObject main;
    public GameObject menuPlay;
    public GameObject menuMatches;
    public GameObject playing;
    public GameObject playingOptions;
    public GameObject playingUndo;
    public GameObject playingTimeout;
    public GameObject playingEventPoint;
    public GameObject playingEvent;
    public GameObject matchProfile;

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
}
