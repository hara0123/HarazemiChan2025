using UnityEngine;

public class MeasureHeight : MonoBehaviour
{
    void Start()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
        {
            Bounds combinedBounds = renderers[0].bounds;
            foreach (Renderer r in renderers)
            {
                combinedBounds.Encapsulate(r.bounds);
            }

            float height = combinedBounds.size.y;
            Debug.Log("モデル全体の身長 " + height + "m");
        }
        else
        {
            Debug.Log("Rendererが見つかりませんでした");
        }
    }
}
