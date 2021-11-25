using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] Canvas menuCanvas;

    public void OnToggleMenu()
    {
        menuCanvas.enabled = !menuCanvas.enabled;
    }

    public void ClickContinue()
    {
        menuCanvas.enabled = false;
    }

    public void ClickQuit()
    {
        Application.Quit();
    }
}
