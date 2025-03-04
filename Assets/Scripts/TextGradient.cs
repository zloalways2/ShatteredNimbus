using TMPro;
using UnityEngine;

/// <summary>
/// Apply gradient to whole text
/// </summary>
public class TextGradient : MonoBehaviour
{

	/// <summary>
	/// Do gradient text
	/// </summary>
	private void Start()
	{
		GameControllerBehaviour.onTextUpdateEvent.AddListener(ApplyGradient);
		ApplyGradient();
	}

	private void OnEnable()
	{
		ApplyGradient();
	}

	/// <summary>
	/// Gradient method
	/// Get gradient by steps, and make vertext gradient array from colors
	/// Apply to each character
	/// </summary>
	void ApplyGradient()
	{
		TMP_Text textComponent = GetComponent<TMP_Text>();
		textComponent.ForceMeshUpdate();
		TMP_TextInfo textInfo = textComponent.textInfo;
		int count = textInfo.characterCount;
		Color[] steps = GetGradients(textComponent.colorGradient.topLeft, textComponent.colorGradient.topRight, count + 1);
		VertexGradient[] gradients = new VertexGradient[steps.Length];
		for (int i = 0; i < steps.Length - 1; i++)
		{
			gradients[i] = new VertexGradient(steps[i], steps[i + 1], steps[i], steps[i + 1]);
		}
		Color32[] colors;
		int index = 0;
		while (index < count)
		{
			int materialIndex = textInfo.characterInfo[index].materialReferenceIndex;
			colors = textInfo.meshInfo[materialIndex].colors32;
			int vertexIndex = textInfo.characterInfo[index].vertexIndex;
			if (textInfo.characterInfo[index].isVisible)
			{
				colors[vertexIndex + 0] = gradients[index].bottomLeft;
				colors[vertexIndex + 1] = gradients[index].topLeft;
				colors[vertexIndex + 2] = gradients[index].bottomRight;
				colors[vertexIndex + 3] = gradients[index].topRight;
				textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
			}
			index++;
		}
	}

	/// <summary>
	/// Split gradient by steps from color to color
	/// </summary>
	/// <param name="start">Start color</param>
	/// <param name="end">End color</param>
	/// <param name="steps">Steps count</param>
	/// <returns>Array of colors</returns>
	public static Color[] GetGradients(Color start, Color end, int steps)
	{
		Color[] result = new Color[steps];
		float r = ((end.r - start.r) / (steps - 1));
		float g = ((end.g - start.g) / (steps - 1));
		float b = ((end.b - start.b) / (steps - 1));
		float a = ((end.a - start.a) / (steps - 1));
		for (int i = 0; i < steps; i++)
		{
			result[i] = new Color(start.r + (r * i), start.g + (g * i), start.b + (b * i), start.a + (a * i));
		}
		return result;
	}
}