﻿// Copyright (c) 2013 Jae-jun Kang
// See the file COPYING for license details.

using System;
using System.Collections.Generic;
using System.IO;

namespace xpiler
{
    public class CSharpFormatter : Formatter
    {
        private const string Extension = ".cs";

        public override string Description { get { return "C#"; } }

        public override bool Format(Document doc, string outDir)
        {
            try
            {
                var context = new CSharpFormatterContext()
                {
                    Doc = doc,
                    Target = Path.Combine(outDir, doc.BaseName + Extension)
                };
                using (var writer = new StreamWriter(context.Target))
                {
                    context.Out = writer;
                    FormatHead(context);
                    FormatBody(context);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        private void FormatHead(FormatterContext context)
        {
            var @out = context.Out;
            @out.WriteLine("// auto-generated by xpiler");
            @out.WriteLine();
            @out.WriteLine("using System;");
            @out.WriteLine("using System.Text;");
            @out.WriteLine();
            @out.WriteLine("using x2;");
            @out.WriteLine();
        }

        private void FormatBody(FormatterContext context)
        {
            var leading = true;
            foreach (var def in context.Doc.Definitions)
            {
                if (leading)
                {
                    leading = false;
                }
                else
                {
                    context.Out.WriteLine();
                }
                def.Format(context);
            }
        }

        public override bool IsUpToDate(string path, string outDir)
        {
            var baseName = Path.GetFileNameWithoutExtension(path);
            var target = Path.Combine(outDir, baseName + Extension);
            return File.Exists(target) &&
                   File.GetLastWriteTime(target) >= File.GetLastWriteTime(path);
        }
    }

    class CSharpFormatterContext : FormatterContext
    {
        public Definition Def { get; set; }
        public string Target { get; set; }

        public override void FormatEnum(EnumDef def) { }
        public override void FormatCell(CellDef def) { }
    }

/*
  class CSharpFormatter : Formatter {
    private static readonly Dictionary<string, string> nativeTypes;
    private static readonly Dictionary<string, string> defaultValues;

    static CSharpFormatter() {
      nativeTypes = new Dictionary<string, string>();
      nativeTypes.Add("bool", "bool");
      nativeTypes.Add("int8", "sbyte");
      nativeTypes.Add("int16", "short");
      nativeTypes.Add("int32", "int");
      nativeTypes.Add("int64", "long");
      nativeTypes.Add("string", "string");

      defaultValues = new Dictionary<string, string>();
      defaultValues.Add("bool", "false");
      defaultValues.Add("int8", "0");
      defaultValues.Add("int16", "0");
      defaultValues.Add("int32", "0");
      defaultValues.Add("int64", "0");
      defaultValues.Add("string", "");
    }

    private void FormatEnum(Context context) {
      StreamWriter @out = context.@out;
      EnumDef def = (EnumDef)context.def;
      @out.WriteLine("  public enum {0} {{", def.Name);
      foreach (EnumDef.Element element in def.Elements) {
        @out.Write("    {0}", element.Name);
        if (!String.IsNullOrEmpty(element.Value)) {
          @out.Write(" = {0}", element.Value);
        }
        if (element != def.Elements[def.Elements.Count - 1]) {
          @out.Write(",");
        }
        @out.WriteLine();
      }
      @out.WriteLine("  }");
    }

    private void FormatCell(Context context) {
      StreamWriter @out = context.@out;
      CellDef def = (CellDef)context.def;
      string @base = def.Base;
      if (String.IsNullOrEmpty(@base)) {
        @base = (context.isEvent ? "Event" : "Cell");
      }
      PreprocessProperties(context);

      @out.WriteLine("  public class {0} : {1} {{", def.Name, @base);
      @out.WriteLine("    new protected static readonly Tag tag;");
      @out.WriteLine();
      FormatFields(context);
      if (def.Properties.Count != 0) {
        @out.WriteLine();
      }
      FormatProperties(context);
      if (def.Properties.Count != 0) {
        @out.WriteLine();
      }
      FormatMethods(context);
      @out.WriteLine("  }");
    }

    private void FormatFields(Context context) {
      CellDef def = (CellDef)context.def;
      foreach (CellDef.Property property in def.Properties) {
        context.@out.WriteLine("    private {0} {1};",
                               property.NativeType, property.NativeName);
      }
    }

    private void FormatProperties(Context context) {
      StreamWriter @out = context.@out;
      CellDef def = (CellDef)context.def;
      foreach (CellDef.Property property in def.Properties) {
        if (property != def.Properties[0]) {
          @out.WriteLine();
        }
        @out.WriteLine("    public {0} {1} {{",
                       property.NativeType, property.Name);
        @out.WriteLine("      get {{ return {0}; }}", property.NativeName);
        @out.WriteLine("      set {");
        @out.WriteLine("        fingerprint.Touch(tag.Offset + {0});",
                       property.Index);
        @out.WriteLine("        {0} = value;", property.NativeName);
        @out.WriteLine("      }");
        @out.WriteLine("    }");
      }
    }

    private void FormatMethods(Context context) {
      StreamWriter @out = context.@out;
      FormatStaticConstructor(context);
      @out.WriteLine();
      FormatConstructor(context);
      @out.WriteLine();
      FormatEqualsTo(context);
      @out.WriteLine();
      FormatGetHashCode(context);
      @out.WriteLine();
      FormatGetType(context);
      @out.WriteLine();
      FormatIsEquivalent(context);
      @out.WriteLine();
      FormatLoad(context);
      if (context.isEvent) {
        @out.WriteLine();
        FormatSerialize(context);
      }
      @out.WriteLine();
      FormatDump(context);
      @out.WriteLine();
      FormatDescribe(context);
      @out.WriteLine();
      FormatInitializer(context);
    }

    private void FormatStaticConstructor(Context context) {
      StreamWriter @out = context.@out;
      CellDef def = (CellDef)context.def;
      string baseTag = def.Base;
      if (String.IsNullOrEmpty(baseTag)) {
        baseTag = (context.isEvent ? "Event.tag" : "null");
      } else {
        baseTag += ".tag";
      }
      @out.WriteLine("    static {0}() {{", def.Name);
      @out.Write("      tag = new Tag({0}, typeof({1}), {2}",
                 baseTag, def.Name, def.Properties.Count);
      if (context.isEvent) {
        int i;
        string s = ((EventDef)def).Id;
        @out.WriteLine(",");
        @out.Write("                    ");
        if (!Int32.TryParse(s, out i)) {
          @out.Write("(int)");
        }
        @out.Write("{0}", s);
      }
      @out.WriteLine(");");
      @out.WriteLine("    }");
    }

    private void FormatConstructor(Context context) {
      StreamWriter @out = context.@out;
      CellDef def = (CellDef)context.def;
      @out.WriteLine("    public {0}()", def.Name);
      @out.WriteLine("        : base(tag.NumProps) {");
      @out.WriteLine("      Initialize();");
      @out.WriteLine("    }");
      @out.WriteLine();
      @out.WriteLine("    protected {0}(int length)", def.Name);
      @out.WriteLine("        : base(length + tag.NumProps) {");
      @out.WriteLine("      Initialize();");
      @out.WriteLine("    }");
    }

    private void FormatEqualsTo(Context context) {
      StreamWriter @out = context.@out;
      CellDef def = (CellDef)context.def;
      @out.WriteLine("    public override bool EqualsTo(Cell other) {");
      @out.WriteLine("      if (!base.EqualsTo(other)) {");
      @out.WriteLine("        return false;");
      @out.WriteLine("      }");
      if (def.Properties.Count != 0) {
        @out.WriteLine("      {0} o = ({0})other;", def.Name);
        foreach (CellDef.Property property in def.Properties) {
          @out.WriteLine("      if ({0} != o.{0}) {{", property.NativeName);
          @out.WriteLine("        return false;");
          @out.WriteLine("      }");
        }
      }
      @out.WriteLine("      return true;");
      @out.WriteLine("    }");
    }

    private void FormatGetHashCode(Context context) {
      StreamWriter @out = context.@out;
      CellDef def = (CellDef)context.def;
      @out.WriteLine("    public override int GetHashCode(Fingerprint fingerprint) {");
      @out.WriteLine("      Hash hash = new Hash(base.GetHashCode(fingerprint));");
      if (def.Properties.Count != 0) {
        @out.WriteLine("      Fingerprint.View fingerprintView = ");
        @out.WriteLine("          new Fingerprint.View(fingerprint, tag.Offset);");
        foreach (CellDef.Property property in def.Properties) {
          @out.WriteLine("      if (fingerprintView[{0}]) {{", property.Index);
          @out.WriteLine("        hash.Update({0});", property.NativeName);
          @out.WriteLine("      }");
        }
      }
      @out.WriteLine("      return hash.Code;");
      @out.WriteLine("    }");
    }

    private void FormatGetType(Context context) {
      StreamWriter @out = context.@out;
      if (context.isEvent) {
        @out.WriteLine("    public override int GetTypeId() {");
        @out.WriteLine("      return tag.TypeId;");
        @out.WriteLine("    }");
        @out.WriteLine();
      }
      @out.WriteLine("    public override Cell.Tag GetTypeTag() {");
      @out.WriteLine("      return tag;");
      @out.WriteLine("    }");
    }

    private void FormatIsEquivalent(Context context) {
      StreamWriter @out = context.@out;
      CellDef def = (CellDef)context.def;
      @out.WriteLine("    public override bool IsEquivalent(Cell other) {");
      @out.WriteLine("      if (!base.IsEquivalent(other)) {");
      @out.WriteLine("        return false;");
      @out.WriteLine("      }");
      if (def.Properties.Count != 0) {
        @out.WriteLine("      {0} o = ({0})other;", def.Name);
        @out.WriteLine("      Fingerprint.View fingerprintView = ");
        @out.WriteLine("          new Fingerprint.View(fingerprint, tag.Offset);");
        foreach (CellDef.Property property in def.Properties) {
          @out.WriteLine("      if (fingerprintView[{0}]) {{", property.Index);
          @out.WriteLine("        if ({0} != o.{0}) {{", property.NativeName);
          @out.WriteLine("          return false;");
          @out.WriteLine("        }");
          @out.WriteLine("      }");
        }
      }
      @out.WriteLine("      return true;");
      @out.WriteLine("    }");
    }

    private void FormatLoad(Context context) {
      StreamWriter @out = context.@out;
      CellDef def = (CellDef)context.def;
      @out.WriteLine("    public override void Load(x2.Buffer buffer) {");
      @out.WriteLine("      base.Load(buffer);");
      if (def.Properties.Count != 0) {
        @out.WriteLine("      Fingerprint.View fingerprintView = ");
        @out.WriteLine("          new Fingerprint.View(fingerprint, tag.Offset);");
        foreach (CellDef.Property property in def.Properties) {
          @out.WriteLine("      if (fingerprintView[{0}]) {{", property.Index);
          if (IsPrimitiveType(property.Type)) {
            @out.WriteLine("        buffer.Read(out {0});", property.NativeName);
          } else {
            @out.WriteLine("        {0}.Load(buffer);", property.NativeName);
          }
          @out.WriteLine("      }");
        }
      }
      @out.WriteLine("    }");
    }

    private void FormatSerialize(Context context) {
      StreamWriter @out = context.@out;
      @out.WriteLine("    public override void Serialize(x2.Buffer buffer) {");
      @out.WriteLine("      buffer.WriteUInt29(tag.TypeId);");
      @out.WriteLine("      this.Dump(buffer);");
      @out.WriteLine("    }");
    }

    private void FormatDump(Context context) {
      StreamWriter @out = context.@out;
      CellDef def = (CellDef)context.def;
      @out.WriteLine("    protected override void Dump(x2.Buffer buffer) {");
      @out.WriteLine("      base.Dump(buffer);");
      if (def.Properties.Count != 0) {
        @out.WriteLine("      Fingerprint.View fingerprintView = ");
        @out.WriteLine("          new Fingerprint.View(fingerprint, tag.Offset);");
        foreach (CellDef.Property property in def.Properties) {
          @out.WriteLine("      if (fingerprintView[{0}]) {{", property.Index);
          if (IsPrimitiveType(property.Type)) {
            @out.WriteLine("        buffer.Write({0});", property.NativeName);
          } else {
            @out.WriteLine("        {0}.Serialize(buffer);", property.NativeName);
          }
          @out.WriteLine("      }");
        }
      }
      @out.WriteLine("    }");
    }

    private void FormatDescribe(Context context) {
      StreamWriter @out = context.@out;
      CellDef def = (CellDef)context.def;
      @out.WriteLine("    protected override void Describe(StringBuilder stringBuilder) {");
      @out.WriteLine("      base.Describe(stringBuilder);");
      foreach (CellDef.Property property in def.Properties) {
        @out.WriteLine("      stringBuilder.AppendFormat(\" {0} = {{0}}\", {1});", property.Name, property.NativeName);
      }
      @out.WriteLine("    }");
    }

    private void FormatInitializer(Context context) {
      StreamWriter @out = context.@out;
      CellDef def = (CellDef)context.def;
      @out.WriteLine("    private void Initialize() {");
      foreach (CellDef.Property property in def.Properties) {
        @out.WriteLine("      {0} = {1};", property.NativeName, property.DefaultValue);
      }
      @out.WriteLine("    }");
    }

    private void PreprocessProperties(Context context) {
      CellDef def = (CellDef)context.def;
      int index = 0;
      foreach (CellDef.Property property in def.Properties) {
        property.Index = index++;
        
        property.NativeName = FirstToLower(property.Name);
        property.Name = FirstToUpper(property.Name);

        if (defaultValues.ContainsKey(property.Type)) {
          if (String.IsNullOrEmpty(property.DefaultValue)) {
            property.DefaultValue = defaultValues[property.Type];
          }
          if (property.Type == "string") {
            property.DefaultValue = "\"" + property.DefaultValue + "\"";
          }
        } else {
          property.DefaultValue = "null";
        }

        if (nativeTypes.ContainsKey(property.Type)) {
          property.NativeType = nativeTypes[property.Type];
        } else {
          if (property.Type.ToLower() == "list") {
            property.NativeType = String.Format("ListCell<{0}>", property.Subtype);
          }
        }
      }
    }

    private static bool IsPrimitiveType(string type) {
      return nativeTypes.ContainsKey(type);
    }

    private static string FirstToLower(string s) {
      if (!String.IsNullOrEmpty(s)) {
        char[] chars = s.ToCharArray();
        if (Char.IsUpper(chars[0])) {
          chars[0] = Char.ToLower(chars[0]);
          return new string(chars);
        }
      }
      return s;
    }

    private static string FirstToUpper(string s) {
      if (!String.IsNullOrEmpty(s)) {
        char[] chars = s.ToCharArray();
        if (Char.IsLower(chars[0])) {
          chars[0] = Char.ToUpper(chars[0]);
          return new string(chars);
        }
      }
      return s;
    }
  }
*/
}
