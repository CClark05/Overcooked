using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class StoveTopParticles : CounterParticles
{
    private StoveTopInteract stoveTopInteract;
    [SerializeField] private ParticleSystem fireParticles;

    private new void Awake()
    {
        base.Awake();
        stoveTopInteract = GetComponent<StoveTopInteract>();
    }

    private new void Start()
    {
        base.Start();
        stoveTopInteract.OnStateChanged += StoveTopInteractOnOnStateChanged;
    }

    private void StoveTopInteractOnOnStateChanged(object sender, StoveTopInteract.OnStateChangedEventArgs e)
    {
        if (e.state is StoveTopInteract.States.Cooking or StoveTopInteract.States.Cooked)
        {
            if (fireParticles.isPlaying) return;
            fireParticles.Play();
            return;
        }
        fireParticles.Stop();
    }
}
