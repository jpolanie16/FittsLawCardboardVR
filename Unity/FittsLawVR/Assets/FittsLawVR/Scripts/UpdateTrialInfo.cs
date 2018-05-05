using UnityEngine;
using UnityEngine.UI;

public class UpdateTrialInfo : MonoBehaviour
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
    public Text startTargetColorText;
    public Text goalTargetColorText;

    public Image startTargetColorImage;
    public Image goalTargetColorImage;

    public void SetTrialInfo(string trialName, string samples, string practiceRounds, string minimumScale, string maximumScale,
        string minimumHorizontalOffset, string maximumHorizontalOffset, string minimumVerticalOffset, string maximumVerticalOffset,
        string startTargetColor, string goalTargetColor)
    {
        trialNameText.text = trialName;
        samplesText.text = "Samples: " + samples;
        practiceRoundsText.text = "Practice Rounds: " + practiceRounds;
        minimumScaleText.text = "Minimum Scale: " + minimumScale;
        maximumScaleText.text = "Maximum Scale: " + maximumScale;
        minimumHorizontalOffsetText.text = "Min Horizontal Offset: " + minimumHorizontalOffset;
        maximumHorizontalOffsetText.text = "Max Horizontal Offset: " + maximumHorizontalOffset;
        minimumVerticalOffsetText.text = "Min Vertical Offset: " + minimumVerticalOffset;
        maximumVerticalOffsetText.text = "Min Vertical Offset: " + maximumVerticalOffset;

        startTargetColor = startTargetColor.Substring(5, 19);
        goalTargetColor = goalTargetColor.Substring(5, 19);

        string[] startColorRGB = startTargetColor.Split(',');
        string[] goalColorRGB = goalTargetColor.Split(',');

        startTargetColorText.text = "Start Color:";
        goalTargetColorText.text = "Goal Color:";

        startTargetColorImage.color = new Color(float.Parse(startColorRGB[0]), float.Parse(startColorRGB[1]), float.Parse(startColorRGB[2]));
        goalTargetColorImage.color = new Color(float.Parse(goalColorRGB[0]), float.Parse(goalColorRGB[1]), float.Parse(goalColorRGB[2]));
    }
}
