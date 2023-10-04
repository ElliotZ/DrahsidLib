using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Memory;
using FFXIVClientStructs.FFXIV.Common.Math;
using System;

namespace DrahsidLib;

/// <summary>
/// Publicized and extended version of Dalamud.Game.ClientState.Objects.Types.GameObject
/// </summary>
public unsafe partial class GameObjectWrapper {
    /// <summary>
    /// Address of the GameObject
    /// </summary>
    public IntPtr Address = IntPtr.Zero;

    /// <summary>
    /// Internal Dalamud Game Object
    /// </summary>
    public GameObject? _DObject;

    /// <summary>
    /// Gets the correlating Dalamud Game Object
    /// </summary>
    public GameObject? DObject {
        get {
            if (_DObject == null) {
                _DObject = Service.ObjectTable.SearchById(ObjectId);
            }

            return _DObject;
        }
        set {
            _DObject = value;
        }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="address">Address of the GameObject</param>
    public GameObjectWrapper(IntPtr address) {
        Address = address;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">Id of the GameObject</param>
    public GameObjectWrapper(ulong id) {
        var obj = Service.ObjectTable.SearchById(id);

        if (obj == null) {
            Service.Logger.Error("GameObjectWrapper was provided a bogus id!");
            return;
        }

        _DObject = obj;
        Address = obj.Address;
    }

    public GameObjectWrapper(GameObject obj) {
        if (obj == null) {
            Service.Logger.Error("GameObjectWrapper was provided a bogus GameObject!");
            return;
        }

        _DObject = obj;
        Address = obj.Address;
    }

    // I can't construct a new Dalamud.Game.ClientState.Objects.Types.GameObject, so we need to SearchById
    /// <summary>
    /// GameObjectWrapper --> Dalamud.Game.ClientState.Objects.Types.GameObject
    /// </summary>
    /// <param name="obj"></param>
    public static explicit operator GameObject?(GameObjectWrapper obj) {
        return Service.ObjectTable.SearchById(obj.ObjectId);
        /*Dalamud.Game.ClientState.Objects.Types.GameObject newObj;
        newObj.Address = obj.Address;
        return newObj;*/
        // return Dalamud.Game.ClientState.Objects.Types.GameObject(obj.Address);
    }

    /// <summary>
    ///  Dalamud.Game.ClientState.Objects.Types.GameObject --> GameObjectWrapper
    /// </summary>
    /// <param name="obj"></param>
    public static explicit operator GameObjectWrapper(GameObject obj) {
        return new GameObjectWrapper(obj.Address);
    }

    /// <summary>
    /// FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject --> GameObjectWrapper
    /// </summary>
    /// <param name="obj"></param>
    public static explicit operator GameObjectWrapper(FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject obj) {
        return new GameObjectWrapper(Service.ObjectTable.SearchById(obj.ObjectID).Address);
    }

    public FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject* Struct => (FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject*)Address;

    public SeString Name => MemoryHelper.ReadSeString((IntPtr)Struct->Name, 64);

    public uint ObjectId => Struct->ObjectID;

    public uint DataId => Struct->DataID;

    public uint OwnerId => Struct->OwnerID;

    public ushort ObjectIndex => Struct->ObjectIndex;

    public ObjectKind ObjectKind => (ObjectKind)Struct->ObjectKind;

    public byte SubKind => Struct->SubKind;

    public byte YalmDistanceX => Struct->YalmDistanceFromPlayerX;

    public byte YalmDistanceZ => Struct->YalmDistanceFromPlayerZ;

    public bool IsDead => Struct->IsDead();

    public bool IsTargetable => Struct->GetIsTargetable();

    public Vector3 Position => new(Struct->Position.X, Struct->Position.Y, Struct->Position.Z);

    public float Rotation => Struct->Rotation;

    public float HitboxRadius => Struct->HitboxRadius;

    public ulong TargetObjectId => DObject.TargetObjectId;

    public GameObjectWrapper? TargetObject => (GameObjectWrapper?)DObject.TargetObject;
 
    public override string ToString() => DObject.ToString();
}

public unsafe partial class GameObjectWrapper {
    public float CursorHeight => Marshal.PtrToStructure<float>(Address + 0x124);

    public Vector3 GetHeadPosition() {
        Vector3 pos = Position;
        pos.Y += CursorHeight - 0.2f;
        return pos;
    }

    public bool IsValid() => DObject.IsValid();

    public bool IsBattleChara => DObject is BattleChara;

    public bool IsBattleNPC => DObject.ObjectKind == ObjectKind.BattleNpc;

    public bool IsPlayerCharacter => DObject.ObjectKind == ObjectKind.Player;

    public BattleChara BattleChara => DObject as BattleChara;

    public PlayerCharacter PlayerCharacter => DObject as PlayerCharacter;

    public unsafe bool TargetIsTargetable() {
        if (TargetObject == null) {
            return false;
        }

        FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject* targetobj = (FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject*)TargetObject.Address;
        return targetobj->GetIsTargetable();
    }
}
