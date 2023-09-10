using UnityEngine;

public class TentacleController : MonoBehaviour
{
    public Transform target; // L'objet cible que la tentacule doit suivre
    public int chainLength = 5; // Nombre de segments dans la tentacule
    public Transform[] segments; // Les segments de la tentacule

    private void Start()
    {
        segments = new Transform[chainLength];
        // Ici, initialisez vos segments. Par exemple, si les segments sont des enfants du GameObject :
        for (int i = 0; i < chainLength; i++)
        {
            segments[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        // Faites en sorte que le dernier segment suive l'objet cible
        segments[chainLength - 1].position = Vector3.Lerp(segments[chainLength - 1].position, target.position, 0.1f);

        // Pour chaque segment, à partir de la fin, faites-le suivre le segment précédent
        for (int i = chainLength - 1; i > 0; i--)
        {
            segments[i - 1].position = Vector3.Lerp(segments[i - 1].position, segments[i].position, 0.1f);
        }
    }
}