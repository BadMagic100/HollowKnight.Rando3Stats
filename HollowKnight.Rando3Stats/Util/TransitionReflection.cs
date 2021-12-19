using RandomizerMod.Randomization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HollowKnight.Rando3Stats.Util
{
#pragma warning disable 0649 // assignment via reflection
    internal struct TransitionDef
    {
        public string sceneName;
        public string doorName;
        public string areaName;

        public string destinationScene;
        public string destinationGate;

        public string[] logic;
        public List<(int, int)> processedLogic;

        public bool isolated;
        public bool deadEnd;
        public int oneWay; // 0 == 2-way, 1 == can only go in, 2 == can only come out
    }
#pragma warning restore 0649

    internal static class TransitionReflection
    {
        private static Type logicManagerType = typeof(LogicManager);
        private static MethodInfo getTransitionInternal = logicManagerType.GetMethod("GetTransitionDef", BindingFlags.NonPublic | BindingFlags.Static);
        private static Type transitionDefInternal = Assembly.GetAssembly(logicManagerType).GetType("RandomizerMod.Randomization.TransitionDef");

        public static TransitionDef GetTransitionDef(string transitionName)
        {
            // needs to be boxed as an object so it's a reference type; otherwise setvalue won't work right because it will operate on a copy
            object boxedDef = new TransitionDef();
            Type defType = typeof(TransitionDef);

            object transitionInfo = getTransitionInternal.Invoke(null, new string[] { transitionName });
            foreach (FieldInfo field in transitionDefInternal.GetFields())
            {
                object fieldValue = field.GetValue(transitionInfo);
                FieldInfo returnField = defType.GetField(field.Name);
                returnField.SetValue(boxedDef, fieldValue);
            }
            return (TransitionDef)boxedDef;
        }
    }
}
