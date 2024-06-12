using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Camera After Switch")]
	private CinemachineVirtualCamera virtualCamera;

	private int defaultPriority;

	// Start is called before the first frame update
	void Start()
	{
		// hold the original priority of BossBattleCamera
		defaultPriority = virtualCamera.Priority;
	}

	/// <summary>
	/// while the object with tag "Player" stay inside of SwitchArea
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerStay(Collider other)
	{
		// recognise the tag and make sure it is Player
		if (other.gameObject.tag == "Player")
		{
			//change the priority of BossCamera higher than any other camera
			virtualCamera.Priority = 100;
		}
	}

	/// <summary>
	/// back to NormalCamera when the player exit SwitchArea
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerExit(Collider other)
	{
		// recognise the tag and make sure it is Player
		if (other.gameObject.tag == "Player")
		{
			// restore to the original priority
			virtualCamera.Priority = defaultPriority;
		}
	}
}
