using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public static ManagerUI MUI;

    private void Awake()
    {
        MUI = this;
    }
}
