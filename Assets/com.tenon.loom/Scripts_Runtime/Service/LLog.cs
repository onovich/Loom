using System;

namespace TenonKit.Loom {

    public static class LLog {
        public static Action<string> Log = Console.WriteLine;
        public static Action<string> Warning = (msg) => Console.WriteLine($"WARNING: {msg}");
        public static Action<string> Error = (msg) => Console.Error.WriteLine($"ERROR: {msg}");
        public static Action<bool, string> Assert = (condition, msg) => {
            if (!condition) {
                Console.Error.WriteLine($"ASSERT: {msg}");
            }
        };
    }

}