using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{

    private float _speed = 10f;
    private AudioSource audio;
    [SerializeField]
    public GameObject Explosion;
    
    // Update is called once per frame
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        
    }

    void Update()
    {

       
       
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

       
            


        if (transform.position.y < -8.0f || transform.position.y > 8.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                player.startShake = true;
            }

            audio.Play();
            Instantiate(Explosion, transform.position, Quaternion.identity);
            _speed = 0.2f;
            Destroy(this.GetComponent<BoxCollider2D>());
            Destroy(this.GetComponent<SpriteRenderer>());
            Destroy(this.gameObject,2.2f);
            
        }
    }
    }
