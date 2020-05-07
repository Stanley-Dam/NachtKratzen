using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Has to be used by conferting floats, might get some more features in the future ;)
/// </summary>
public abstract class PacketUtils {

    private const int packetFloatToIntMultiplier = 1000;

    public static string ToPacketString(float number) {
        number *= packetFloatToIntMultiplier;
        int integerToSend = Mathf.FloorToInt(number);
        return integerToSend.ToString();
    }

    public static float FromPacketString(string packetNumber) {
        int integerFromPacket = int.Parse(packetNumber);
        float number = integerFromPacket / packetFloatToIntMultiplier;
        return number;
    }

}