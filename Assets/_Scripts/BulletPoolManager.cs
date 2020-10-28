using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletPoolManager : Singleton<BulletPoolManager> {
	[SerializeField]
	private GameObject bulletPrefab;

	[SerializeField]
	private int maxBullets = 10;

	private Queue<GameObject> m_bulletPool;

	private int m_numActiveBullets;

	public int  NumActiveBullets => m_numActiveBullets;

	public bool IsPoolEmpty => m_numActiveBullets < m_bulletPool.Count;

	// Start is called before the first frame update
	private void Start() {
		BuildBulletPool();
	}

	public GameObject GetBullet() {
		if (m_bulletPool.Count <= 0) {
			return null;
		}
		
		// Move bullet to back of queue then return a reference to it
		// When there are no more inactive bullets, calling GetBullet() implicitly retreives
		// the least recent bullet with no check necessary
		var bullet = m_bulletPool.Dequeue();
		m_bulletPool.Enqueue(bullet);
		
		bullet.SetActive(true);
		bullet.transform.position = bulletPrefab.transform.position;
		bullet.transform.rotation = Quaternion.identity;

		if (m_numActiveBullets < m_bulletPool.Count) {
			m_numActiveBullets++;
		}
		
		return bullet;
	}

	public void ResetBullet(GameObject a_bullet) {
		if (a_bullet == null || !m_bulletPool.Contains(a_bullet)) {
			return;
		}
		
		a_bullet.SetActive(false);
		m_numActiveBullets--;
	}

	private void BuildBulletPool() {
		m_bulletPool = new Queue<GameObject>(maxBullets);
		
		for (var i = 0; i < maxBullets; i++) {
			m_bulletPool.Enqueue(Instantiate(bulletPrefab, transform));
		}
	}
}
