using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveDmgTest : MonoBehaviour
{
    private Transform _model;

    // Start is called before the first frame update
    void Start()
    {
        _model = GetComponentsInChildren<Transform>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void Wobble()
    {
        _model.DOKill();
        _model.localScale = Vector3.one;
        _model.DOShakeScale(1,1,10,90).SetEase(Ease.OutElastic);
    }

    public void TakeDamage()
    {
        Wobble();
    }
}
