using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCardView : CardView
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    private void OnMouseDown()
    {
        DragCard();
    }

    private void DragCard()
    {
        throw new NotImplementedException();
    }

    private void OnMouseUp()
    {
        CheckCardTarget();
    }

    private void CheckCardTarget()
    {
        throw new NotImplementedException();
    }
}
