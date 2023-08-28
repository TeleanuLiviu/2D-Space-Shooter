using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
  

    void Update()
    {
            transform.Translate(Vector3.left * Time.deltaTime * 3);

            if (transform.position.y < -8f)
            {
                Destroy(this.gameObject);
            }

    }

  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Player p = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (p != null)
            {
                p.AddAmmo();


            }
            Destroy(this.gameObject);
        }
    }
}
