using System;

namespace Abduction
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Abduction game = new Abduction())
            {
                game.Run();
            }
        }
    }
#endif
}