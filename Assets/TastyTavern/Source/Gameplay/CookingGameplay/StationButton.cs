using System;
using UnityEngine;
using UnityEngine.UIElements;

// Inherits helper classes and returns itself on OnClick() with OnClickButton delegate
public class StationButton : DataButton {

    public StationData Data { get; set; }

    public event Action<StationButton> OnClickButton = delegate { };

    public StationButton(StationData data) 
    {
        Data = data;
        // Label.text = Data.Name;

        AddStyles();
        AttachLabel();
    }

    protected override void OnClick(){
        OnClickButton.Invoke(this);
    }
    
    protected override void AddStyles(){
        this.AddToClassList("slot");
    }

}
