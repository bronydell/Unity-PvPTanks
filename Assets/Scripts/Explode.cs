using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

    private AudioSource _audio;

    public List<AudioClip> BoomAudioClips;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.clip = BoomAudioClips[Random.Range(0, BoomAudioClips.Count)];
        _audio.PlayDelayed(Random.Range(0, 0.1f));
    }
    void Bom()
    {
        foreach (var collision in Physics2D.OverlapCircleAll(transform.position, 0.4f))
        {
            if (collision.tag == "Player")
            {
                var player = collision.GetComponent<Player>();
                if (player.Lives <= 1)
                {
                    player.Die();
                    player.Score -= 100;
                }
                else
                    player.Lives -= 1;
            }
        }
        Destroy(gameObject);
    }
}
