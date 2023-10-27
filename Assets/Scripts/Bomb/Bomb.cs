using UnityEngine;

public class Bomb : MonoBehaviour, ICutable
{
    public void Cut()
    {
        GameEvents.FireBombCutEvent(this);
        Destroy(gameObject);
    }
}
