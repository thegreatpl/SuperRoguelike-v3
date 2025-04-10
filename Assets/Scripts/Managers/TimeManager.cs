using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public List<ITickable> TimeObjects = new List<ITickable> ();

    public bool IsPaused = false;

    public int MaxUpdated = 20;


    public float TimeBetweenTicks = 2.5f; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTime()
    {
        StartCoroutine(Run()); 
    }

    public void ResetTime()
    {
        StopAllCoroutines();
        TimeObjects.Clear();
    }


    IEnumerator Run()
    {
        while (true)
        {
            while (IsPaused)
            {
                yield return null;
            }

            //get the current objects to update. 
            var currentTickUpdate = TimeObjects.Where(x => (x != null || !x.Equals(null)) && x.Cooldown <= 0);
            int currentUpdated = 0; 
            foreach (var t in currentTickUpdate)
            {

                if (t == null || t.Equals(null))
                    continue;

                t?.BeginTick();

                while (t?.WaitingForPlayerInput == true) //nullable value, so only if explicitely true. 
                    yield return null;

                try
                {

                    t?.RunTick();
                    
                }
                catch (Exception e)
                {
                    Debug.LogError($"Exception caught by TimeManager: {e.Message}"); 
                }
                currentUpdated++;

                if (currentUpdated >= MaxUpdated)
                {
                    yield return null ;
                    currentUpdated = 0;
                }
            }


            TimeObjects.ForEach(x => x.EndTick());
            yield return new WaitForSeconds(TimeBetweenTicks); 
        }
    }

}
