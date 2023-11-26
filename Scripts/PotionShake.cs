using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShake : MonoBehaviour
{
    public float magnitude;
    private Vector3 pos;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pos + new Vector3(0.05f * magnitude * Mathf.Sin(offset), 0f, 0f);
        offset += 50f * magnitude * Time.deltaTime;
    }
}
