using UnityEngine;
using System.Collections;

public class ReplicatingBhv : MonoBehaviour {
	public Manager manager;
	private bool seen;
	private int rearrangeDelay = 3;
	public static int bgWidth = 8;

	void OnBecameInvisible() {
		if (seen) {
//			Invoke("rearrange", rearrangeDelay);
			rearrange();
		}
	}

	void OnBecameVisible() {
		seen = true;
	}

	void rearrange ()
	{
		seen = false;
		Vector3 pos = transform.parent.transform.position;
		pos.x += (bgWidth * manager.backgroundPool);
		transform.parent.transform.position = pos;
	}
}
