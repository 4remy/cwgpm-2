using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class BranchingDialogController : MonoBehaviour
{
    [SerializeField] private GameObject branchingCanvas;
    [SerializeField] private GameObject dialogPrefab;
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private TextAssetValue dialogValue;
    [SerializeField] private Story myStory;
    [SerializeField] private GameObject dialogHolder;
    [SerializeField] private GameObject choiceholder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnableCanvas()
    {
        branchingCanvas.SetActive(true);
        SetStory();
        RefreshView();
    }

    public void SetStory()
    {
        if(dialogValue.value)
        {
            myStory = new Story(dialogValue.value.text);
        }
        else
        {
            Debug.Log("Something went wrong with Story setup");
        }
    }
    public void RefreshView()
    {
        //change for customisation later
        //hit enter to advance
        while (myStory.canContinue)
        {
            //this is continuing the story for one line of text
            MakeNewDialog(myStory.Continue());

        }
        if (myStory.currentChoices.Count > 0)
        {
            MakeNewChoices();
        }
        else
        {
            branchingCanvas.SetActive(false);
        }
    }

    void MakeNewDialog(string newDialog)
    {
        DialogObject newDialogObject = Instantiate(dialogPrefab, dialogHolder.transform).GetComponent<DialogObject>();
        newDialogObject.Setup(newDialog);
    }

    void MakeNewResponse(string newDialog, int choiceValue)
    {
        ResponseObject newResponseObject = Instantiate(choicePrefab, choiceholder.transform).GetComponent<ResponseObject>();
        newResponseObject.Setup(newDialog, choiceValue);
    }

    void MakeNewChoices()
    {
        for(int i = 0; i < choiceholder.transform.childCount; i++)
        {
            Destroy(choiceholder.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < myStory.currentChoices.Count; i++)
        {
            MakeNewResponse(myStory.currentChoices[i].text, i);
        }
    }
}
