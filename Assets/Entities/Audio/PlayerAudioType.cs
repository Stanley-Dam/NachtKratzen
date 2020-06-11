using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioType {

    const string PREFIX = "Audio/";

    public static readonly PlayerAudioType DEAD = new PlayerAudioType(0, new string[] { "Dead/Dead_1", "Dead/Dead_2", "Dead/Dead_3", "Dead/Dead_4", "Dead/Dead_5" });
    public static readonly PlayerAudioType END = new PlayerAudioType(1, new string[] { "End/End" });
    public static readonly PlayerAudioType HIDER_SEES_SEEKER = new PlayerAudioType(2, new string[] { "Hider_sees_seeker/Hider_sees_seeker_1", "Hider_sees_seeker/Hider_sees_seeker_2", "Hider_sees_seeker/Hider_sees_seeker_3", "Hider_sees_seeker/Hider_sees_seeker_4" });
    public static readonly PlayerAudioType HIDER_JUMP = new PlayerAudioType(3, new string[] { "Jump/Jump_1" });
    public static readonly PlayerAudioType HIDER_RUN = new PlayerAudioType(4, new string[] { "Run/Run_1", "Run/Run_2", "Run/Run_3", "Run/Run_4", "Run/Run_5", "Run/Run_6" });
    public static readonly PlayerAudioType SEEKER_JUMP = new PlayerAudioType(5, new string[] { "Seeker_jump/Seeker_jump_1", "Seeker_jump/Seeker_jump_2", "Seeker_jump/Seeker_jump_3", "Seeker_jump/Seeker_jump_4" });
    public static readonly PlayerAudioType SEEKER_RELEASED = new PlayerAudioType(6, new string[] { "Seeker_released/Seeker_released" });
    public static readonly PlayerAudioType SEEKER_RUN = new PlayerAudioType(7, new string[] { "Seeker_run/Seeker_run_1", "Seeker_run/Seeker_run_2", "Seeker_run/Seeker_run_3", "Seeker_run/Seeker_run_4", "Seeker_run/Seeker_run_5", "Seeker_run/Seeker_run_6" });
    public static readonly PlayerAudioType SEEKER_SEES_YOU = new PlayerAudioType(8, new string[] { "Seeker_sees_you/Seeker_sees_you_1", "Seeker_sees_you/Seeker_sees_you_2", "Seeker_sees_you/Seeker_sees_you_3", "Seeker_sees_you/Seeker_sees_you_4", "Seeker_sees_you/Seeker_sees_you_5" });
    public static readonly PlayerAudioType SEEKER_WALK = new PlayerAudioType(9, new string[] { "Seeker_walk/Seeker_walk_1", "Seeker_walk/Seeker_walk_2", "Seeker_walk/Seeker_walk_3", "Seeker_walk/Seeker_walk_4", "Seeker_walk/Seeker_walk_5", "Seeker_walk/Seeker_walk_6" });
    public static readonly PlayerAudioType START = new PlayerAudioType(10, new string[] { "Start/Start" });
    public static readonly PlayerAudioType HIDER_WALK = new PlayerAudioType(11, new string[] { "Walk/Walk_1", "Walk/Walk_2", "Walk/Walk_3", "Walk/Walk_4", "Walk/Walk_5", "Walk/Walk_6" });

    public static IEnumerable<PlayerAudioType> Values {
        get {
            yield return DEAD;
        }
    }

    public int TypeId { get; private set; }
    public string[] Sounds { get; private set; }

    PlayerAudioType(int typeId, string[] sounds) =>
        (TypeId, Sounds) = (typeId, sounds);

    public static PlayerAudioType GetWalkByPlayerType(PlayerTypes playerType) {
        if (playerType == PlayerTypes.HIDER)
            return HIDER_WALK;

        return SEEKER_WALK;
    }

    public static PlayerAudioType GetRunByPlayerType(PlayerTypes playerType) {
        if (playerType == PlayerTypes.HIDER)
            return HIDER_RUN;

        return SEEKER_RUN;
    }

    public static PlayerAudioType GetJumpByPlayerType(PlayerTypes playerType) {
        if (playerType == PlayerTypes.HIDER)
            return HIDER_JUMP;

        return SEEKER_JUMP;
    }

    public static AudioClip GetClip(string type) {
        return Resources.Load<AudioClip>(PREFIX + type);
    }

    public static PlayerAudioType GetByMessageType(MessageTypes messageType) {

        switch(messageType) {
            case MessageTypes.HAPPY_NEWS:
                return START;
            case MessageTypes.SAD_NEWS:
                return DEAD;
            case MessageTypes.SPOOKY_NEWS:
                return HIDER_SEES_SEEKER;
            case MessageTypes.VICTORIOUS_NEWS:
                return END;
        }
        
        return null;
    }
}
