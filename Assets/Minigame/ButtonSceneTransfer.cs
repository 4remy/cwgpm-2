using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneTransfer : MonoBehaviour

{
    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerMemory;

    [Header("Scene Transition Style")]
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public string soundEffectToPlay;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            runTransfer();
        }
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
            AudioManager.Instance.Play(soundEffectToPlay);

        }
        yield return new WaitForSeconds(fadeWait);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

    }

    public void runTransfer()
    {

        playerMemory.initialValue = playerPosition;
        StartCoroutine(FadeCo());
    }
}
