using UnityEngine;

public class block : MonoBehaviour
{
    private Rigidbody2D rb;

    public float DownwardVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -DownwardVelocity);
        InvokeRepeating("IncreaseSpeed", 1f, 1f);
    }

    void Update()
    {
        if(transform.position.y < -7f)
        {
            Destroy(gameObject);
        }        
    }
    void IncreaseSpeed()
    {
        rb.velocity = new Vector2(0, -DownwardVelocity * 1.15f);
    }
}
