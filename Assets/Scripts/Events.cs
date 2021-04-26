using UnityEngine.Events;

[System.Serializable]
public class ShopEvent : UnityEvent<Items, uint, string> { }

[System.Serializable]
public class HumanoidAudioEvent : UnityEvent<HumanoidAudioType> { }


[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }

[System.Serializable]
public class ByteEvent : UnityEvent<byte> { }

[System.Serializable]
public class StringEvent : UnityEvent<string> { }