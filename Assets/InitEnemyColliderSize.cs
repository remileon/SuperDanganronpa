using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitEnemyColliderSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshPro>().ForceMeshUpdate();
//        Debug.Log("tmp text: " + GetComponent<TextMeshPro>().text);
//        Debug.Log("tmp width: " + GetComponent<TextMeshPro>().GetRenderedValues().x);

        var tmp = GetComponent<TextMeshPro>();
        tmp.ForceMeshUpdate();
        var boxCollider = GetComponent<BoxCollider>();
        var oldSize = boxCollider.size;
        boxCollider.size = new Vector3(tmp.GetRenderedValues().x, oldSize.y, oldSize.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
