using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -8f)
        {
            Destroy(this.gameObject);
        }

    }
    //Secondary Fire Powerup●Create a new form ofprojectile.You shouldalready have a tripleshot. Includesomething new frommulti direction shot, toheat seeking shots,etc.●Replaces thestandard fire for 5seconds.●Spawns rarely
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Player")
        {

            Player p = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (p!=null)
            {
                if (powerupID == 0)
                { p.TripleShotActive(); }
                else if (powerupID == 1)
                { p.SpeedActive(); }
                else if (powerupID == 2)
                { p.ShieldActive(); }
                else if (powerupID == 3)
                { p.AddHeart(); }
                else if (powerupID == 4)
                { p.SpecialShot(); }
                else if (powerupID == 5)
                { p.NegativePickUp(); }


            }
            Destroy(this.gameObject);
        }
    }

    

}
