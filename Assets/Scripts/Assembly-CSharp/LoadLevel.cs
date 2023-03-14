using UnityEngine;

public class LoadLevel : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			AutoFade.LoadLevel("Game Over", 1f, 1f, Color.red);
		}
	}
}
