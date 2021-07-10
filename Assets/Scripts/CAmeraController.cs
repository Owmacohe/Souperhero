using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmeraController : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 minPosition, maxPosition;
   
    
    private void Update()
    {
        if(transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -90);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);


            transform.position = Vector3.Lerp(transform.position,targetPosition,smoothing);
        }
        
    }
}
