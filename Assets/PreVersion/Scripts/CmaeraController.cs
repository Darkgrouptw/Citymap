using UnityEngine;
using System.Collections;

public class CmaeraController : MonoBehaviour
{
    GameObject m_camera;
    public float scale = 8;
    public float scale2 = 3;
    public float scale3 = 8;
    // Use this for initialization
    void Start()
    {
        m_camera = GameObject.FindGameObjectWithTag("MainCamera");

        scale = -0.33f;
        scale2 = 2.4f;
    }

    void Update()
    {
        m_camera.transform.position = (this.transform.position - this.transform.forward * scale + Vector3.up * scale2);
        m_camera.transform.LookAt(this.transform.position + this.transform.forward * scale3);
    }
}
