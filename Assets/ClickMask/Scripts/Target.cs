using UnityEngine;

public class Target : MonoBehaviour
{

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        rb.AddForce(Vector3.up * Random.Range(12, 16), ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 10, ForceMode.Impulse);

        this.transform.position = new Vector3(Random.Range(-4, 4), -6, 0);
    }

    // Update is called once per frame
    void Update()
    {


        
    }
}
