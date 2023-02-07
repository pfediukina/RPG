using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
