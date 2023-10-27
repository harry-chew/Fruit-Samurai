using UnityEngine;

public class FruitCollector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Fruit fruit))
        {
            GameEvents.FireFruitDropEvent(fruit);
            Destroy(collision.gameObject);
        }
    }
}