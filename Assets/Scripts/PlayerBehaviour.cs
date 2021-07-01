using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Stars config")]
    [SerializeField] private TextMeshProUGUI starCountText;
    [SerializeField] private int Stars = 0;

    [Header("End Game config")]
    [SerializeField] private EndGameBehaviour endGamePanel;

    [Header("Player Configs")]
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 10f;

    [Header("Platform Rotation")]
    [SerializeField] private float velocityPlatformDown = 5f;

    private RoadBehaviour roadPrefab;
    private Transform rotateRoad;
    private PlatformBehaviour currentPlatform;
    private Rigidbody playerRigidBody;
    private BoxCollider collider;

    private bool firstClickSpace = false;
    private bool isSpacePressed = false;
    private bool hasReleasedSpacebar = false;
    private bool moveToEndOfRoad = false;
    private bool hasAddedTargetScore = false;
    private float time = 0f;
    private bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        starCountText.text = "Stars: " + Stars.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        //has finished the game
        if (currentPlatform != null && currentPlatform.IsEndGame)
            return;

        isSpacePressed = Input.GetKey(KeyCode.Space);
        if (isSpacePressed && (firstClickSpace == false || hasReleasedSpacebar == false))
        {
            if (firstClickSpace == false)
            {
                roadPrefab = currentPlatform.InstantiateRoad();
                collider = roadPrefab.GetComponent<BoxCollider>();
                rotateRoad = currentPlatform.GetRoadPoint();
                firstClickSpace = true;
            }

            if (roadPrefab != null)
            {
                Resize(roadPrefab.transform, 1, new Vector3(0f, 0.01f, 0f));
            }
        }
        else if (firstClickSpace == true)
        {            
            collider.size = new Vector3(1f, 1f, 1f);

            hasReleasedSpacebar = true;
            time += Time.deltaTime;
            var rotationAngle = time * (90 / velocityPlatformDown);

            bool canMove = true;
            if (rotationAngle <= 90)
            {
                rotateRoad.transform.rotation = Quaternion.Euler(rotationAngle, 0f, 0f);
                canMove = false;
                animator.SetBool("Walk", canMove);
            }

            if (canMove && moveToEndOfRoad == false)
            {
                //move to the end of the road
                animator.SetBool("Walk", canMove);
                //transform.position = Vector3.MoveTowards(transform.position, roadPrefab.GetEndOfRoad().transform.position, Time.deltaTime * 5);
                var moveTo = Vector3.MoveTowards(transform.position, roadPrefab.GetEndOfRoad().transform.position, Time.deltaTime * movementSpeed);
                playerRigidBody.MovePosition(moveTo);
                if (playerRigidBody.position.Equals(roadPrefab.GetEndOfRoad().transform.position))
                {
                    moveToEndOfRoad = true;

                    time = 0f;
                    firstClickSpace = false;
                    isSpacePressed = false;
                    hasReleasedSpacebar = false;
                    moveToEndOfRoad = false;

                    canMove = false;
                    animator.SetBool("Walk", canMove);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Platform")
        {
            Debug.Log("Estamos na plataforma");
            if (currentPlatform != null && other.gameObject != currentPlatform)
            {
                Stars += 2;
                Debug.Log("(Platform) Somado +2 na stars. Total = " + Stars.ToString());
            }

            currentPlatform = other.gameObject.transform.GetComponent<PlatformBehaviour>();
            if (currentPlatform.IsEndGame)
            {
                bool win = true;

                Stars *= 2;
                Debug.Log("(Platform Final) duplicado stars. Total = " + Stars.ToString());

                endGamePanel.gameObject.SetActive(true);
                endGamePanel.SetWinOrLose(win);
                endGamePanel.SetStarsCount(Stars);
            }

            starCountText.text = "Stars: " + Stars.ToString();
        }
    }

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
}
