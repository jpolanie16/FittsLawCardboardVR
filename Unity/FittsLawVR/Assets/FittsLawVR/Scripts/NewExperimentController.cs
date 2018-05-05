using System.Collections;
using UnityEngine;
using LlockhamIndustries.Decals;
using System.Diagnostics;
using System;
using System.Net;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;

public class NewExperimentController : MonoBehaviour
{
    public GameObject messageBoard;
    public Transform playerCamera;

    public RayPositioner startTargetPositioner;
    public RayPositioner goalTargetPositioner;

    private Stopwatch sampleStopwatch;
    private Stopwatch cursorStopwatch = new Stopwatch();

    private SpreadsheetManager spreadsheetManager;

    private GameObject startTarget;
    private GameObject goalTarget;

    private int currentTrial;
    private int currentSample;
    private int currentPracticeRound;
    private bool isPracticeRound;

    private string participantID;
    private string participantName;
    private string trialName;
    private int samples;
    private int practiceRounds;
    private int minimumScale;
    private int maximumScale;
    private float minimumHorizontalOffset;
    private float maximumHorizontalOffset;
    private float minimumVerticalOffset;
    private float maximumVerticalOffset;
    private Color startTargetColor;
    private Color goalTargetColor;
    private string startColorString;
    private string goalColorString;
    private string sampleElapsedTime;
    private string cursorPositionString;
    private bool sampleInProcess;
    private int trialNumber;

    private List<string> experimentData;
    

    void Start()
    {
        currentTrial = 0;
        currentSample = 0;
        currentPracticeRound = 0;
        trialNumber = 0;
        cursorPositionString = "";
        isPracticeRound = false;
        experimentData = new List<string>();

        LoadNextTrial();
        SetTrialParameters();

        participantID = IDManager.idManagerController.GetParticipantID();
        participantName = IDManager.idManagerController.GetParticipantName();

        spreadsheetManager = GetComponent<SpreadsheetManager>();
    }

    public void LoadNextTrial()
    {
        if (ES2.Exists("Trials/" + "trial" + trialNumber + ".txt"))
        {
            string fileName = "Trials/" + "trial" + trialNumber + ".txt";

            trialName = ES2.Load<string>(fileName + "?tag=trialName");
            samples = int.Parse(ES2.Load<string>(fileName + "?tag=sampleCount"));
            practiceRounds = int.Parse(ES2.Load<string>(fileName + "?tag=practiceCount"));
            minimumScale = int.Parse(ES2.Load<string>(fileName + "?tag=minimumScale"));
            maximumScale = int.Parse(ES2.Load<string>(fileName + "?tag=maximumScale"));
            minimumHorizontalOffset = int.Parse(ES2.Load<string>(fileName + "?tag=minimumHorizontalOffset"));
            maximumHorizontalOffset = int.Parse(ES2.Load<string>(fileName + "?tag=maximumHorizontalOffset"));
            minimumVerticalOffset = int.Parse(ES2.Load<string>(fileName + "?tag=minimumVerticalOffset"));
            maximumVerticalOffset = int.Parse(ES2.Load<string>(fileName + "?tag=maximumVerticalOffset"));

            Color startCol = ES2.Load<Color>(fileName + "?tag=startColor");
            Color goalCol = ES2.Load<Color>(fileName + "?tag=goalColor");

            UnityEngine.Debug.Log(startCol.ToString());

            startColorString = startCol.ToString();
            goalColorString = goalCol.ToString();

            string startColor = startColorString.Substring(5, 19);
            string goalColor = goalColorString.Substring(5, 19);
            string[] startColorRGB = startColor.Split(',');
            string[] goalColorRGB = goalColor.Split(',');

            startTargetColor = new Color(float.Parse(startColorRGB[0]), float.Parse(startColorRGB[1]), float.Parse(startColorRGB[2]));
            goalTargetColor = new Color(float.Parse(goalColorRGB[0]), float.Parse(goalColorRGB[1]), float.Parse(goalColorRGB[2]));

            startColorString = "R: " + goalColorRGB[0] + " G:" + goalColorRGB[1] + " B:" + goalColorRGB[2];
            goalColorString = "R: " + goalColorRGB[0] + " G:" + goalColorRGB[1] + " B:" + goalColorRGB[2];

            SetTargetColor(startTarget, startTargetColor);
            SetTargetColor(goalTarget, goalTargetColor);

            if (practiceRounds > 0)
            {
                isPracticeRound = true;
            }

            trialNumber++;
        }

        else
        {
            if (trialNumber <= 9)
            {
                trialNumber++;
                LoadNextTrial();
            }
            else
            {
                isPracticeRound = true;
                practiceRounds = 999;
                messageBoard.SetActive(true);
                //UploadCSVToServer();
                StartCoroutine(ReturnToMenu());
            }
        }   
    }

    public void SetTrialParameters()
    {
        if (currentSample < samples)
        {
            SetScale(minimumScale, maximumScale);
            SetOffsetRotation(minimumHorizontalOffset, maximumHorizontalOffset, true);
            SetOffsetRotation(minimumVerticalOffset, maximumVerticalOffset, false);
            cursorPositionString = "";

            if (isPracticeRound == true)
            {
                currentPracticeRound++;
            }

            else
            {
                currentSample++;
            }
        }

        else
        {
            currentSample = 0;
            currentPracticeRound = 0;
            LoadNextTrial();
        }
    }

    public void SetScale(int minScale, int maxScale)
    {
        int targetScales = UnityEngine.Random.Range(minScale, maxScale);
        startTarget.transform.localScale = new Vector3(targetScales, targetScales, targetScales);
        goalTarget.transform.localScale = new Vector3(targetScales, targetScales, targetScales);
    }

    public void SetOffsetRotation(float minimumRotation, float maximumRotation, bool isHorizontal)
    {
        // Pick a random value within the min and max
        float rotationOffset = (UnityEngine.Random.Range(minimumRotation, maximumRotation) * -1);

        if (isHorizontal == true)
        {
            if (UnityEngine.Random.value < 0.5f)
            {
                rotationOffset = rotationOffset * -1;
            }

            goalTargetPositioner.rotationOffset.y = rotationOffset;
        }

        else
        {
            goalTargetPositioner.rotationOffset.x = rotationOffset;
        }
    }

    public void SetTargetColor(GameObject target, Color color)
    {
        target.GetComponent<TargetObjectController>().SetColor(color);
    }

    public void IncrementSample()
    {
        samples++;
    }

    public int GetSample()
    {
        return samples;
    }

    public void IncrementPracticeRound()
    {
        currentPracticeRound++;

        if (currentPracticeRound > practiceRounds)
        {
            isPracticeRound = false;
            currentPracticeRound = 0;
        }
    }

    public int GetPracticeRounds()
    {
        return practiceRounds;
    }

    public void SetIsPracticeRound(bool status)
    {
        isPracticeRound = status;
    }

    public void LinkStartTarget(GameObject targetToLink)
    {
        startTarget = targetToLink;
    }

    public void LinkGoalTarget(GameObject targetToLink)
    {
        goalTarget = targetToLink;
    }

    public void StartTargetClickEvent()
    {
        if (isPracticeRound != true)
        {
            sampleStopwatch = new Stopwatch();
            sampleStopwatch.Start();
            sampleInProcess = true;
            spreadsheetManager.CreateSpreadsheet(participantID);
        }

        goalTarget.GetComponent<TargetObjectController>().SetClickable();
    }

    public void GoalTargetClickEvent()
    {
        if (isPracticeRound != true)
        {
            WriteToCSV();
            sampleInProcess = false;
            cursorPositionString = "";
        }

        else
        {
            IncrementPracticeRound();
        }

        startTarget.GetComponent<TargetObjectController>().SetClickable();
        SetTrialParameters();
    }

    public void WriteToCSV()
    {
        string[] newRow = new string[13];
        newRow[0] = trialName;
        newRow[1] = participantID;
        newRow[2] = participantName;
        newRow[3] = currentSample.ToString();
        newRow[4] = GetElapsedTime();
        newRow[5] = startTarget.transform.localScale.x.ToString();
        newRow[6] = goalTarget.transform.localScale.x.ToString();
        newRow[7] = CalculateArcDistance().ToString("#.##");
        newRow[8] = startTarget.transform.position.x.ToString() + " : " + startTarget.transform.position.y.ToString() + " : " + startTarget.transform.position.z.ToString();
        newRow[9] = goalTarget.transform.position.x.ToString() + " : " + goalTarget.transform.position.y.ToString() + " : " + goalTarget.transform.position.z.ToString();
        newRow[10] = startColorString;
        newRow[11] = goalColorString;
        newRow[12] = cursorPositionString;

        spreadsheetManager.AppendNewRow(newRow);

        foreach (string value in newRow)
        {
            experimentData.Add(value);
        }
        
    }

    public string GetElapsedTime()
    {
        sampleStopwatch.Stop();
        TimeSpan ts = sampleStopwatch.Elapsed;
        sampleElapsedTime = sampleStopwatch.Elapsed.TotalSeconds.ToString("#.#####");

        return sampleElapsedTime;
    }

    public double CalculateArcDistance()
    {
        double angle = Vector3.Angle(startTarget.transform.position, goalTarget.transform.position);
        double arcDistance = angle * (float)(Math.PI / 180) * 10;

        return arcDistance;
    }

    private void Update()
    {
        if (sampleInProcess)
        {
            if (cursorStopwatch.IsRunning == false)
                cursorStopwatch.Start();

            if (cursorStopwatch.Elapsed.TotalMilliseconds >= 200)
            {
                double angle = Vector3.Angle(goalTarget.transform.position, playerCamera.transform.position);
                double arcDistance = angle * (float)(Math.PI / 180) * 10;

                cursorPositionString = cursorPositionString + "<" + arcDistance.ToString("##.####") + ": " + sampleStopwatch.Elapsed.TotalSeconds.ToString("##.###") + "> ";
                cursorStopwatch.Reset();
            }
        }
    }

    public IEnumerator ReturnToMenu()
    {
        yield return new WaitForSecondsRealtime(10);
        gameObject.GetComponent<TraverseScene>().ChangeScene("HomeScreenNew");
    }

    public void UploadCSVToServer()
    {
        using (WebClient client = new WebClient())
        {
            string endpoint = "http://astralqueen.bw.edu/hci/upload.php";

            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

            UnityEngine.Debug.Log("Sending Data");

            using (ES2Reader reader = ES2Reader.Create(spreadsheetManager.GetResultsFile()))
            {
                string data = reader.Read<string>();
                string ud = "userStudyId=" + participantID + "&userStudyData=" + data;
                string response = client.UploadString(endpoint, ud);

                UnityEngine.Debug.Log(ud);
                UnityEngine.Debug.Log("Received Response: " + response);
            }
        }
    }

}