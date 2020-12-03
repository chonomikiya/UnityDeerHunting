using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
	[SerializeField]
	//　ポーズした時に表示するUIのプレハブ
	private GameObject pauseUIPrefab = null;
	//　ポーズUIのインスタンス
	private GameObject pauseUIInstance = null;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("q")) {
			if (pauseUIInstance == null) {
				pauseUIInstance = GameObject.Instantiate (pauseUIPrefab) as GameObject;
				Time.timeScale = 0f;
			} else {
				Destroy (pauseUIInstance);
				Time.timeScale = 1f;
			}
		}
	}
}
