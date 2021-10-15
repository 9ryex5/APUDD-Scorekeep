using UnityEngine;
using TMPro;

public class MenuMatches : MonoBehaviour
{
    public static MenuMatches MM;

    public TextMeshProUGUI textTitle;
    public Transform parentMatches;
    public ItemMatch prefabItemMatch;

    public TextMeshProUGUI textMatchTitle;
    public Transform parentMatchEvents;
    public ItemMatchEvent prefabMatchEvent;

    private void Awake()
    {
        MM = this;
    }

    private void OnEnable()
    {
        for (int i = 0; i < parentMatches.childCount; i++)
            Destroy(parentMatches.GetChild(i).gameObject);

        for (int i = 0; i < SaveData.SD.matches.Count; i++)
        {
            ItemMatch im = Instantiate(prefabItemMatch, parentMatches);
            im.StartThis(SaveData.SD.matches[i]);
        }

        Language();
    }

    public void OpenMatch(Match _m)
    {
        textMatchTitle.text = _m.teamA.myName + " | " + _m.teamB.myName;

        for (int i = 0; i < parentMatchEvents.childCount; i++)
            Destroy(parentMatchEvents.GetChild(i).gameObject);

        for (int i = 0; i < SaveData.SD.matches.Count; i++)
        {
            ItemMatchEvent ime = Instantiate(prefabMatchEvent, parentMatchEvents);
            ime.StartThis(_m.events[i]);
        }

        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.matchProfile);
    }

    private void Language()
    {
        textTitle.text = ManagerLanguages.ML.Translate("Matches");
    }
}
