using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour, IPointerDownHandler
{

    private Rigidbody rb;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    public AudioSource audioSource;
    public AudioClip hitSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        audioSource = GameObject.Find("AUDIO_MANAGER").GetComponent<AudioSource>();
        gameManager = GameObject.Find("GAME_MANAGER").GetComponent<GameManager>();

        rb.AddForce(Vector3.up * Random.Range(10, 14), ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 10, ForceMode.Impulse);

        this.transform.position = new Vector3(Random.Range(-4, 4), -4, 0);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left Click");
        }

        audioSource.PlayOneShot(hitSound);
        if (!gameManager.isGameActive) return;
        Instantiate(explosionParticle, this.transform.position, explosionParticle.transform.rotation);
        Destroy(gameObject);
        gameManager.UpdateScore(pointValue);
        Debug.Log("Hit");

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!this.CompareTag("Bad") && other.CompareTag("Respawn"))
        {
            gameManager.GameOver();
            Debug.Log("Game Over");
        }
        if(other.CompareTag("Respawn"))
            Destroy(this.gameObject);
    }
}
