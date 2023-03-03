using UnityEngine;

public class Player : Entity
{
    public float speed = 1.5f;
    public float firingCooldown = 1f;
    public GameObject laserPrefab;
    public new GameObject explosionPrefab;

    [SerializeField] private AudioClip laserAudio;
    [SerializeField] private float horizontalLimit = 2.5f;
    private AudioSource audioSource;
    private float cooldownTimer;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        // Player Movement
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0f);
        if (transform.position.x > horizontalLimit)
        {
            rb.velocity = Vector2.zero;
            transform.position = new Vector3(horizontalLimit, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -horizontalLimit)
        {
            rb.velocity = Vector2.zero;
            transform.position = new Vector3(-horizontalLimit, transform.position.y, transform.position.z);
        }

        // Player Attack
        if (Input.GetMouseButtonDown(0))
        {
            if (cooldownTimer < 0)
            {
                cooldownTimer = firingCooldown;
                audioSource.PlayOneShot(laserAudio);

                GameObject laser = PlayerLaserPool.Instance.Get();
                laser.SetActive(true);
                laser.transform.position = transform.position;
                laser.GetComponent<Projectile>().Init();
            }
        }
    }

    protected override void OnDie()
    {
        base.OnDie();
        SceneController.Instance.ChangeScene("Menu", 1f);
    }
}