using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Transform roadPrefab;

    private Transform currentPlatform;
    private bool firstClickSpace;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(firstClickSpace == false)
            {
                
            }

            Debug.Log("pressionando espaço");

            if(roadPrefab != null)
            {
                Resize(roadPrefab, 1, new Vector3(0f, 0.01f, 0f));
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Platform")
        {
            currentPlatform = other.gameObject.transform;
            Debug.Log("Estmoas na plataform", currentPlatform);
        }
    }

    private void Resize(Transform currentPlatform, float amount, Vector3 direction)
    {        
        currentPlatform.position += direction * amount / 2;
        
        currentPlatform.localScale += direction * amount;
    }
}
