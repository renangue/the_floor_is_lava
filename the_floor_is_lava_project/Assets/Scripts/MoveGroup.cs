using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGroup : MonoBehaviour
{
    
    [Header("Velocidade e Objetos")]
    [SerializeField] private Vector3 speed;
    [SerializeField] Transform[] objectTransform;
    [SerializeField] private float multiSpeed;
    

    [Header("Random")]
    [SerializeField] private bool randomizeOnRefresh;
  
    [SerializeField]private float numberPorcent;

    private void Start() {
        StartCoroutine(SlowUptade());
    }
    void Update()
    {
        foreach (Transform obj in objectTransform)
        {
            obj.Translate(speed * Time.deltaTime);

            if (obj.transform.position.x <= -13)
            {
                obj.transform.Translate(new Vector3( 64, 0, 0));

                if(randomizeOnRefresh)
                {
                    float randomNumber = Random.Range(0, 11);
                    if(randomNumber >= numberPorcent)
                    {
                        obj.gameObject.SetActive(false);
                    }
                    else
                    obj.gameObject.SetActive(true);                  
                }
            }     
            
        }

        if(speed.x > 5)
        {
            speed.x = 5;
            StopCoroutine(SlowUptade());
        }
             
       
    }

    IEnumerator SlowUptade()
    {
        while(true)
        {
            speed = speed * multiSpeed;
            Debug.Log(speed);
            yield return new WaitForSeconds(10);
            
        }

        
            
    }
    
}
