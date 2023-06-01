using System.Reflection;
using AutoBogus;
using Bogus;

namespace TestDataGeneration;

internal static class AutoFakerExtensions
{
    /// <summary>
    /// Clones the internal state of a <seealso cref="AutoFaker{T}"/> into a new <seealso cref="AutoFaker{T}"/> so that
    /// both are isolated from each other. The clone will have internal state
    /// reset as if <seealso cref="AutoFaker{T}.Generate(string)"/> was never called.
    /// </summary>
    public static AutoFaker<T> AutoFakerClone<T>(this AutoFaker<T> original) where T : class
    {
        var clone = new AutoFaker<T>(original.Locale, original.Binder);

        //copy internal state.
        //strict modes.
        var strictModesField = typeof(AutoFaker<T>).GetField("StrictModes", BindingFlags.NonPublic | BindingFlags.Instance);
        var originalStrictModes = (Dictionary<string, bool>)strictModesField!.GetValue(original)!;
        var cloneStrictModes = (Dictionary<string, bool>)strictModesField!.GetValue(clone)!;
        foreach (var root in originalStrictModes)
        {
            cloneStrictModes.Add(root.Key, root.Value);
        }
        //strictModesField.SetValue(clone, cloneStrictModes);

        //create actions
        var createActionsField = typeof(AutoFaker<T>).GetField("CreateActions", BindingFlags.NonPublic | BindingFlags.Instance);
        var originalCreateActions = (Dictionary<string, Func<Faker, T>>)createActionsField!.GetValue(original)!;
        var cloneCreateActions = (Dictionary<string, Func<Faker, T>>)createActionsField.GetValue(clone)!;
        foreach (var root in originalCreateActions)
        {
            cloneCreateActions[root.Key] = root.Value;
        }
        //createActionsField.SetValue(clone, cloneCreateActions);

        //finalize actions
        var finalizeActionsField = typeof(AutoFaker<T>).GetField("FinalizeActions", BindingFlags.NonPublic | BindingFlags.Instance);
        var originalFinalizeActions = (Dictionary<string, FinalizeAction<T>>)finalizeActionsField!.GetValue(original)!;
        var cloneFinalizeActions = (Dictionary<string, FinalizeAction<T>>)finalizeActionsField.GetValue(clone)!;
        foreach (var root in originalFinalizeActions)
        {
            cloneFinalizeActions.Add(root.Key, root.Value);
        }
        //finalizeActionsField.SetValue(clone, cloneFinalizeActions);
        
        //actions
        var actionsField = typeof(AutoFaker<T>).GetField("Actions", BindingFlags.NonPublic | BindingFlags.Instance);
        var originalActions = (MultiDictionary<string, string, PopulateAction<T>>)actionsField!.GetValue(original)!;
        var cloneActions = (MultiDictionary<string, string, PopulateAction<T>>)actionsField.GetValue(clone)!;
        foreach (var root in originalActions)
        {
            foreach (var kv in root.Value)
            {
                cloneActions.Add(root.Key, kv.Key, kv.Value);
            }
        }

        //get Faker<T> localSeed by reflection.
        var localSeedField = typeof(Faker<T>).GetField("localSeed", BindingFlags.NonPublic | BindingFlags.Instance);
        var localSeed = (int?)localSeedField.GetValue(original);
        if (localSeed.HasValue)
        {
            clone.UseSeed(localSeed.Value);
        }

        return clone;
    }
}