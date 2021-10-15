using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuPlay : MonoBehaviour
{
    public TextMeshProUGUI textTitle;
    public TextMeshProUGUI textImport;
    public Button buttonStart;
    public TextMeshProUGUI textStart;

    public TextMeshProUGUI textTeamA, textTeamB;
    private Team teamA, teamB;

    private void OnEnable()
    {
        textTeamA.text = string.Empty;
        textTeamB.text = string.Empty;
        buttonStart.interactable = false;
        Language();
    }

    public void ButtonImportMatch()
    {
        Ultiorganizer ult = new Ultiorganizer();
        Team[] teams = ult.ImportTeams();
        teamA = teams[0];
        teamB = teams[1];
        textTeamA.text = teamA.myName;
        textTeamB.text = teamB.myName;

        buttonStart.interactable = true;
    }

    public void ButtonStartMatch()
    {
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playing);
        ManagerPlaying.MP.StartMatch(teamA, teamB);
    }

    private void Language()
    {
        textTitle.text = ManagerLanguages.ML.Translate("Play");
        textImport.text = ManagerLanguages.ML.Translate("Import");
        textStart.text = ManagerLanguages.ML.Translate("Start");
    }
}
