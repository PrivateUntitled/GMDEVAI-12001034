using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosion;

	private GameObject shooter;
	private int dmg;

	public void Init(int _dmg, GameObject _shooter)
    {
		dmg = _dmg;
		shooter = _shooter;
    }
	
	void OnCollisionEnter(Collision col)
    {
		if (col.gameObject != shooter)
		{
			GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);

			HPComponent hpComponent = col.gameObject.GetComponentInParent<HPComponent>();

			if (hpComponent != null)
				hpComponent.TakeDamage(dmg);


			Destroy(e, 1.5f);
			Destroy(this.gameObject);
		}
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
