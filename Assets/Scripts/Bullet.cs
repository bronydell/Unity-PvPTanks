using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{

    public float Speed = 0.5f;

    public GameObject Ignore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector2.up * Speed * Time.deltaTime);
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Ignore)
            return;
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Undestructable")
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            var player = other.GetComponent<Player>();
            if (player.Lives <= 1)
            {
                player.Die();
                var source = Ignore.GetComponent<Player>();
                source.Score += 200;
            }
            else
                player.Lives -= 1;
            Destroy(this.gameObject);
        }
    }
}
