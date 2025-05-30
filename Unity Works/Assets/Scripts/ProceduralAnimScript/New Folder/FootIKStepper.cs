using UnityEngine;

public class FootIKStepper : MonoBehaviour
{
    public Transform footTarget;         
    public Transform footHome;          
    public float stepDistance = 0.5f;    
    public float stepHeight = 0.2f;      
    public float moveSpeed = 5f;         

    private Vector3 oldPos;
    private Vector3 newPos;
    private float lerp;
    private bool isStepping = false;

    void Start()
    {
        oldPos = footTarget.position;
        newPos = oldPos;
    }

    void Update()
    {
        // Adým atma için zorla karakter yürüsün gibi simüle et
        footHome.position += Vector3.forward * Time.deltaTime * 1.0f;

        float distanceFromHome = Vector3.Distance(footTarget.position, footHome.position);

        if (!isStepping && distanceFromHome > stepDistance)
        {
            newPos = footHome.position;
            oldPos = footTarget.position;
            lerp = 0;
            isStepping = true;
        }

        if (isStepping)
        {
            lerp += Time.deltaTime * moveSpeed;
            Vector3 pos = Vector3.Lerp(oldPos, newPos, lerp);
            pos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
            footTarget.position = pos;

            if (lerp >= 1)
                isStepping = false;
        }
    }

}
