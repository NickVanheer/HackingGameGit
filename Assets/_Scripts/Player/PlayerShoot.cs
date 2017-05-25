using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    public GameObject BulletPrefab;
    public Transform SpawnPosition;
    public float FireCooldown = 1;

    private float coolDownTimer = 0;

	void Update () {

        coolDownTimer -= Time.deltaTime;

        float rT = Input.GetAxis("RightTrigger");

        if (Input.GetMouseButton(0))
            rT = 1;
        
        if (rT > 0.95f && coolDownTimer <= 0)
        {
            GameObject gO = Utils.InstantiateSafe(BulletPrefab, SpawnPosition.position);
            Bullet b = gO.GetComponent<Bullet>();

            //parent, can be optimized
            string name = this.gameObject.name + " bullets";
            GameObject groupObject = GameObject.Find(name);

            if (groupObject == null)
                groupObject = new GameObject(name);

            gO.transform.parent = groupObject.transform;

            b.Move(this.transform.forward);

            //move forward
            coolDownTimer = FireCooldown;

            //shoot sound
            SoundManager.GetInstance().PlayShootSound();
        }

	}
}
