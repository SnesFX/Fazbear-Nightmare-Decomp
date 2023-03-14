using UnityEngine;

public class LoadLevel1 : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			AutoFade.LoadLevel("Game Over2", 1f, 1f, Color.red);
		}
	}
}
