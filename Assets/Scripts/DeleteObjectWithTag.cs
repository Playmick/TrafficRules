using UnityEngine;

[RequireComponent(typeof(Collider))] // требуем наличия компонента триггер на объекте
public class DeleteObjectWithTag : MonoBehaviour
{
    public string objectTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectTag))
        {
            Destroy(other.gameObject);
        }
    }
}
