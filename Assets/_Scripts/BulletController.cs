using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class BulletController : MonoBehaviour {
	public float    bulletSpeed = 0.1f;
	public Boundary boundary;

	private void Start() {
		boundary.top = 2.45f;
	}

	// Update is called once per frame
	private void Update() {
		Move();
		CheckBounds();
	}

	private void Move() {
		transform.position += new Vector3(0.0f, bulletSpeed * Time.deltaTime, 0.0f);
	}

	private void CheckBounds() {
		if (transform.position.y >= boundary.top) {
			BulletPoolManager.Instance.ResetBullet(gameObject);
		}
	}
}
