// Copyright (c) 2013 Jae-jun Kang
// See the file COPYING for license details.

using System;
using System.Text;

namespace x2.Events
{
    public sealed class TimeoutEvent : Event
    {
        new private static readonly Tag tag;

        new public static int TypeId { get { return tag.TypeId; } }

        private object key;

        public object Key
        {
            get { return key; }
            set
            {
                fingerprint.Touch(tag.Offset + 0);
                key = value;
            }
        }

        static TimeoutEvent()
        {
            tag = new Tag(Event.tag, typeof(TimeoutEvent), 1,
                    (int)(int)BuiltinType.TimeoutEvent);
        }

        public TimeoutEvent()
            : base(tag.NumProps)
        {
            Initialize();
        }

        public override bool EqualsTo(Cell other)
        {
            if (!base.EqualsTo(other))
            {
                return false;
            }
            TimeoutEvent o = (TimeoutEvent)other;
            if ((object)key != null)
            {
                if (!key.Equals(o.key))
                {
                    return false;
                }
            }
            else if ((object)o.key != null)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode(Fingerprint fingerprint)
        {
            var hash = new Hash(base.GetHashCode(fingerprint));
            if (fingerprint.Length <= tag.Offset)
            {
                return hash.Code;
            }
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                hash.Update(key);
            }
            return hash.Code;
        }

        public override int GetTypeId()
        {
            return tag.TypeId;
        }

        public override Cell.Tag GetTypeTag() 
        {
            return tag;
        }

        public override bool IsEquivalent(Cell other)
        {
            if (!base.IsEquivalent(other))
            {
                return false;
            }
            TimeoutEvent o = (TimeoutEvent)other;
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                if ((object)key != null)
                {
                    if (key is Cell)
                    {
                        if (!((Cell)key).IsEquivalent((Cell)o.key))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!key.Equals(o.key))
                        {
                            return false;
                        }
                    }
                }
                else if ((object)o.key != null)
                {
                    return false;
                }
            }
            return true;
        }

        protected override void Describe(StringBuilder stringBuilder)
        {
            base.Describe(stringBuilder);
            stringBuilder.AppendFormat(" Key={0}", key);
        }

        private void Initialize()
        {
            key = null;
        }
    }
}
