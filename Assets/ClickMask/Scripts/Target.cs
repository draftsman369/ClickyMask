using System.Drawing;
using UnityEngine;

public class Target : MonoBehaviour
{

    private Rigidbody rb;
    private GameManager gameManager;
    public int pointValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GAME_MANAGER").GetComponent<GameManager>();

        rb.AddForce(Vector3.up * Random.Range(10, 14), ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 10, ForceMode.Impulse);

        this.transform.position = new Vector3(Random.Range(-4, 4), -4, 0);
    }


    private void OnMouseDown()
    {
        if (!gameManager.isGameActive) return;
        Destroy(gameObject);
        gameManager.UpdateScore(pointValue);
        Debug.Log("Hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!this.CompareTag("Bad"))
        {
            gameManager.GameOver();
            Debug.Log("Game Over");
        }
        Destroy(this.gameObject);
    }
}
