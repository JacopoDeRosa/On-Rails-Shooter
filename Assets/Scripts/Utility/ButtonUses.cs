using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUses : MonoBehaviour
{
   public void LoadMainScene()
   {
        SceneManager.LoadScene(1);
   }

    public void Quit()
    {
        Application.Quit();
    }
}
