using Quintessential;

namespace Fanolytics
{
    public class MainClass : QuintessentialMod
    {
        public static readonly string ReflectionPermission = "fanolytics:reflection";

        public override void Load()
        {
            Logger.Log("Fanolytics Loaded");
        }


        public override void LoadPuzzleContent()
        {
            Atoms.AddAtomTypes();
            Glyphs.AddPartTypes();
            QApi.AddPuzzlePermission(ReflectionPermission, "Glyph of Reflection", "Fanolytics");
        }
        public override void PostLoad()
        {
            
        }

        public override void Unload()
        {
            
        }
    }
}
