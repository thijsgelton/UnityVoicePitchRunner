using UnityEngine;

public class Block : MonoBehaviour
{
    private Rigidbody2D rb;

    public float DownwardVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -DownwardVelocity);
    }

    void Update()
    {
        if(transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }
}
