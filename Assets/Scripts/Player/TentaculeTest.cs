using System.Collections.Generic;
using UnityEngine;

public class TentaculeTest : MonoBehaviour
{
    public List<Transform> segmentsTransform; //Liste de tous les points du tentacule

    public Transform origin; //Origine du tentacule
    public Transform target; //Cible de la tentacule
    public float targetDist; //Distance entre les points du tentacule
    
    [Range(0.1f, 5f)]
    public float smoothSpeed; //Vitesse de deplacement des points 
    [Range(0.1f, 5f)]
    public float smoothRotationSpeed; //Vitesse de rotation des points
    
    public float rotaClamp; //Angle en degré maxi de rotation des points

    private Vector3[] lineSegment; //Array des points pour le line renderer
    private Vector3[] segmentV; //Array de ref
    private LineRenderer lineRend; //Line Renderer

    private void Start()
    {
        Init();
        
        //Recuperation des composants
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = segmentsTransform.Count;
        
        //Init des array 
        segmentV = new Vector3[segmentsTransform.Count];
        lineSegment = new Vector3[segmentsTransform.Count];
    }

    void Init()
    {
        //Ajout de tout les objets enfant dans la liste des points
        for(int i = 0; i < transform.childCount; i++)
        {
            segmentsTransform.Add(transform.GetChild(i));
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Default();
    }

    private void Default()
    {
        for(int i = segmentsTransform.Count - 1; i >=0 ; i--)
        {
            lineSegment[i] = segmentsTransform[i].position;
            float intensity = (float)(i + 1) / segmentsTransform.Count;
            if (i == 0)
            {
                //Rotation du segment d'origine en fonction de la cible 
                segmentsTransform[i].position = transform.position;
                segmentsTransform[i].rotation = Quaternion.Slerp(segmentsTransform[i].rotation,
                    origin.rotation * transform.rotation, smoothRotationSpeed);
            }
            else
            {
                Vector3 dir = target.position - segmentsTransform[i].position; //Calcul de la direction de rotation voulue
                float angle = Vector3.SignedAngle(-segmentsTransform[i].right, dir, Vector3.forward); //Angle 
                angle = Mathf.Clamp(angle, -rotaClamp, rotaClamp); //Clamp de l'angle
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle) * segmentsTransform[i-1].rotation;

                segmentsTransform[i].position = Vector3.SmoothDamp(segmentsTransform[i].position, 
                    segmentsTransform[i - 1].position + segmentsTransform[i - 1].right * targetDist,
                    ref segmentV[i], smoothSpeed * ( 1 - intensity)); //Deplacment des points 

                if (Quaternion.Angle(segmentsTransform[i].rotation,segmentsTransform[i-1].rotation) > rotaClamp) 
                {
                    segmentsTransform[i].rotation = segmentsTransform[i-1].rotation; //Si la diff de rotation entre deux segment est supp a rotaclamp, reset la rotation
                }
                else
                {
                    //Rotation 
                    segmentsTransform[i].rotation = Quaternion.Slerp(segmentsTransform[i].rotation,targetRotation, smoothRotationSpeed * intensity * Time.deltaTime);
                }
            }
        }
        lineRend.SetPositions(lineSegment); //Assignation des position du line renderer
    }
}
