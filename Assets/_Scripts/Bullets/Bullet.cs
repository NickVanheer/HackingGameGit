using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float Speed = 20;
    public float duration = 5;
    public bool IsMoving = false;
    public bool IsIgnoreWall = false;
    public bool IsImmortal = false;
    public Vector3 MoveDirection;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (IsMoving)
        {
            transform.Translate(MoveDirection * Speed * Time.deltaTime);

        }

        if (IsImmortal)
            return;

        duration -= Time.deltaTime;

        if (duration <= 0)
            Destroy(this.gameObject);

    }

    public void Move(Vector3 direction)
    {
        MoveDirection = direction;
        IsMoving = true;
    }

    public void Move(Vector3 direction, float speed)
    {
        MoveDirection = direction;
        Speed = speed;
        IsMoving = true;
    }


    void OnTriggerEnter(Collider col)
    {
        //spawn particle effect whent it's a player bullet.
        if(this.tag == "PlayerBullet" && col.name != "Player")
            GameManager.GetInstance().SpawnParticles(transform.position);

        //destroy whenever the bullet touches anything, unrelated to what kind of bullet it it is.
        if (col.gameObject.tag == "Wall" && !IsIgnoreWall)
        {
            //Debug.Log(col.name + " > Bullet hit wall, destroying it");
            Destroy(this.gameObject);
        }

        //this bullet (orange enemy) touched a player bullet, destroy this bullet.
        if (col.gameObject.tag == "PlayerBullet" && this.tag == "OrangeEnemyBullet")
        {
            Destroy(this.gameObject);
        }

        //this bullet (player bullet) touched an enemy bullet, destroy this bullet.
        if (col.gameObject.tag == "OrangeEnemyBullet" && this.tag == "PlayerBullet")
        {
            Destroy(this.gameObject);
        }



    }
}
