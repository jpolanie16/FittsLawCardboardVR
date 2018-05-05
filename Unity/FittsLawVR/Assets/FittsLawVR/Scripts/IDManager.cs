using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDManager : MonoBehaviour
{
    public static IDManager idManagerController;

    private string participantID;
    private string participantName;

    public Text participantIDText;
    public Text participantNameText;

    void Awake ()
    {
        if (idManagerController == null)
        {
            DontDestroyOnLoad(gameObject);
            idManagerController = this;
        }

        else if (idManagerController != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetParticipantID()
    {
        if (participantIDText.text == "")
            participantID = "0000";
        else
        participantID = participantIDText.text;
    }

    public void SetParticipantName()
    {
        if (participantNameText.text == "")
            participantName = "Debug";
        else
        participantName = participantNameText.text;
    }

    public string GetParticipantID()
    {
        return participantID;
    }

    public string GetParticipantName()
    {
        return participantName;
    }
}
