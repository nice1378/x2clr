﻿// Copyright (c) 2013 Jae-jun Kang
// See the file COPYING for license details.

using System;
using System.Collections.Generic;
using System.IO;

namespace x2.xpiler {
  class Xpiler {
    private static readonly Dictionary<string, IHandler> handlers;
    private static readonly Dictionary<string, IFormatter> formatters;

    private readonly Options options;
    private readonly IFormatter formatter;
    private bool error;

    public Options Options {
      get { return options; }
    }

    public bool Error {
      get { return error; }
    }

    static Xpiler() {
      handlers = new Dictionary<string, IHandler>();
      handlers.Add(".xml", new XmlHandler());

      formatters = new Dictionary<string, IFormatter>();
      formatters.Add("cs", new CSharpFormatter());
    }

    public Xpiler(Options options) {
      this.options = options;
      formatter = formatters[options.Spec];
      error = false;
    }

    public void Process(string path) {
      if (Directory.Exists(path)) {
        ProcessDir(path);
      } else if (File.Exists(path)) {
        ProcessFile(path);
      } else {
        Console.Error.WriteLine("{0} doesn't exist.", path);
        error = true;
      }
    }

    private void ProcessDir(string path) {
      Console.WriteLine("Directory {0}", Path.GetFullPath(path));
      DirectoryInfo di = new DirectoryInfo(path);
      FileSystemInfo[] entries = di.GetFileSystemInfos();
      foreach (FileSystemInfo entry in entries) {
        string pathname = Path.Combine(path, entry.Name);
        if ((entry.Attributes & FileAttributes.Directory) != 0) {
          if (options.Recursive) {
            ProcessDir(pathname);
          }
        } else {
          ProcessFile(pathname);
        }
      }
    }

    private void ProcessFile(string path) {
      string filename = Path.GetFileName(path);
      string extension = Path.GetExtension(path);

      IHandler handler;
      if (handlers.TryGetValue(extension.ToLower(), out handler) == false) {
        return;
      }

      Console.WriteLine(filename);

      Document doc = handler.Handle(path);
      if (doc == null) {
        error = true;
        return;
      }
      if (formatter.Format(doc) == false) {
        error = true;
      }
    }
  }
}