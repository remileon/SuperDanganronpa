using System;
using MoonSharp.Interpreter;
using UnityEngine;
using Coroutine = MoonSharp.Interpreter.Coroutine;

namespace common_scripts
{
    public class MoonSharpManager
    {
        private static readonly Script script = new Script();

        static MoonSharpManager()
        {
            script.DoFile("lib.lua");
            RegisterFunc("Log", (Action<object>) Debug.Log);
        }

        public static void RegisterFunc(string name, object func)
        {
            script.Globals[name] = func;
        }

        public static Coroutine RunCoroutine(string scriptName)
        {
            DynValue function = script.DoFile(scriptName + ".lua");
            DynValue coroutine = script.CreateCoroutine(function);
            return coroutine.Coroutine;
        }
    }
}