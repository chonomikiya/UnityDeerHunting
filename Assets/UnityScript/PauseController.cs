using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
	//　ポーズした時に表示するUIのプレハブ
	[SerializeField] private GameObject pauseUIPrefab = null;
	//　ポーズUIのインスタンス
	private GameObject pauseUIInstance = null;
	[SerializeField] private GameObject myMouseLook = null;
	void Start() {
		Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("q")) {
			if (pauseUIInstance == null) {
				pauseUIInstance = GameObject.Instantiate (pauseUIPrefab) as GameObject;
				myMouseLook.GetComponent<RigidbodyFirstPersonController>().mouseLook.MouseCursolLook();
				Time.timeScale = 0f;
			} else {
				myMouseLook.GetComponent<RigidbodyFirstPersonController>().mouseLook.MouseCursolInvisible();
				Destroy (pauseUIInstance);
				Time.timeScale = 1f;
			}
		}
	}
}
