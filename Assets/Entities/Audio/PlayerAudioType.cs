using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioType {

    const string PREFIX = "Audio/Player/";

    public static readonly PlayerAudioType WALK_AUDIO_CONCRETE = new PlayerAudioType (0, new string[] { "walk/concrete/concrete_01", "walk/concrete/concrete_02", "walk/concrete/concrete_03", "walk/concrete/concrete_04", "walk/concrete/concrete_05", "walk/concrete/concrete_06" });
    public static readonly PlayerAudioType RUN_AUDIO_CONCRETE = new PlayerAudioType(1, new string[] { "run/Run_1", "run/Run_2", "run/Run_3" });

    public static readonly PlayerAudioType NOISE_HAND_CLAP = new PlayerAudioType(2, new string[] { "run/concrete/concrete_01" });

    public static IEnumerable<PlayerAudioType> Values {
        get {
            yield return WALK_AUDIO_CONCRETE;
            yield return RUN_AUDIO_CONCRETE;
        }
    }

    public int TypeId { get; private set; }
    public string[] Sounds { get; private set; }

    PlayerAudioType(int typeId, string[] sounds) =>
        (TypeId, Sounds) = (typeId, sounds);

    public static AudioClip GetClip(string type) {
        return Resources.Load<AudioClip>(PREFIX + type);
    }
}
