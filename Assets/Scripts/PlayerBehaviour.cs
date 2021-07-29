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

    private PlatformBehaviour currentPlatform;
    private Rigidbody playerRigidBody;
        
    private bool hasAddedTargetScore = false;    
    private bool isDead = false;
    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        starCountText.text = "Stars: " + Stars.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
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
            if (currentPlatform != null && other.gameObject != currentPlatform)
            {
                var actual = Stars;
                Stars += 2;
                Debug.Log("(Platform) Somado +2 na stars. Atual = " + actual +" Total = " + Stars.ToString());
            }

            currentPlatform = other.gameObject.transform.GetComponent<PlatformBehaviour>();

            if(currentState == null && currentPlatform != null)
            {
                Debug.Log("criado InstantiatePlatformState");
                SetState(new InstantiatePlatformState(this, currentPlatform));
            }            

            if (currentPlatform.IsEndGame)
            {
                SetState(new EndGameState(this, endGamePanel, win: true));
            }
            
            starCountText.text = "Stars: " + Stars.ToString();
        }
    }

    //Need to refactor giving star points
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target" && hasAddedTargetScore == false)
        {
            Stars += 2;
            starCountText.text = "Stars: " + Stars.ToString();
            Debug.Log("(Target) Somado +2 na stars. Total = " + Stars.ToString());
            hasAddedTargetScore = true;
        }

        if (other.gameObject.tag == "Falling")
        {
            bool win = false;
            isDead = true;
            animator.SetBool("Dead", isDead);

            Debug.Log("Dead press F to pay respect");
            endGamePanel.gameObject.SetActive(true);
            endGamePanel.SetWinOrLose(win);
            endGamePanel.SetStarsCount(Stars);
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