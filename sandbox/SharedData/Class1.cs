﻿using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData
{
    public enum ByteEnum : byte { A, B, C, D, E }
    public enum SByteEnum : sbyte { A, B, C, D, E }
    public enum ShortEnum : short { A, B, C, D, E }
    public enum UShortEnum : ushort { A, B, C, D, E }
    public enum IntEnum : int { A, B, C, D, E }
    public enum UIntEnum : uint { A, B, C, D, E }
    public enum LongEnum : long { A, B, C, D, E }
    public enum ULongEnum : ulong { A, B, C, D, E }

    [MessagePackObject]
    public class FirstSimpleData
    {
        [Key(0)]
        public int Prop1 { get; set; }
        [Key(1)]
        public string Prop2 { get; set; }
        [Key(2)]
        public int Prop3 { get; set; }
    }

    [MessagePackObject]
    public class SimpleIntKeyData
    {
        [Key(0)]
        public int Prop1 { get; set; }
        [Key(1)]
        public ByteEnum Prop2 { get; set; }
        [Key(2)]
        public string Prop3 { get; set; }
        [Key(3)]
        public SimlpeStringKeyData Prop4 { get; set; }
        [Key(4)]
        public SimpleStructIntKeyData Prop5 { get; set; }
        [Key(5)]
        public SimpleStructStringKeyData Prop6 { get; set; }
        [Key(6)]
        public byte[] BytesSpecial { get; set; }
    }

    [MessagePackObject(true)]
    public class SimlpeStringKeyData
    {
        public int Prop1 { get; set; }
        public ByteEnum Prop2 { get; set; }
        public int Prop3 { get; set; }
    }

    [MessagePackObject]
    public struct SimpleStructIntKeyData
    {
        [Key(0)]
        public int X { get; set; }
        [Key(1)]
        public int Y { get; set; }
        [Key(2)]
        public byte[] BytesSpecial { get; set; }
    }


    [MessagePackObject]
    public struct SimpleStructStringKeyData
    {
        [Key("key-X")]
        public int X { get; set; }
        [Key("key-Y")]
        public int[] Y { get; set; }
    }

    [MessagePackObject]
    public struct Vector2
    {
        [Key(0)]
        public readonly float X;
        [Key(1)]
        public readonly float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    [MessagePackObject]
    public class EmptyClass
    {

    }

    [MessagePackObject]
    public struct EmptyStruct
    {

    }



    [MessagePackObject]
    public class Version1
    {
        [Key(3)]
        public int MyProperty1 { get; set; }
        [Key(4)]
        public int MyProperty2 { get; set; }
        [Key(5)]
        public int MyProperty3 { get; set; }
    }


    [MessagePackObject]
    public class Version2
    {
        [Key(3)]
        public int MyProperty1 { get; set; }
        [Key(4)]
        public int MyProperty2 { get; set; }
        [Key(5)]
        public int MyProperty3 { get; set; }
        // [Key(6)]
        // public int MyProperty4 { get; set; }
        [Key(7)]
        public int MyProperty5 { get; set; }
    }


    [MessagePackObject]
    public class Version0
    {
        [Key(3)]
        public int MyProperty1 { get; set; }
    }

    [MessagePackObject]
    public class HolderV1
    {
        [Key(0)]
        public Version1 MyProperty1 { get; set; }
        [Key(1)]
        public int After { get; set; }
    }

    [MessagePackObject]
    public class HolderV2
    {
        [Key(0)]
        public Version2 MyProperty1 { get; set; }
        [Key(1)]
        public int After { get; set; }
    }

    [MessagePackObject]
    public class HolderV0
    {
        [Key(0)]
        public Version0 MyProperty1 { get; set; }
        [Key(1)]
        public int After { get; set; }
    }

    [MessagePackObject]
    public class Callback1 : IMessagePackSerializationCallbackReceiver
    {
        [Key(0)]
        public int X { get; set; }

        public bool CalledBefore { get; private set; }
        public bool CalledAfter { get; private set; }

        public Callback1(int x)
        {

        }

        public void OnBeforeSerialize()
        {
            CalledBefore = true;
        }

        public void OnAfterDeserialize()
        {
            CalledAfter = true;
        }
    }

    [MessagePackObject]
    public class Callback1_2 : IMessagePackSerializationCallbackReceiver
    {
        [Key(0)]
        public int X { get; set; }

        public bool CalledBefore { get; private set; }
        public bool CalledAfter { get; private set; }
        public Callback1_2(int x)
        {
            this.X = x;
        }

        void IMessagePackSerializationCallbackReceiver.OnBeforeSerialize()
        {
            CalledBefore = true;
        }

        void IMessagePackSerializationCallbackReceiver.OnAfterDeserialize()
        {
            CalledAfter = true;
        }
    }

    [MessagePackObject(true)]
    public struct Callback2 : IMessagePackSerializationCallbackReceiver
    {
        [Key(0)]
        public int X { get; set; }

        Action onBefore;
        Action onAfter;

        public static bool CalledAfter = false;

        public Callback2(int x)
            : this(x, () => { }, () => { })
        {

        }


        public Callback2(int x, Action onBefore, Action onAfter)
        {
            this.X = x;
            this.onBefore = onBefore;
            this.onAfter = onAfter;
        }

        public void OnBeforeSerialize()
        {
            onBefore();
        }

        public void OnAfterDeserialize()
        {
            CalledAfter = true;
        }
    }

    [MessagePackObject(true)]
    public struct Callback2_2 : IMessagePackSerializationCallbackReceiver
    {
        [Key(0)]
        public int X { get; set; }

        public static bool CalledAfter = false;


        public Callback2_2(int x)
            : this(x, () => { }, () => { })
        {

        }

        Action onBefore;
        Action onAfter;
        public Callback2_2(int x, Action onBefore, Action onAfter)
        {
            this.X = x;
            this.onBefore = onBefore;
            this.onAfter = onAfter;
        }

        void IMessagePackSerializationCallbackReceiver.OnBeforeSerialize()
        {
            onBefore();
        }

        void IMessagePackSerializationCallbackReceiver.OnAfterDeserialize()
        {
            CalledAfter = true;
        }
    }
}