using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class PlayerController : MonoBehaviour {
	public Speed          verticalSpeed;
	public float          maxSpeed;
	public Boundary       boundary;
	public GameController gameController;
	public Transform      bulletSpawn;

	// private instance variables
	private AudioSource m_thunderSound;
	private AudioSource m_yaySound;
	private AudioSource m_bulletSound;
	private Rigidbody2D m_rigidbody2D;

	private bool m_isFiring = false;

	// Start is called before the first frame update
	private void Start() {
		m_thunderSound = gameController.audioSources[(int) SoundClip.Thunder];
		m_yaySound     = gameController.audioSources[(int) SoundClip.Yay];
		m_bulletSound  = GetComponent<AudioSource>();
		m_rigidbody2D  = GetComponent<Rigidbody2D>();

		// Shoots bullet on a delay if button is pressed
		StartCoroutine(FireBullet());
	}

	// Update is called once per frame
	private void Update() {
		// Move player
		Move();

		// Checks if shoot button is pressed
		ActionCheck();

		// Destroys bullet when it's off screen
		CheckBounds();
	}

	public void Move() {
		if (Input.GetAxis("Horizontal") > 0.1f) {
			m_rigidbody2D.AddForce(new Vector2(verticalSpeed.max * Time.deltaTime, 0.0f));
		}

		if (Input.GetAxis("Horizontal") < -0.1f) {
			m_rigidbody2D.AddForce(new Vector2(verticalSpeed.min * Time.deltaTime, 0.0f));
		}

		m_rigidbody2D.velocity =  Vector2.ClampMagnitude(m_rigidbody2D.velocity, 5.0f);
		m_rigidbody2D.velocity *= 0.95f;
	}

	private void CheckBounds() {
		// check right boundary
		if (transform.position.x > boundary.right) {
			transform.position = new Vector2(boundary.right, transform.position.y);
		}

		// check left boundary
		if (transform.position.x < boundary.left) {
			transform.position = new Vector2(boundary.left, transform.position.y);
		}
	}

	private void ActionCheck() {
		// see Edit -> Project Settings -> Input
		if (Input.GetAxis("Jump") > 0) {
			m_isFiring = true;
		} else {
			m_isFiring = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D a_other) {
		switch (a_other.gameObject.tag) {
		case "Cloud":
			m_thunderSound.Play();
			gameController.Lives -= 1;
			break;
		case "Island":
			m_yaySound.Play();
			gameController.Score += 100;
			break;
		}
	}

	private IEnumerator FireBullet() {
		while (true) {
			// Check every 0.15 seconds if shoot button is pressed
			yield return new WaitForSeconds(0.15f);
			if (m_isFiring) {
				m_bulletSound.Play();

				var bullet = BulletPoolManager.Instance.GetBullet();
				bullet.transform.position = bulletSpawn.position;
				bullet.transform.rotation = Quaternion.identity;
			}

		}
	}

}
