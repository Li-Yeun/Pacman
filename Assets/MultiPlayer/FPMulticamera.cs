using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPMulticamera : MonoBehaviour
{
    [SerializeField] public string mode;
    private Transform tr_Target;
    public float turnspeed = 0.1f;
    Vector3 Offset = Vector3.zero;
    private Vector3 R_offset = new Vector3(0,-90,0);
    private Quaternion TargetAngle;
    [SerializeField] string tag;

    private void Start()
    {
        if(mode == "Pacman")
            gameObject.transform.parent = GameObject.FindGameObjectWithTag("Camera Parent").transform;
    }

    void Update ()
    {
        if (FindObjectOfType<PacmanMovement>() == null)
            return;
        else
        {
            PacmanMovement Target = FindObjectOfType<PacmanMovement>().GetComponent<PacmanMovement>();
            tr_Target = Target.gameObject.transform;
        }
        gameObject.transform.position = tr_Target.position + Offset;
        TargetAngle = tr_Target.rotation * Quaternion.Euler(0, R_offset.y, 0);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, TargetAngle, turnspeed * Time.deltaTime);
    }

    public void PacmanInstantiated()
    {
        if(mode == "Spectate")
        {
            PacmanMovement Target = GameObject.FindGameObjectWithTag(tag).GetComponent<PacmanMovement>();
            tr_Target = Target.gameObject.transform;
        }
    }
}