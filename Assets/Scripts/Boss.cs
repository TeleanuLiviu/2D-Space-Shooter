using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    public GameObject _laser;
    private Animator _anim;
    private AudioSource audio;
    int hitcount = 0;
    void Start()
    {
        StartCoroutine(Shot());
        _anim = this.gameObject.GetComponent<Animator>();
        audio = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
  

    IEnumerator Shot()
    {

        while (true)
        {
           
            Instantiate(_laser, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.0f);


        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();

        if (other.tag == "Player")
        {
            
            if (player != null)
            {
                player.Damage();
                player.startShake = true;
            }

            
            
        }


        if (other.tag == "Laser")
        {
             hitcount = hitcount + 1;
            Destroy(other.gameObject);
            if (player != null)
            {
                player.AdScore();
            }

            if(hitcount >=30)
            {
                _anim.SetTrigger("OnEnemyDeath");


                audio.Play();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 2.2f);
            }
           

        }
    }
}
