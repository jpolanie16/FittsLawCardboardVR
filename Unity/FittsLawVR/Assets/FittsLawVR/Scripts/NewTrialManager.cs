using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class NewTrialManager : MonoBehaviour
{
    public Text trialNameText;
    public Text samplesText;
    public Text practiceRoundsText;
    public Text minimumScaleText;
    public Text maximumScaleText;
    public Text minimumHorizontalOffsetText;
    public Text maximumHorizontalOffsetText;
    public Text minimumVerticalOffsetText;
    public Text maximumVerticalOffsetText;

    public Image startTargetColorIndicator;
    public Image goalTargetColorIndicator;

    private int trialNumber;
    string trialID;

    public void SetTrialID()
    {
        trialID = "trial" + trialNumber.ToString() + ".txt";

        if (ES2.Exists("Trials/" + trialID))
        {
            trialNumber++;
            SetTrialID();
        }
    }

    public void SaveTrial()
    {
        trialNumber = 0;
        SetTrialID();
        string fileName = trialID;
        fileName = "Trials/" + fileName;

        ES2.Save(trialNameText.text, fileName + "?tag=trialName");
        ES2.Save(samplesText.text, fileName + "?tag=sampleCount");
        ES2.Save(practiceRoundsText.text, fileName + "?tag=practiceCount");
        ES2.Save(minimumScaleText.text, fileName + "?tag=minimumScale");
        ES2.Save(maximumScaleText.text, fileName + "?tag=maximumScale");
        ES2.Save(minimumHorizontalOffsetText.text, fileName + "?tag=minimumHorizontalOffset");
        ES2.Save(maximumHorizontalOffsetText.text, fileName + "?tag=maximumHorizontalOffset");
        ES2.Save(minimumVerticalOffsetText.text, fileName + "?tag=minimumVerticalOffset");
        ES2.Save(maximumVerticalOffsetText.text, fileName + "?tag=maximumVerticalOffset");
        ES2.Save(startTargetColorIndicator.color, fileName + "?tag=startColor");
        ES2.Save(goalTargetColorIndicator.color, fileName + "?tag=goalColor");
    }
}
