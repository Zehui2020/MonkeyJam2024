using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Class that handles player movement, inputs & physics
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    //SerializeField Speed of Bike, BrakeForce, Jump Force, rotation AirForce & RotationSpeed of bike
    [SerializeField] float speed;
    [SerializeField] float speedLimit;
    [SerializeField] float rampSpeedModifier;
    [SerializeField] float brakeForce;
    [SerializeField] float jumpForce;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationAirForce;
    [SerializeField] float minGroundDist;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ParticleSystem dustTrail;
    [SerializeField] private ItemStats itemStats;

    private bool isFallen = false;
    [SerializeField] private float minFallAngle;
    [SerializeField] private float maxFallAngle;
    [SerializeField] private GameObject welshFlag;

    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI moneyText;

    //UI
    [SerializeField] Image gunImage;
    [SerializeField] private TextMeshProUGUI gunText;
    [SerializeField] private GameObject weaponUI;

    private Coroutine jumpRoutine;
    private Coroutine checkFlipRoutine = null;

    private RaycastHit2D rampHit;

    private IInteractable interactable;

    //Rigidbody of the bicycle
    private Rigidbody2D rigidBody;
    //Checks if bike is braking & will allow player to move forward or not
    private bool isBraking;
    //Checks if wheels are grounded
    [SerializeField] private bool isGrounded;
    ////Checks if player is near anything
    //private bool isPlayerNearAnything;
    //Checks if player is pressing W or S or nothing
    private int isPlayerMoving;
    //setting weapons
    [SerializeField] List<Weapon> WeaponList;
    private Weapon equippedWeapon;
    public enum WeaponType
    {
        Rifle,
        Shotgun,
        BurstRifle,
        SniperRifle,
        RocketLauncher,
        Flamethrower,
        Sword

    }

    private EntityAudioController entityAudioController;

    bool isFalling;

    //Code for initialising in Game Controller
    public void Initialise()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Instance = this;
        equippedWeapon = null;

        isFalling = false;

        //sound
        entityAudioController = GetComponent<EntityAudioController>();
        //check if don't have component
        if (entityAudioController == null)
        {
            //add component
            entityAudioController = gameObject.AddComponent<EntityAudioController>();
        }
    }

    //Code to update player in Game Controller
    public void UpdatePlayer()
    {
        HandleGunUI();
        CheckFallenDown();
        CheckGroundCollision();
        SpeedControl();
        UpdateDustTrailPS();

        moneyText.text = money.ToString();

        if (itemStats.stunRadius > 0)
            welshFlag.SetActive(true);
        else
            welshFlag.SetActive(false);

        if (!isGrounded && checkFlipRoutine == null)
            checkFlipRoutine = StartCoroutine(CheckFlip());
        else if (isGrounded && checkFlipRoutine != null)
        {
            StopCoroutine(checkFlipRoutine);
            checkFlipRoutine = null;
        }

        Vector3 force;
        // Adjust drag & force
        if (isGrounded)
            force = Vector3.right * speed * itemStats.movementSpeedModifier * isPlayerMoving * 10f;
        else
            force = Vector3.zero;

        // Move player
        if (OnRamp())
        {
            rigidBody.AddForce(GetRampMoveDir() * speed * itemStats.movementSpeedModifier * rampSpeedModifier * isPlayerMoving, ForceMode2D.Force);
            Debug.Log("CALLED");
        }
        else
            rigidBody.AddForce(force, ForceMode2D.Force);

        if (isGrounded)
            rigidBody.angularVelocity = Mathf.Clamp(rigidBody.angularVelocity, -60f, 60f);
        else
            rigidBody.angularVelocity = Mathf.Clamp(rigidBody.angularVelocity, -60f * rotationAirForce, 60f * rotationAirForce);

        float scaleX = transform.localScale.x;
        if (rigidBody.velocity.x > 0.05 && isPlayerMoving == 1)
            transform.localScale = new Vector3(scaleX > 0 ? -scaleX : scaleX, transform.localScale.y, transform.localScale.z);
        else if (rigidBody.velocity.x < -0.05f && isPlayerMoving == -1)
            transform.localScale = new Vector3(Mathf.Abs(scaleX), transform.localScale.y, transform.localScale.z);
        //player not moving
        if (isPlayerMoving == 0 || !isGrounded)
        {
            entityAudioController.StopAudio("cycling");
        }
        //player is moving
        else
        {
            entityAudioController.PlayAudio("cycling");
        }

        if (equippedWeapon)
        equippedWeapon.UpdateGun();
    }

    //Sets if player is pressing W or S or nothing
    public void SetPlayerMovement(int _newIsPlayerMoving)
    {
        isPlayerMoving = _newIsPlayerMoving;
    }

    //Movement input from GameControlller
    public void Movement(MovementAxisCommand _movementAxisCommand)
    {
        if (!isBraking && isGrounded && !isFallen)
        {
            rigidBody.AddForce(new Vector2(_movementAxisCommand.HorizontalAxis * Time.deltaTime * speed * itemStats.movementSpeedModifier, 0), ForceMode2D.Impulse);

            if (_movementAxisCommand.HorizontalAxis < 0)
            {
                isPlayerMoving = -1;
            }
            else
            {
                isPlayerMoving = 1;
            }
        }
    }

    //Braking input from GameControlller
    public void Braking()
    {
        if (isGrounded)
            rigidBody.AddForce(new Vector2(-rigidBody.velocity.x * Time.deltaTime * brakeForce, 0), ForceMode2D.Impulse);
    }

    //Jump input from GameController
    public void Jump()
    {
        if (jumpRoutine == null && !isFallen)
            jumpRoutine = StartCoroutine(JumpRoutine());
    }

    private IEnumerator JumpRoutine()
    {
        if (isGrounded)
        {
            
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //jump sound
        entityAudioController.PlayAudio("jump", true);

        yield return new WaitForSeconds(0.5f);
        jumpRoutine = null;
    }

    //Setting boolean of braking
    public void SetBraking(bool newBraking)
    {
        if (isGrounded)
            isBraking = newBraking;
    }

    //Adding torque for rotation of bike, rotation input from GameControlller
    public void Rotation(RotationAxisCommand rotationAxisCommand)
    {
        if (isGrounded)
        {
            rigidBody.AddTorque(rotationAxisCommand.RotationAxis * Time.deltaTime * rotationSpeed, ForceMode2D.Impulse);
        }
        else
        {
            rigidBody.AddTorque(rotationAxisCommand.RotationAxis * Time.deltaTime * rotationSpeed * rotationAirForce, ForceMode2D.Impulse);
        }
    }

    public void CheckGroundCollision()
    {
        RaycastHit2D groundHit;
        groundHit = Physics2D.Raycast(transform.position, Vector3.down, 100, groundLayer);

        float dist = Vector3.Distance(transform.position, groundHit.point);
        if (dist <= minGroundDist)
        {
            isGrounded = true;
            //check if is falling
            if (isFalling)
            {
                isFalling = false;
                //play fall sound
                entityAudioController.PlayAudio("land");
            }
        }
            
        else if (dist > minGroundDist)
        {
            isGrounded = false;
            isFalling = true;
        }
            
    }

    private void CheckFallenDown()
    {
        if (transform.eulerAngles.z > minFallAngle && transform.eulerAngles.z < maxFallAngle)
            isFallen = true;
        else
            isFallen = false;
    }

    private void UpdateDustTrailPS()
    {
        if (isGrounded && !isFallen)
        {
            float normalizedVelocity = Mathf.Clamp01(rigidBody.velocity.magnitude / speedLimit * itemStats.movementSpeedModifier);
            float emissionRate = Mathf.Lerp(0, 20, normalizedVelocity);

            var em = dustTrail.emission;
            em.enabled = true;
            em.rateOverTime = emissionRate;
        }
        else
        {
            var em = dustTrail.emission;
            em.enabled = false;
        }

        if (rigidBody.velocity.x > 0)
        {
            var vel = dustTrail.velocityOverLifetime;
            ParticleSystem.MinMaxCurve xCurve = new ParticleSystem.MinMaxCurve(-1, -5);
            vel.x = xCurve;
        }
        else
        {
            var vel = dustTrail.velocityOverLifetime;
            ParticleSystem.MinMaxCurve xCurve = new ParticleSystem.MinMaxCurve(1, 5);
            vel.x = xCurve;
        }
    }

    private bool OnRamp()
    {
        rampHit = Physics2D.Raycast(transform.position, Vector3.down, minGroundDist, groundLayer);

        if (rampHit)
        {
            float angle = Vector3.Angle(Vector3.up, rampHit.normal);
            return angle < -3 || angle > 3;
        }

        return false;
    }

    private Vector3 GetRampMoveDir()
    {
        return Vector3.ProjectOnPlane(Vector3.right, rampHit.normal).normalized;
    }

    private void SpeedControl()
    {
        Vector2 currentVel = new Vector2(rigidBody.velocity.x, 0);

        if (currentVel.magnitude > speedLimit * itemStats.movementSpeedModifier)
        {
            Vector3 limitVel = currentVel.normalized * speedLimit * itemStats.movementSpeedModifier;
            rigidBody.velocity = new Vector2(limitVel.x, rigidBody.velocity.y);
        }
    }

    private IEnumerator CheckFlip()
    {
        float accumulatedRotation = 0f;
        float lastFrameRotation = 0f;

        while (true)
        {
            float currentRotation = transform.eulerAngles.z;
            float deltaRotation = Mathf.DeltaAngle(lastFrameRotation, currentRotation);

            accumulatedRotation += Mathf.Abs(deltaRotation);
            lastFrameRotation = currentRotation;

            if (accumulatedRotation >= 340f)
            {
                accumulatedRotation = 0f;
                OnFlip();
            }

            yield return null;
        }
    }

    private void OnFlip()
    {
        if (itemStats.stunRadius <= 0)
            return;

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, itemStats.stunRadius);
        foreach (Collider2D col in cols)
        {
            if (!col.TryGetComponent<EnemyEntity>(out EnemyEntity enemy))
                continue;

            enemy.Stun();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 collisionPoint = collision.contacts[collision.contacts.Length - 1].point;
        dustTrail.transform.position = new Vector3(collisionPoint.x, collisionPoint.y + 0.1f, collisionPoint.z);
    }

    public void SetInteractable(IInteractable interactable)
    {
        this.interactable = interactable;
    }

    public void OnInteract()
    {
        if (interactable == null)
            return;

        interactable.OnInteract();
    }

    //Weapon Usage
    public void UseWeapon()
    {
        if (equippedWeapon)
        equippedWeapon.Use("Player");
    }

    //Equipping a new weapon
    public void EquipWeapon(WeaponType newweapon)
    {
        if (equippedWeapon)
        {
            if (equippedWeapon && equippedWeapon.GetName().Equals(WeaponList[(int)newweapon].GetName()))
            {
                equippedWeapon.Upgrade();
                return;
            }
            equippedWeapon.gameObject.SetActive(false);
        }
        equippedWeapon = WeaponList[(int)newweapon];
        equippedWeapon.Initialise();
        equippedWeapon.gameObject.SetActive(true);
    }

    public int GetMoney()
    {
        return money;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void DeductMoney(int amount)
    {
        money -= amount;
    }

    //Getting isGrounded
    public bool GetGrounded()
    {
        return isGrounded;
    }

    //Getting equipped weapon
    public Weapon GetWeapon()
    {
        return equippedWeapon;
    }

    public void Reload()
    {
        if (equippedWeapon)
        {
            equippedWeapon.StartReload();
        }
    }

    public void InstantlyReload()
    {
        if (equippedWeapon)
            equippedWeapon.Reload();
    }

    public void HandleGunUI()
    {
        if (equippedWeapon == null)
        {
            //no gun
            gunImage.sprite = null;
            gunText.text = "";
            weaponUI.SetActive(false);
        }
        else
        {
            //have gun
            weaponUI.SetActive(true);
            gunImage.sprite = equippedWeapon.weaponItem.spriteIcon;
            gunText.text = equippedWeapon.GetName() + "\nLevel: " + equippedWeapon.GetUpgradeLevel().ToString() + "\n" + equippedWeapon.GetAmmoString();
        }
    }
}
