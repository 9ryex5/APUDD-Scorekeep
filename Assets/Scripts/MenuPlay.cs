using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuPlay : MonoBehaviour
{
    public TextMeshProUGUI textTitle;
    public TextMeshProUGUI textStart;

    public TextMeshProUGUI textDay;
    public TextMeshProUGUI textHour;
    public TextMeshProUGUI textTeamA, textTeamB;
    public TextMeshProUGUI textX;
    private Match match;

    private void OnEnable()
    {
        textDay.text = string.Empty;
        textHour.text = string.Empty;
        textTeamA.text = string.Empty;
        textTeamB.text = string.Empty;
        textX.text = string.Empty;
        match = new Match();
        Language();
    }

    public void ButtonImportMatch()
    {
        Ultiorganizer ult = new Ultiorganizer();
        match = ult.ImportMatch();

        textDay.text = match.date.Year + "/" + match.date.Month + "/" + match.date.Day;
        textHour.text = match.date.Hour.ToString("00") + ":" + match.date.Minute.ToString("00");
        textTeamA.text = match.teamA.myName;
        textTeamB.text = match.teamB.myName;
        textX.text = "X";
    }

    public void ButtonStartMatch()
    {
        if (match.date.Year == 1)
        {
            ManagerUI.MUI.Warning(ManagerLanguages.ML.Translate("ImportMatch"));
            return;
        }

        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playing);
        ManagerPlaying.MP.StartMatch(match);
    }

    private void Language()
    {
        textTitle.text = ManagerLanguages.ML.Translate("SettingMatch");
        textStart.text = ManagerLanguages.ML.Translate("Start");
    }
}
