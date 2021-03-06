// auto-generated by xpiler

using System;
using System.Text;

using x2;

namespace x2.Tests
{
    public static class SampleConsts
    {
        public const int Const1 = 1;
        public const int Const2 = 2;
    }

    public class SampleCell1 : Cell
    {
        new protected static readonly Tag tag;

        private int foo;
        private string bar;

        public int Foo
        {
            get { return foo; }
            set
            {
                fingerprint.Touch(tag.Offset + 0);
                foo = value;
            }
        }

        public string Bar
        {
            get { return bar; }
            set
            {
                fingerprint.Touch(tag.Offset + 1);
                bar = value;
            }
        }

        static SampleCell1()
        {
            tag = new Tag(null, typeof(SampleCell1), 2);
        }

        public static SampleCell1 New()
        {
            return new SampleCell1();
        }

        public SampleCell1()
            : base(tag.NumProps)
        {
            Initialize();
        }

        protected SampleCell1(int length)
            : base(length + tag.NumProps)
        {
            Initialize();
        }

        public override bool EqualsTo(Cell other)
        {
            if (!base.EqualsTo(other))
            {
                return false;
            }
            SampleCell1 o = (SampleCell1)other;
            if (foo != o.foo)
            {
                return false;
            }
            if (bar != o.bar)
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
                hash.Update(foo);
            }
            if (touched[1])
            {
                hash.Update(bar);
            }
            return hash.Code;
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
            SampleCell1 o = (SampleCell1)other;
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                if (foo != o.foo)
                {
                    return false;
                }
            }
            if (touched[1])
            {
                if (bar != o.bar)
                {
                    return false;
                }
            }
            return true;
        }

        public override void Load(x2.Buffer buffer)
        {
            base.Load(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Read(out foo);
            }
            if (touched[1])
            {
                buffer.Read(out bar);
            }
        }

        public SampleCell1 Run(Action<SampleCell1> action)
        {
            action(this);
            return this;
        }

        protected override void Dump(x2.Buffer buffer)
        {
            base.Dump(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Write(foo);
            }
            if (touched[1])
            {
                buffer.Write(bar);
            }
        }

        protected override void Describe(StringBuilder stringBuilder)
        {
            base.Describe(stringBuilder);
            stringBuilder.AppendFormat(" Foo={0}", foo);
            stringBuilder.AppendFormat(" Bar=\"{0}\"", bar.Replace("\"", "\\\""));
        }

        private void Initialize()
        {
            foo = 0;
            bar = "";
        }
    }

    public class SampleCell2 : SampleCell1
    {
        new protected static readonly Tag tag;

        private bool baz;

        public bool Baz
        {
            get { return baz; }
            set
            {
                fingerprint.Touch(tag.Offset + 0);
                baz = value;
            }
        }

        static SampleCell2()
        {
            tag = new Tag(SampleCell1.tag, typeof(SampleCell2), 1);
        }

        new public static SampleCell2 New()
        {
            return new SampleCell2();
        }

        public SampleCell2()
            : base(tag.NumProps)
        {
            Initialize();
        }

        protected SampleCell2(int length)
            : base(length + tag.NumProps)
        {
            Initialize();
        }

        public override bool EqualsTo(Cell other)
        {
            if (!base.EqualsTo(other))
            {
                return false;
            }
            SampleCell2 o = (SampleCell2)other;
            if (baz != o.baz)
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
                hash.Update(baz);
            }
            return hash.Code;
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
            SampleCell2 o = (SampleCell2)other;
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                if (baz != o.baz)
                {
                    return false;
                }
            }
            return true;
        }

        public override void Load(x2.Buffer buffer)
        {
            base.Load(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Read(out baz);
            }
        }

        public SampleCell2 Run(Action<SampleCell2> action)
        {
            action(this);
            return this;
        }

        protected override void Dump(x2.Buffer buffer)
        {
            base.Dump(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Write(baz);
            }
        }

        protected override void Describe(StringBuilder stringBuilder)
        {
            base.Describe(stringBuilder);
            stringBuilder.AppendFormat(" Baz={0}", baz);
        }

        private void Initialize()
        {
            baz = false;
        }
    }

    public class SampleCell3 : SampleCell1
    {
        new protected static readonly Tag tag;

        private bool qux;

        public bool Qux
        {
            get { return qux; }
            set
            {
                fingerprint.Touch(tag.Offset + 0);
                qux = value;
            }
        }

        static SampleCell3()
        {
            tag = new Tag(SampleCell1.tag, typeof(SampleCell3), 1);
        }

        new public static SampleCell3 New()
        {
            return new SampleCell3();
        }

        public SampleCell3()
            : base(tag.NumProps)
        {
            Initialize();
        }

        protected SampleCell3(int length)
            : base(length + tag.NumProps)
        {
            Initialize();
        }

        public override bool EqualsTo(Cell other)
        {
            if (!base.EqualsTo(other))
            {
                return false;
            }
            SampleCell3 o = (SampleCell3)other;
            if (qux != o.qux)
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
                hash.Update(qux);
            }
            return hash.Code;
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
            SampleCell3 o = (SampleCell3)other;
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                if (qux != o.qux)
                {
                    return false;
                }
            }
            return true;
        }

        public override void Load(x2.Buffer buffer)
        {
            base.Load(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Read(out qux);
            }
        }

        public SampleCell3 Run(Action<SampleCell3> action)
        {
            action(this);
            return this;
        }

        protected override void Dump(x2.Buffer buffer)
        {
            base.Dump(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Write(qux);
            }
        }

        protected override void Describe(StringBuilder stringBuilder)
        {
            base.Describe(stringBuilder);
            stringBuilder.AppendFormat(" Qux={0}", qux);
        }

        private void Initialize()
        {
            qux = false;
        }
    }

    public class SampleCell4 : SampleCell2
    {
        new protected static readonly Tag tag;

        private bool quux;

        public bool Quux
        {
            get { return quux; }
            set
            {
                fingerprint.Touch(tag.Offset + 0);
                quux = value;
            }
        }

        static SampleCell4()
        {
            tag = new Tag(SampleCell2.tag, typeof(SampleCell4), 1);
        }

        new public static SampleCell4 New()
        {
            return new SampleCell4();
        }

        public SampleCell4()
            : base(tag.NumProps)
        {
            Initialize();
        }

        protected SampleCell4(int length)
            : base(length + tag.NumProps)
        {
            Initialize();
        }

        public override bool EqualsTo(Cell other)
        {
            if (!base.EqualsTo(other))
            {
                return false;
            }
            SampleCell4 o = (SampleCell4)other;
            if (quux != o.quux)
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
                hash.Update(quux);
            }
            return hash.Code;
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
            SampleCell4 o = (SampleCell4)other;
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                if (quux != o.quux)
                {
                    return false;
                }
            }
            return true;
        }

        public override void Load(x2.Buffer buffer)
        {
            base.Load(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Read(out quux);
            }
        }

        public SampleCell4 Run(Action<SampleCell4> action)
        {
            action(this);
            return this;
        }

        protected override void Dump(x2.Buffer buffer)
        {
            base.Dump(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Write(quux);
            }
        }

        protected override void Describe(StringBuilder stringBuilder)
        {
            base.Describe(stringBuilder);
            stringBuilder.AppendFormat(" Quux={0}", quux);
        }

        private void Initialize()
        {
            quux = false;
        }
    }

    public class SampleEvent1 : Event
    {
        new protected static readonly Tag tag;

        new public static int TypeId { get { return tag.TypeId; } }

        private int foo;
        private string bar;

        public int Foo
        {
            get { return foo; }
            set
            {
                fingerprint.Touch(tag.Offset + 0);
                foo = value;
            }
        }

        public string Bar
        {
            get { return bar; }
            set
            {
                fingerprint.Touch(tag.Offset + 1);
                bar = value;
            }
        }

        static SampleEvent1()
        {
            tag = new Tag(Event.tag, typeof(SampleEvent1), 2,
                    1);
        }

        new public static SampleEvent1 New()
        {
            return new SampleEvent1();
        }

        public SampleEvent1()
            : base(tag.NumProps)
        {
            Initialize();
        }

        protected SampleEvent1(int length)
            : base(length + tag.NumProps)
        {
            Initialize();
        }

        public override bool EqualsTo(Cell other)
        {
            if (!base.EqualsTo(other))
            {
                return false;
            }
            SampleEvent1 o = (SampleEvent1)other;
            if (foo != o.foo)
            {
                return false;
            }
            if (bar != o.bar)
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
                hash.Update(foo);
            }
            if (touched[1])
            {
                hash.Update(bar);
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
            SampleEvent1 o = (SampleEvent1)other;
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                if (foo != o.foo)
                {
                    return false;
                }
            }
            if (touched[1])
            {
                if (bar != o.bar)
                {
                    return false;
                }
            }
            return true;
        }

        public override void Load(x2.Buffer buffer)
        {
            base.Load(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Read(out foo);
            }
            if (touched[1])
            {
                buffer.Read(out bar);
            }
        }

        public SampleEvent1 Run(Action<SampleEvent1> action)
        {
            action(this);
            return this;
        }

        public override void Serialize(x2.Buffer buffer)
        {
            buffer.WriteUInt29(tag.TypeId);
            this.Dump(buffer);
        }

        protected override void Dump(x2.Buffer buffer)
        {
            base.Dump(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Write(foo);
            }
            if (touched[1])
            {
                buffer.Write(bar);
            }
        }

        protected override void Describe(StringBuilder stringBuilder)
        {
            base.Describe(stringBuilder);
            stringBuilder.AppendFormat(" Foo={0}", foo);
            stringBuilder.AppendFormat(" Bar=\"{0}\"", bar.Replace("\"", "\\\""));
        }

        private void Initialize()
        {
            foo = 0;
            bar = "";
        }
    }

    public class SampleEvent2 : SampleEvent1
    {
        new protected static readonly Tag tag;

        new public static int TypeId { get { return tag.TypeId; } }

        private bool baz;

        public bool Baz
        {
            get { return baz; }
            set
            {
                fingerprint.Touch(tag.Offset + 0);
                baz = value;
            }
        }

        static SampleEvent2()
        {
            tag = new Tag(SampleEvent1.tag, typeof(SampleEvent2), 1,
                    2);
        }

        new public static SampleEvent2 New()
        {
            return new SampleEvent2();
        }

        public SampleEvent2()
            : base(tag.NumProps)
        {
            Initialize();
        }

        protected SampleEvent2(int length)
            : base(length + tag.NumProps)
        {
            Initialize();
        }

        public override bool EqualsTo(Cell other)
        {
            if (!base.EqualsTo(other))
            {
                return false;
            }
            SampleEvent2 o = (SampleEvent2)other;
            if (baz != o.baz)
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
                hash.Update(baz);
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
            SampleEvent2 o = (SampleEvent2)other;
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                if (baz != o.baz)
                {
                    return false;
                }
            }
            return true;
        }

        public override void Load(x2.Buffer buffer)
        {
            base.Load(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Read(out baz);
            }
        }

        public SampleEvent2 Run(Action<SampleEvent2> action)
        {
            action(this);
            return this;
        }

        public override void Serialize(x2.Buffer buffer)
        {
            buffer.WriteUInt29(tag.TypeId);
            this.Dump(buffer);
        }

        protected override void Dump(x2.Buffer buffer)
        {
            base.Dump(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Write(baz);
            }
        }

        protected override void Describe(StringBuilder stringBuilder)
        {
            base.Describe(stringBuilder);
            stringBuilder.AppendFormat(" Baz={0}", baz);
        }

        private void Initialize()
        {
            baz = false;
        }
    }

    public class SampleEvent3 : SampleEvent1
    {
        new protected static readonly Tag tag;

        new public static int TypeId { get { return tag.TypeId; } }

        private bool qux;

        public bool Qux
        {
            get { return qux; }
            set
            {
                fingerprint.Touch(tag.Offset + 0);
                qux = value;
            }
        }

        static SampleEvent3()
        {
            tag = new Tag(SampleEvent1.tag, typeof(SampleEvent3), 1,
                    3);
        }

        new public static SampleEvent3 New()
        {
            return new SampleEvent3();
        }

        public SampleEvent3()
            : base(tag.NumProps)
        {
            Initialize();
        }

        protected SampleEvent3(int length)
            : base(length + tag.NumProps)
        {
            Initialize();
        }

        public override bool EqualsTo(Cell other)
        {
            if (!base.EqualsTo(other))
            {
                return false;
            }
            SampleEvent3 o = (SampleEvent3)other;
            if (qux != o.qux)
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
                hash.Update(qux);
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
            SampleEvent3 o = (SampleEvent3)other;
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                if (qux != o.qux)
                {
                    return false;
                }
            }
            return true;
        }

        public override void Load(x2.Buffer buffer)
        {
            base.Load(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Read(out qux);
            }
        }

        public SampleEvent3 Run(Action<SampleEvent3> action)
        {
            action(this);
            return this;
        }

        public override void Serialize(x2.Buffer buffer)
        {
            buffer.WriteUInt29(tag.TypeId);
            this.Dump(buffer);
        }

        protected override void Dump(x2.Buffer buffer)
        {
            base.Dump(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Write(qux);
            }
        }

        protected override void Describe(StringBuilder stringBuilder)
        {
            base.Describe(stringBuilder);
            stringBuilder.AppendFormat(" Qux={0}", qux);
        }

        private void Initialize()
        {
            qux = false;
        }
    }

    public class SampleEvent4 : SampleEvent2
    {
        new protected static readonly Tag tag;

        new public static int TypeId { get { return tag.TypeId; } }

        private bool quux;

        public bool Quux
        {
            get { return quux; }
            set
            {
                fingerprint.Touch(tag.Offset + 0);
                quux = value;
            }
        }

        static SampleEvent4()
        {
            tag = new Tag(SampleEvent2.tag, typeof(SampleEvent4), 1,
                    4);
        }

        new public static SampleEvent4 New()
        {
            return new SampleEvent4();
        }

        public SampleEvent4()
            : base(tag.NumProps)
        {
            Initialize();
        }

        protected SampleEvent4(int length)
            : base(length + tag.NumProps)
        {
            Initialize();
        }

        public override bool EqualsTo(Cell other)
        {
            if (!base.EqualsTo(other))
            {
                return false;
            }
            SampleEvent4 o = (SampleEvent4)other;
            if (quux != o.quux)
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
                hash.Update(quux);
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
            SampleEvent4 o = (SampleEvent4)other;
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                if (quux != o.quux)
                {
                    return false;
                }
            }
            return true;
        }

        public override void Load(x2.Buffer buffer)
        {
            base.Load(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Read(out quux);
            }
        }

        public SampleEvent4 Run(Action<SampleEvent4> action)
        {
            action(this);
            return this;
        }

        public override void Serialize(x2.Buffer buffer)
        {
            buffer.WriteUInt29(tag.TypeId);
            this.Dump(buffer);
        }

        protected override void Dump(x2.Buffer buffer)
        {
            base.Dump(buffer);
            var touched = new Capo<bool>(fingerprint, tag.Offset);
            if (touched[0])
            {
                buffer.Write(quux);
            }
        }

        protected override void Describe(StringBuilder stringBuilder)
        {
            base.Describe(stringBuilder);
            stringBuilder.AppendFormat(" Quux={0}", quux);
        }

        private void Initialize()
        {
            quux = false;
        }
    }
}
