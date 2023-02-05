using UnityEngine;

public class Collectable : MonoBehaviour
{

    //DONT FORGET TO SET TAG PLAYER ON CURRENT PLAYER!
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().AddScore(1);
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject, gameObject.GetComponent<AudioSource>().clip.length);
        }
    }
}
