using UnityEngine;
using UnityEngine.UI;

public class ExampleColorReceiver : MonoBehaviour {

    public Image targetColorIndicatorImage;

	public void OnColorChange(HSBColor hsbColor) 
	{
        Color color = hsbColor.ToColor();
        targetColorIndicatorImage.color = new Color(color.r, color.g, color.b, 255);
    }

    /*
    public string ToHex(float n)
    {
        return ((int)(n * 255)).ToString("X").PadLeft(2, '0');
    }
    */
}
