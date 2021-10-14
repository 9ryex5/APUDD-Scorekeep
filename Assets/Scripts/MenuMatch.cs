using UnityEngine;
using TMPro;

public class MenuMatch : MonoBehaviour
{
    public TextMeshProUGUI textTitle;
    public TextMeshProUGUI textStart;

    private void OnEnable()
    {
        Language();
    }

    private void Language()
    {
        textTitle.text = ManagerLanguages.ML.Translate("Match");
        textStart.text = ManagerLanguages.ML.Translate("Start");
    }
}
