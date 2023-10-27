using UnityEngine;

public  class Fruit : MonoBehaviour, ICutable
{
    public GameObject topCut;
    public GameObject bottomCut;

    public int scoreValue;
    public Color splashColor;

    public virtual void Init(GameObject topCut, GameObject bottomCut)
    {
        this.topCut = topCut;
        this.bottomCut = bottomCut;
    }

    public void Cut()
    {
        GameEvents.FireFruitCutEvent(this);
        Destroy(gameObject);
    }
}