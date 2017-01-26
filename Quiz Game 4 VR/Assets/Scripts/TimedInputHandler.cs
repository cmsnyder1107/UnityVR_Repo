using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; //need since we'll be firing events

public interface TimedInputHandler : IEventSystemHandler
{
    void HandleTimedInput(); //this will be implemented in classes and objects that inherit from this interface
}
