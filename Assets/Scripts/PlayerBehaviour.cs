using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private RoadBehaviour roadPrefab;
    private Transform rotateRoad;
    

    private PlatformBehaviour currentPlatform;
    
    private bool firstClickSpace = false;
    private bool isSpacePressed = false;
    private bool hasReleasedSpacebar = false;
    private bool moveToEndOfRoad = false;

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
            if(firstClickSpace == false)
            {
                roadPrefab = currentPlatform.InstantiateRoad();
                rotateRoad = currentPlatform.GetRoadPoint();
                firstClickSpace = true;
            }            

            if(roadPrefab != null)
            {
                Resize(roadPrefab.transform, 1, new Vector3(0f, 0.01f, 0f));
            }
        }
        else if(firstClickSpace == true) {
            Debug.Log("Soltou a barra de espaço");
            hasReleasedSpacebar = true;

            var rotationAngle = Time.time * (90/10);
            if(rotationAngle <= 90)
            {                
                Debug.Log("Rotacionando: " + rotationAngle + "graus");
                rotateRoad.transform.rotation = Quaternion.Euler(rotationAngle, 0f, 0f);
            }
            else if(moveToEndOfRoad == false){
                //move to the end of the road
                transform.position = Vector3.MoveTowards(transform.position, roadPrefab.GetEndOfRoad().transform.position, Time.deltaTime * 5);
                if(transform.position.Equals(roadPrefab.GetEndOfRoad().transform.position))
                {
                    moveToEndOfRoad = true;
                }
            }                
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Platform")
        {
            Debug.Log("Estamos na plataforma");
            currentPlatform = other.gameObject.transform.GetComponent<PlatformBehaviour>();            
        }

        if(other.gameObject.tag == "target")
        {
            
        }
    }

    private void Resize(Transform currentPlatform, float amount, Vector3 direction)
    {        
        currentPlatform.position += direction * amount / 2;
        
        currentPlatform.localScale += direction * amount;
    }
}
