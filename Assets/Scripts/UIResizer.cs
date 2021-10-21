using UnityEngine;
using TMPro;

public class UIResizer : MonoBehaviour
{

    private RectTransform myRectT;
    private TextMeshProUGUI myText;

    public float sizePercentWidth, sizePercentHeight;
    public bool square;
    public float textPercentHeight;

    private void Awake()
    {
        myRectT = GetComponent<RectTransform>();
        myText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (sizePercentWidth != 0 || sizePercentHeight != 0)
        {
            float width = sizePercentWidth * Screen.width;
            float height = sizePercentHeight * Screen.height;

            if (square) myRectT.sizeDelta = new Vector2(Mathf.Min(width, height), Mathf.Min(width, height));
            else myRectT.sizeDelta = new Vector2(width, height);
        }

        if (myText != null && textPercentHeight != 0)
        {
            myText.fontSize = textPercentHeight * Screen.height;
        }
    }
}
