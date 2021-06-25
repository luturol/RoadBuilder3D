using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private int Stars = 0;

    [SerializeField] private RoadBehaviour roadPrefab;
    [SerializeField] private Transform rotateRoad;


    private PlatformBehaviour currentPlatform;

    private bool firstClickSpace = false;
    private bool isSpacePressed = false;
    private bool hasReleasedSpacebar = false;
    private bool moveToEndOfRoad = false;
    
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isSpacePressed = Input.GetKey(KeyCode.Space);
        if (isSpacePressed && (firstClickSpace == false || hasReleasedSpacebar == false))
        {
            if (firstClickSpace == false)
            {
                roadPrefab = currentPlatform.InstantiateRoad();
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
            hasReleasedSpacebar = true;
            time += Time.deltaTime;
            var rotationAngle = time * (90 / 10);

            bool canMove = true;
            
            if (rotationAngle <= 90)
            {
                Debug.Log("Rotacionando: " + rotationAngle + "graus");
                rotateRoad.transform.rotation = Quaternion.Euler(rotationAngle, 0f, 0f);
                canMove = false;
            }
            
            if (canMove && moveToEndOfRoad == false)
            {
                Debug.Log(rotationAngle);

                //move to the end of the road
                transform.position = Vector3.MoveTowards(transform.position, roadPrefab.GetEndOfRoad().transform.position, Time.deltaTime * 5);
                if (transform.position.Equals(roadPrefab.GetEndOfRoad().transform.position))
                {
                    moveToEndOfRoad = true;

                    time = 0f;
                    firstClickSpace = false;
                    isSpacePressed = false;
                    hasReleasedSpacebar = false;
                    moveToEndOfRoad = false;
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
            }

            currentPlatform = other.gameObject.transform.GetComponent<PlatformBehaviour>();


        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            Stars += 2;
        }

        if (other.gameObject.tag == "Falling")
        {
            Debug.Log("Dead press F to pay respect");
        }
    }

    private void Resize(Transform currentPlatform, float amount, Vector3 direction)
    {
        currentPlatform.position += direction * amount / 2;

        currentPlatform.localScale += direction * amount;
    }
}
