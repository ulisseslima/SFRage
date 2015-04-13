using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public int backgroundPool; // 2

	void Awake() {
		for (int i = 0; i < backgroundPool; i++) {
			GameObject bg = Instantiate (Resources.Load ("bg/BG")) as GameObject;
			Vector3 pos = bg.transform.position;
			pos.x += (ReplicatingBhv.bgWidth*i);
			bg.transform.position = pos;
		}
	}
}
