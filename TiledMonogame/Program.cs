using System;

namespace TiledMonogame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ExampleGame())
                game.Run();
        }
    }
}
