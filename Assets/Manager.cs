using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public int backgroundPoolSize; // 2
	public int enemyPoolSize;

	public GameObject[] enemyCollection;

	public GameObject player;

	void Awake() {
		for (int i = 0; i < backgroundPoolSize; i++) {
			GameObject o = Instantiate (Resources.Load ("bg/BG")) as GameObject;
			incX (o, ReplicatingBhv.bgWidth * i);
		}

		for (int i = 0; i < enemyPoolSize; i++) {
			GameObject o = loadRandomEnemy();
			setXy (o, player.transform.position.x + 5, Random.Range(0, 5));
		}
	}

	private GameObject loadRandomEnemy() {
		if (enemyCollection.Length < 1) {
			Debug.LogWarning("enemy collection is empty");
		}
		return Instantiate (enemyCollection[Random.Range(0, enemyCollection.Length -1)]) 
			as GameObject;
	}

	private void incX(GameObject o, float x){
		incXy (o, x, null);
	}

	private void incY(GameObject o, float y){
		incXy (o, null, y);
	}

	private void setX(GameObject o, float x){
		setXy (o, x, null);
	}
	
	private void setY(GameObject o, float y){
		setXy (o, null, y);
	}

	private void incXy(GameObject o, float? x, float? y){
		if (x == null && y == null) return;

		Vector3 pos = o.transform.position;
		if (x!=null) pos.x += (float)x;
		if (y!=null) pos.y += (float)y;
		o.transform.position = pos;
	}

	private void setXy(GameObject o, float? x, float? y){
		if (x == null && y == null) return;
		
		Vector3 pos = o.transform.position;
		if (x!=null) pos.x = (float)x;
		if (y!=null) pos.y = (float)y;
		o.transform.position = pos;
	}
}
