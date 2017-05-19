using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCutoffOverTime : MonoBehaviour {
	public Renderer r;
	public float startingCutoff = 0.6f;
	public float time = 3f;
	private float timer;
	private float range;
	// Use this for initialization
	void Start () {
		range = 1f - startingCutoff;
	}
	void Update () {
		float newCutoff = ((timer / time) * range) + startingCutoff;
		newCutoff = Mathf.Clamp01(newCutoff);
		Debug.Log(newCutoff);
		r.material.SetFloat("_Cutoff", newCutoff);

		timer += Time.deltaTime;
	}
}
