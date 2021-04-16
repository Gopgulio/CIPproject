using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public int maxHealth = 100;
    private int _currentHealth;

    private GameObject currentStatus;
    private IconStatus statusIcon;

    public Rigidbody2D body;
    public Animator animator;
    public Animator legs;

    public Vector2 movement;
    public Vector3 worldPoint;
    private Vector3 aimDirection;
    private bool fltEnabled = false;

    [SerializeField] private FieldOfVision fov;
    [SerializeField] private UnityEngine.Experimental.Rendering.Universal.Light2D flashlight;

    void Start()
    {
        flashlight.enabled = fltEnabled;

        currentHealth = maxHealth;
        currentStatus = GameObject.Find("currentStatusIcon");
        statusIcon = currentStatus.GetComponent<IconStatus>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

        aimDirection = worldPoint - transform.position;
        fov.setOrigin(transform.position);
        fov.setAimDirection(aimDirection);

        animator.SetFloat("Speed", movement.sqrMagnitude);
        legs.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.F))
        {
            fltEnabled = !fltEnabled;
            flashlight.enabled = fltEnabled;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            takeDamage(20);
        }

    }
    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * moveSpeed * Time.fixedDeltaTime);
        body.rotation = Utils.angleFromVector(aimDirection) - 90f;

    }

    public int currentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            if (_currentHealth < value && currentStatus != null)
            {
                StartCoroutine(flashGreen(.4f, 1f, 1f, 1f, 1.0f / 90.0f));
            }
            _currentHealth = value;


            if (statusIcon != null)
            {
                statusIcon.changeState(_currentHealth);
            }

            if (_currentHealth > maxHealth)
            {
                _currentHealth = maxHealth;
            }
        }
    }
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public IEnumerator flashGreen(float minAlpha, float maxAlpha, float interval, float duration, float timeRate)
    {
        Image iconImage = currentStatus.GetComponent<Image>();
        Color colorNow = iconImage.color;
        Color minColor = new Color(.8f, iconImage.color.g, .8f, minAlpha);
        Color maxColor = new Color(.8f, iconImage.color.g, .8f, maxAlpha);

        float currentInterval = 0;
        while (duration > 0)
        {
            float tColor = currentInterval / interval;
            iconImage.color = Color.Lerp(minColor, maxColor, tColor);

            currentInterval += timeRate;
            if (currentInterval >= interval)
            {
                Color temp = minColor;
                minColor = maxColor;
                maxColor = temp;
                currentInterval = currentInterval - interval;
            }
            duration -= timeRate;
            yield return null;
        }

        iconImage.color = colorNow;
    }
}
