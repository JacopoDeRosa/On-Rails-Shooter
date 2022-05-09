using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    

    public void LoadLevel(int index)
    {
        StartCoroutine(LoadLevelRoutine(index));
    }

    private IEnumerator LoadLevelRoutine(int index)
    {
        _loadingScreen.SetActive(true);

        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(index);
    }
}
