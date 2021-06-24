using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Transform roadPrefab;
    private Transform rotateRoad;

    private PlatformBehaviour currentPlatform;
    private bool firstClickSpace = false;
    private bool isSpacePressed = false;
    private bool hasReleasedSpacebar = false;

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
                Resize(roadPrefab, 1, new Vector3(0f, 0.01f, 0f));
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
            else{
                //move to the end of the road
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
    }

    private void Resize(Transform currentPlatform, float amount, Vector3 direction)
    {        
        currentPlatform.position += direction * amount / 2;
        
        currentPlatform.localScale += direction * amount;
    }
}
