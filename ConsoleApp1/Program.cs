using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(@"E:\New folder\Syndrome [Game] [v01.00].pkg");
            PS4_Tools.TROPHY.TROPCONF.Trophy test = new PS4_Tools.TROPHY.TROPUSR
                Console.WriteLine(test.detail);
        }
    }
}
