using UnityEngine;
public class QuantumDisappear : QuantumGroupObject
{
    void Update(){
        if (isVisible()){
            setInvisible();
        } else {
            setVisible();
        }
    }

    public override void onLookAway(){
        
    }
}