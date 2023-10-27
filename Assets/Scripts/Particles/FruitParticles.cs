using System;
using UnityEngine;

public class FruitParticles : MonoBehaviour
{
    public GameObject fruitCutParticlePrefab;
    public Color explosionColor;
    void Start()
    {
        GameEvents.FruitEvent += OnFruitEvent;
        GameEvents.BombEvent += OnBombEvent;
    }

    private void OnDestroy()
    {
        GameEvents.FruitEvent -= OnFruitEvent;
        GameEvents.BombEvent -= OnBombEvent;
    }

    private void OnBombEvent(object sender, BombEventArgs e)
    {
        if (e.EventType == BombEventType.Cut)
        {
            GameObject cut = Instantiate(fruitCutParticlePrefab, e.Bomb.transform.position, Quaternion.identity, transform);
            cut.GetComponent<ParticleSystem>().startColor = explosionColor;
            Destroy(cut, 1.0f);
        }
    }

    private void OnFruitEvent(object sender, FruitEventArgs e)
    {
        if (e.EventType == FruitEventType.Cut)
        {
            GameObject cut = Instantiate(fruitCutParticlePrefab, e.Fruit.transform.position, Quaternion.identity, transform);
            cut.GetComponent<ParticleSystem>().startColor = e.Fruit.splashColor;
            Destroy(cut, 1.0f);
        }
    }
}
