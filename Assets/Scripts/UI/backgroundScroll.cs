using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroll : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    Vector3 startPos;
    public Vector3 offset;
    Vector3 offsetNow;
    Vector3 correction;
    int backgroundNo;
    void Awake()
    {

        if (backgroundNo == 0) 
        { 
            backgroundNo = Mathf.RoundToInt(transform.position.z + 1);
            startPos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        offsetNow = transform.position - startPos;
        if (Mathf.Abs(offsetNow.x) > offset.x) correction = new Vector3(offset.x, 0) * (direction.x / Mathf.Abs(direction.x));
        else if (Mathf.Abs(offsetNow.y) > offset.y) correction = new Vector3(0, offset.y) * (direction.y / Mathf.Abs(direction.y));
        else correction = new Vector3(0, 0);
        transform.position = transform.position-correction + direction * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 8*Mathf.Sin(Time.fixedTime + 45 * backgroundNo));
    }
}
