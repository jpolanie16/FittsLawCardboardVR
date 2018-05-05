using UnityEngine;
using LlockhamIndustries.Decals;

public class TargetObjectController : MonoBehaviour
{
    private ProjectionRenderer myProjectionRenderer;
    private NewExperimentController expController;
    private Color myColor;
    private int myScale;
    private bool isClickable;

    void Awake()
    {
        myProjectionRenderer = gameObject.GetComponent<ProjectionRenderer>();

        expController = GameObject.FindGameObjectWithTag("TargetCaster").GetComponent<NewExperimentController>();

        if (gameObject.tag == "Start")
        {
            isClickable = true;
            SetMediumAlpha();
            expController.LinkStartTarget(gameObject);
        }

        else
        {
            isClickable = false;
            SetLowAlpha();
            expController.LinkGoalTarget(gameObject);
        } 
    }


    public void UpdateProjectionColor()
    {
        myProjectionRenderer.SetColor(0, myColor);
        myProjectionRenderer.UpdateProperties();
    }

    public void SetColor(Color newColor)
    {
        myColor = newColor;
        UpdateProjectionColor();
    }

    public void SetHighAlpha()
    {
        if (isClickable == true)
        {
            myColor = new Color(myColor.r, myColor.g, myColor.b, 1);
            UpdateProjectionColor();
        }
    }

    public void SetMediumAlpha()
    {
        if (isClickable == true)
        {
            myColor = new Color(myColor.r, myColor.g, myColor.b, 0.75f);
            UpdateProjectionColor();
        }
    }

    public void SetLowAlpha()
    {
        myColor = new Color(myColor.r, myColor.g, myColor.b, 0.25f);
        UpdateProjectionColor();
    }

    public void SetScale(int newScale)
    {
        myScale = newScale;
        myProjectionRenderer.UpdateProperties();
    }

    public void SetClickable()
    {
        isClickable = true;
        SetMediumAlpha();
    }

    public void SetUnClickable()
    {
        isClickable = false;
        SetLowAlpha();
    }


    public void StartTargetClickEvent()
    {
        if (isClickable == true)
        {
            SetUnClickable();
            expController.StartTargetClickEvent();
        }
    }

    public void GoalTargetClickEvent()
    {
        if (isClickable == true)
        {
            SetUnClickable();
            expController.GoalTargetClickEvent();
        }
    }

    public ProjectionRenderer GetProjectionRenderer()
    {
        return myProjectionRenderer;
    }

    public Color GetColor()
    {
        return myColor;
    }

    public int GetScale()
    {
        return myScale;
    }

    public bool GetClickableStatus()
    {
        return isClickable;
    }
}