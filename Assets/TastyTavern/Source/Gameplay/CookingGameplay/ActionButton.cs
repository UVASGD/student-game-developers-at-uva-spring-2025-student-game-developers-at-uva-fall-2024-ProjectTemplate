using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ActionButton : DataButton {

    public ActionButton(ActionData data) : base(data)
    {
        Icon = new(){ image = ((ActionData)Data).Sprite.texture };
        Label.text = ((ActionData)Data).Name;

        AddStyles();
        AttachIcon();
        AttachLabel();
    }
    
    protected override void AddStyles(){
        Icon.AddToClassList("action-icon");
        Label.AddToClassList("action-label");
    }

}
