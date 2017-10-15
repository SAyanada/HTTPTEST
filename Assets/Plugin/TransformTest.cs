using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest
    : MonoBehaviour
{
    [SerializeField]Network n;
    [SerializeField] bool isNetUp;
    [SerializeField] string ID;

    void Start()
    {
        StartCoroutine(Five_seconds());

    }
    IEnumerator Five_seconds()
    {
        while (true)
        {
            StartCoroutine(n.NetUp(isNetUp,transform));
            yield return new WaitForSeconds(n.SyncTime);
        }
    }



    private void Update()
    {
        transform.position = new Vector3(
    transform.position.x + Input.GetAxis("Horizontal") *Time.deltaTime *10,
    0,
    transform.position.z + Input.GetAxis("Vertical") * Time.deltaTime *10
);
    }

}