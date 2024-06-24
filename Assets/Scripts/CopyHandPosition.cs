using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyHandPosition : MonoBehaviour
{
    public Transform HandTransform;
    public Transform FollowerObject;

    public bool FreezeFollowerRotation = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FollowerObject == null || HandTransform == null) return;

        if(!FreezeFollowerRotation) 
            FollowerObject.SetPositionAndRotation(HandTransform.position, HandTransform.rotation);
        else
            FollowerObject.position = HandTransform.position;
    }
}
