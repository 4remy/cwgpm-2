﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenCraft : Interactable

{
    public BoxCollider2D physicsCollider;

    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerMemory;

    [Header("Scene Transition Style")]
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public string soundEffectToPlay;

    private void Awake()
    {
        if (fadeInPanel != null)
        {
            //awake happens before anything else
            //if the fadeInPanel doesn't already exist, instantiate it in a place

            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            //destroy the fadein after 1 second
            Destroy(panel, 1);
        }
    }

    protected override void Interact()
    {
                if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
                {
                    playerMemory.initialValue = playerPosition;
            FindObjectOfType<AudioManager>().Play(soundEffectToPlay);
            StartCoroutine(FadeCo());
                }
                else
                    Debug.Log("space not pressed");
                //SceneManager.LoadScene(sceneToLoad);
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

    }
}
