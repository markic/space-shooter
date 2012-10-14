using System;

namespace Space
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SpaceShooter game = new SpaceShooter())
            {
                game.Run();         
                
            }    
        
        }
    }
#endif
}

