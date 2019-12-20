using Assets.Script;
using Assets.Script.Interfaces;
using Assets.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanInteract : MonoBehaviour
{
    public int DistnaceOfInteract = 2;
    private GameObject ActiveObjToInteract;

    HeroStuff HeroStuff;

    // Start is called before the first frame update
    void Start()
    {
        HeroStuff = GetComponent<HeroStuff>();
    }

    // Update is called once per frame
    void Update()
    {
        AutoUpdateActiveObjToInteract();

        if (Input.GetKey(KeyCode.Space)
            && ActiveObjToInteract != null)
        {
            GetICanBeInteracted().Interact(HeroStuff);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        var script = InterfaceUtility.GetMonoBehaviourImplementTheInterface<ICanBeInteracted>(collision.gameObject);
        if (script != null)
        {
            var canBeInteracted = script as ICanBeInteracted;
            if (canBeInteracted.IsAutoInteracted)
            {
                canBeInteracted.Interact(HeroStuff);
            }
        }
    }

    private void AutoUpdateActiveObjToInteract()
    {
        var all = FindObjectsOfType<GameObject>();
        var canBeInteracted = InterfaceUtility.FindObjectsImplementOfInterface<ICanBeInteracted>(all);
        var closest = canBeInteracted
            .Where(x => Vector3.Distance(transform.position, x.transform.position) < DistnaceOfInteract)
            .OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();

        if (ActiveObjToInteract != closest)
        {
            GetICanBeInteracted()?.DeselectAsActive();
            ActiveObjToInteract = closest;
            GetICanBeInteracted()?.SelectAsActive();
        }
    }

    private ICanBeInteracted GetICanBeInteracted()
    {
        return InterfaceUtility.GetMonoBehaviourImplementTheInterface<ICanBeInteracted>(ActiveObjToInteract) as ICanBeInteracted;
    }
}
