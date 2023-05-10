using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public float speed = 1.5f;
    public float shootingSpeed = 5f;
    public float firingCooldown = 1f;

    [SerializeField] private AudioClip laserAudio;
    [SerializeField] private float horizontalLimit = 2.5f;
    private PlayerInput playerInput;
    private AudioSource audioSource;
    private float cooldownTimer, inputAxis;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.actions["Fire"].performed += Fire;
    }

    private void OnDisable()
    {
        playerInput.actions["Fire"].performed -= Fire;
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0)) Fire();
        inputAxis = Input.GetAxisRaw("Horizontal");
#elif UNITY_ANDROID
        inputAxis = playerInput.actions["Move"].ReadValue<float>();
#endif

        // Player Movement
        rb.velocity = new Vector2(inputAxis * speed, 0f);
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
    }

    private void Fire(InputAction.CallbackContext ctx) => Fire();

    private void Fire()
    {
        // Player Attack
        if (cooldownTimer < 0)
        {
            cooldownTimer = firingCooldown;
            audioSource.PlayOneShot(laserAudio);

            // Get gameobject from pool
            GameObject laser = PlayerLaserPool.Instance.Get();
            laser.transform.position = transform.position;

            if (laser.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.Init();
                projectile.CancelInvoke();
                projectile.Invoke("Release", projectile.lifetime);
            }
        }
    }

    protected override void OnDie()
    {
        base.OnDie();
        SceneController.Instance.ChangeScene("Menu", 1f);
    }
}
