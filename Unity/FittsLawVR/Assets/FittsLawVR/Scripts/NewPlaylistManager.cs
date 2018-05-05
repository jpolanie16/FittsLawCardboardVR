using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class NewPlaylistManager : MonoBehaviour
{
    public static NewPlaylistManager playlistController;
    private List<string> trialsList;
    public GameObject playlistGrid;
    public GameObject playlistGridPrefab;
    public GameObject panelPrefab;
    int trialNumber;

    void Start()
    {
        trialsList = new List<string>();
        playlistController = this;
        trialNumber = 0;
    }

    public void UpdatePlaylistInformation()
    {
        if (ES2.Exists("Trials/" + "trial" + trialNumber + ".txt"))
        {
            string fileName = "Trials/" + "trial" + trialNumber + ".txt";

            if (!(trialsList.Contains(fileName)))
            {
                string trialName = ES2.Load<string>(fileName + "?tag=trialName");
                string samples = ES2.Load<string>(fileName + "?tag=sampleCount");
                string practiceRounds = ES2.Load<string>(fileName + "?tag=practiceCount");
                string minimumScale = ES2.Load<string>(fileName + "?tag=minimumScale");
                string maximumScale = ES2.Load<string>(fileName + "?tag=maximumScale");
                string minimumHorizontalOffset = ES2.Load<string>(fileName + "?tag=minimumHorizontalOffset");
                string maximumHorizontalOffset = ES2.Load<string>(fileName + "?tag=maximumHorizontalOffset");
                string minimumVerticalOffset = ES2.Load<string>(fileName + "?tag=minimumVerticalOffset");
                string maximumVerticalOffset = ES2.Load<string>(fileName + "?tag=maximumVerticalOffset");
                Color startColor = ES2.Load<Color>(fileName + "?tag=startColor");
                Color goalColor = ES2.Load<Color>(fileName + "?tag=goalColor");
                string startColorString = startColor.ToString();
                string goalColorString = goalColor.ToString();

                GameObject newPanel = Instantiate(panelPrefab, transform.position, transform.rotation, playlistGrid.transform);

                newPanel.GetComponent<UpdateTrialInfo>().SetTrialInfo(trialName, samples, practiceRounds, minimumScale,
                    maximumScale, minimumHorizontalOffset, maximumHorizontalOffset, minimumVerticalOffset, maximumVerticalOffset,
                    startColorString, goalColorString);

                trialsList.Add(fileName);
            }

            else
            {
                Debug.Log("Trials/" + "trial" + trialNumber + ".txt" + " Already Displayed");
            }
        }

        else
        {
            Debug.Log("Trials/" + "trial" + trialNumber + ".txt" + " Does Not Exist");
        }

        trialNumber++;

        if (trialNumber <=9)
        {
            UpdatePlaylistInformation();
        }

        else
        {
            trialNumber = 0;
        }
    }

    public void TrialMoved(int formerPosition, int newPosition)
    {

        ES2.Rename(trialsList[formerPosition], "Trials/tempName.txt");
        ES2.Rename(trialsList[newPosition], trialsList[formerPosition]);
        ES2.Rename("Trials/tempName.txt", trialsList[newPosition]);
    }

    public void DeleteTrial(GameObject trialToDelete)
    {
        int index = trialToDelete.transform.GetSiblingIndex();
        string fileLocation = trialsList[index];
        ES2.Delete(fileLocation);
        trialsList.RemoveAt(index);
        Destroy(trialToDelete);
    }
}
