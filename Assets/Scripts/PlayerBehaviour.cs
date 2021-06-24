using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Transform roadPrefab;
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
        if (isSpacePressed && hasReleasedSpacebar == false)
        {
            if(firstClickSpace == false)
            {
                roadPrefab = currentPlatform.InstantiateRoad();
                firstClickSpace = true;
            }            

            if(roadPrefab != null)
            {
                Resize(roadPrefab, 1, new Vector3(0f, 0.01f, 0f));
            }
        }
        else{
            hasReleasedSpacebar = true;
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
