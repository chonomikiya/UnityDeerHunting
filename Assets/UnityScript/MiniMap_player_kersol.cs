using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap_player_kersol : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    RectTransform rectTransform = null;
	[SerializeField] Transform target = null;
	[SerializeField] Camera MiniMapCamera = null;

	void Awake()
	{
		rectTransform = GetComponent<RectTransform> ();
	}

	void Update()
	{
		target.position = RectTransformUtility.WorldToScreenPoint (MiniMapCamera, player.transform.position);
	}
}
