using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IndicatorSystem : MonoBehaviour {

    [SerializeField] private HiderIndicator indicatorPrefab;
    [SerializeField] private RectTransform holder;
    [SerializeField] private new Camera camera;
    private Transform player;

    private Dictionary<Hider, HiderIndicator> indicators = new Dictionary<Hider, HiderIndicator>();

    public static Action<Hider, Transform> CreateIndicator = delegate { };
    public static Action<Hider> DestroyIndicator = delegate { };

    private void OnEnable() {
        CreateIndicator += Create;
        DestroyIndicator += Destroy;
    }

    private void OnDisable() {
        CreateIndicator -= Create;
        DestroyIndicator -= Destroy;
    }

    private void Create(Hider hider, Transform player) {
        if(this.player == null)
            this.player = player;

        if(indicators.ContainsKey(hider)) {
            indicators[hider].Restart();
            return;
        }

        HiderIndicator newIndicator = Instantiate(indicatorPrefab, holder);
        newIndicator.Register(hider, this.player, new Action(() => { indicators.Remove(hider); }));

        indicators.Add(hider, newIndicator);
    }
}
