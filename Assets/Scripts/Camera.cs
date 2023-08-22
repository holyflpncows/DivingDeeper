using UnityEngine;

public class OrtoCamResizerByScreenSize : MonoBehaviour
{
	[SerializeField] private float multiplier = 2.82f;

	private void Awake()
	{
	    // Camera.main.orthographicSize = Screen.height / Screen.width * multiplier;
	}
}

