using UnityEngine;
using TMPro;

public class UIResizer : MonoBehaviour
{
    private RectTransform myRectT;
    private TextMeshProUGUI myText;

    public bool changePosition;
    public float positionPercentX, positionPercentY;
    public bool invertX, invertY;
    public bool changeSize;
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
        if (changePosition)
        {
            if (positionPercentX == -1) positionPercentX = positionPercentY * Screen.height / Screen.width;
            if (positionPercentY == -1) positionPercentY = positionPercentX * Screen.width / Screen.height;
            if (invertX) positionPercentX = 1 - positionPercentX;
            if (invertY) positionPercentY = 1 - positionPercentY;

            myRectT.anchorMin = new Vector2(positionPercentX, positionPercentY);
            myRectT.anchorMax = new Vector2(positionPercentX, positionPercentY);
        }

        if (changeSize)
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
