using System.Collections;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
	public GameObject ScaryEmpty;

	public bool hasPlayed;

	public bool triggerHit;

	public float DisplayTime;

	private void Start()
	{
		triggerHit = false;
		ScaryEmpty.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			triggerHit = true;
		}
	}

	private void Update()
	{
		if (triggerHit && !hasPlayed)
		{
			base.GetComponent<AudioSource>().Play();
			ScaryEmpty.SetActive(true);
			hasPlayed = true;
			StartCoroutine(RemoveOverTime());
		}
	}

	private IEnumerator RemoveOverTime()
	{
		yield return new WaitForSeconds(DisplayTime);
		ScaryEmpty.SetActive(false);
	}
}
