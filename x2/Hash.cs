﻿// Copyright (c) 2013 Jae-jun Kang
// See the file COPYING for license details.

using System;

namespace x2
{
    /// <summary>
    /// Hash Code generation unitily struct.
    /// </summary>
    public struct Hash
    {
        public const int Seed = 17;

        public int Code;

        public Hash(int seed)
        {
            Code = seed;
        }

        public static int Update(int seed, bool value)
        {
            return ((seed << 5) + seed) ^ (value ? 2 : 1);
        }

        public static int Update(int seed, int value)
        {
            return ((seed << 5) + seed) ^ value;
        }

        public static int Update(int seed, uint value)
        {
            return ((seed << 5) + seed) ^ (int)value;
        }

        public static int Update(int seed, long value)
        {
            return ((seed << 5) + seed) ^ (int)(value ^ (value >> 32));
        }

        public static int Update(int seed, ulong value)
        {
            return ((seed << 5) + seed) ^ (int)(value ^ (value >> 32));
        }

        public static int Update(int seed, float value)
        {
            return Update(seed, (double)value);
        }

        public static int Update(int seed, double value)
        {
            long bits = System.BitConverter.DoubleToInt64Bits(value);
            return ((seed << 5) + seed) ^ (int)((ulong)bits >> 20);
        }

        public static int Update(int seed, string value)
        {
            return ((seed << 5) + seed) ^ (value != null ? value.GetHashCode() : 0);
        }

        public void Update(bool value)
        {
            Code = Update(Code, value);
        }

        public void Update(int value)
        {
            Code = Update(Code, value);
        }

        public void Update(uint value)
        {
            Code = Update(Code, value);
        }

        public void Update(long value)
        {
            Code = Update(Code, value);
        }

        public void Update(ulong value)
        {
            Code = Update(Code, value);
        }

        public void Update(float value)
        {
            Code = Update(Code, value);
        }

        public void Update(double value)
        {
            Code = Update(Code, value);
        }

        public void Update(string value)
        {
            Code = Update(Code, value);
        }

        public void Update<T>(T obj)
        {
            Code = Update(Code, obj.GetHashCode());
        }
    }
}
