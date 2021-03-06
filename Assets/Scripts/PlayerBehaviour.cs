using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int Stars { get; set; } = 0;

    [Header("Stars config")]
    [SerializeField] private TextMeshProUGUI starCountText;

    [Header("End Game config")]
    [SerializeField] private EndGameBehaviour endGamePanel;

    [Header("Player Configs")]
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 10f;

    [Header("Platform Rotation")]
    [SerializeField] private float velocityPlatformDown = 5f;

    [Header("Audio")]
    [SerializeField] private AudioClip starsPlatformSound;
    [SerializeField] private AudioClip targetPlatformSound;

    //Cached
    private PlatformBehaviour currentPlatform;
    private Rigidbody playerRigidBody;
    private AudioSource audioSource;

    private bool hasAddedTargetScore = false;
    private bool hasAddedPlatformScore = false;
    private bool isDead = false;
    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerRigidBody = GetComponent<Rigidbody>();
        starCountText.text = "Stars: " + Stars.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
            currentState.Tick();
    }

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExist();

        currentState = state;

        if (currentState != null)
            currentState.OnStateEnter();
    }

    //Need to refactor giving star points
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Platform")
        {
            Debug.Log("Estamos na plataforma");

            bool hasNotAddedScore = hasAddedPlatformScore == false;
            bool isStateInstatiatePlatform = currentState is InstantiatePlatformState;

            Debug.Log("Já adicionar o score " + hasNotAddedScore + " e state é instantiate " + isStateInstatiatePlatform);
            if (hasNotAddedScore && isStateInstatiatePlatform)
            {
                AddScore(starsPlatformSound);
                Debug.Log("(Platform) Somado +2 na stars. Total = " + Stars.ToString());
                hasAddedPlatformScore = true;
            }

            SetCurrentPlatform(other.gameObject);


            if (currentState == null && currentPlatform != null)
            {
                hasAddedPlatformScore = false;
                Debug.Log("criado InstantiatePlatformState");
                SetState(new InstantiatePlatformState(this));
            }

            if (currentPlatform.IsEndGame)
            {
                SetState(new EndGameState(this, endGamePanel, win: true));
            }

            starCountText.text = "Stars: " + Stars.ToString();
        }

    }

    private void AddScore(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        var actual = Stars;
        Stars += 2;        
    }

    private void SetCurrentPlatform(GameObject platform)
    {
        currentPlatform = platform.GetComponent<PlatformBehaviour>();
    }

    //Need to refactor giving star points
    private void OnTriggerEnter(Collider other)
    {
        if (currentState is InstantiatePlatformState)
        {
            if (other.gameObject.tag == "Target" && hasAddedTargetScore == false)
            {
                AddScore(targetPlatformSound);                
                hasAddedTargetScore = true;
            }
        }

        if (other.gameObject.tag == "Falling")
        {
            Debug.Log("Dead press F to pay respect");   
            bool win = false;
            isDead = true;
            animator.SetBool("Dead", isDead);
            
            SetState(new EndGameState(this, endGamePanel, win));                     
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            Debug.Log("Não estamos mais tocando na plataforma");
            hasAddedTargetScore = false;
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Platform")
        {
            hasAddedPlatformScore = false;
        }
    }

    private void Resize(Transform currentPlatform, float amount, Vector3 direction)
    {
        currentPlatform.position += direction * amount / 2;

        currentPlatform.localScale += direction * amount;
    }

    public float GetMovementSpeed() => movementSpeed;
    public float GetVelocityPlatformDown() => velocityPlatformDown;
    public Animator GetAnimator() => animator;
    public Rigidbody GetRigidBody() => playerRigidBody;
    public PlatformBehaviour GetCurrentPlatform() => currentPlatform;
}