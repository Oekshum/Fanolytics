using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartType = class_139;
using Texture = class_256;
using Permissions = enum_149;
using Quintessential;

namespace Fanolytics
{
    internal static class Glyphs
    {
        public static PartType Reflection;

        public static Texture reflectionBase = Brimstone.API.GetTexture("textures/parts/oekshum/Fanolytics/reflection_base");
        public static Texture bowl = class_238.field_1989.field_90.field_170;
        public static Texture hole = class_238.field_1989.field_90.field_255.field_293;
        public static Texture[] irisAnimation = class_238.field_1989.field_90.field_246;
        public static Texture irisRing = class_238.field_1989.field_90.field_228.field_271;
        public static Texture irisWell = class_238.field_1989.field_90.field_228.field_272;

        public static Texture reflectionGlow = Brimstone.API.GetTexture("textures/select/erikhaag/MiscGlyphs/triline_glow");
        public static Texture reflectionStroke = Brimstone.API.GetTexture("textures/select/erikhaag/MiscGlyphs/triline_stroke");
        public static Texture reflectionIcon = Brimstone.API.GetTexture("textures/parts/erikhaag/MiscGlyphs/icons/reflection");
        public static Texture reflectionIconHover = Brimstone.API.GetTexture("textures/parts/erikhaag/MiscGlyphs/icons/reflection_hover");

        public static readonly HexIndex reflectionBowl = new(0, 0);
        public static readonly HexIndex reflectionInput = new(-1, 0);
        public static readonly HexIndex reflectionOutput = new(1, 0);

        public static void AddPartTypes()
        {
            Reflection = new()
            {
                field_1528 = "fano-reflect", // ID
                field_1529 = class_134.method_253("Glyph of Reflection", string.Empty), // Name
                field_1530 = class_134.method_253("The glyph of reflection consumes a Fanolytic, mirroring its aspect relative to another.", string.Empty), // Description
                field_1531 = 25, // Cost
                field_1539 = true, // Is a glyph
                field_1549 = reflectionGlow, // Shadow/glow
                field_1550 = reflectionStroke, // Stroke/outline
                field_1547 = reflectionIcon, // Panel icon
                field_1548 = reflectionIconHover, // Hovered panel icon
                field_1540 = new HexIndex[]
                {
                reflectionBowl,
                reflectionInput,
                reflectionOutput
                },
                field_1551 = Permissions.None,
                CustomPermissionCheck = perms => perms.Contains(MainClass.ReflectionPermission)
            };

            QApi.AddPartTypeToPanel(Reflection, false);

            QApi.AddPartType(Reflection, static (part, pos, editor, renderer) =>
            {
                PartSimState pss = editor.method_507().method_481(part);
                class_236 uco = editor.method_1989(part, pos);
                float time = editor.method_504();

                Vector2 offset = new(140f, 65f);
                renderer.method_523(reflectionBase, Vector2.Zero, offset, 0f);
                renderer.method_529(hole, reflectionInput, Vector2.Zero);
                renderer.method_528(bowl, reflectionBowl, Vector2.Zero);
                renderer.method_529(irisWell, reflectionOutput, Vector2.Zero);

                int irisFrame = 15;
                bool afterIrisOpens = false;
                AtomType outputAtom = pss.field_2743 ? pss.field_2744[0] : Brimstone.API.VanillaAtoms.salt;
                Molecule risingAtom = Molecule.method_1121(outputAtom);

                Vector2 risingOffset = uco.field_1984 + class_187.field_1742.method_492(reflectionOutput).Rotated(uco.field_1985);

                if (pss.field_2743)
                {
                    irisFrame = class_162.method_404((int)(class_162.method_411(1f, -1f, time) * 16f), 0, 15);
                    afterIrisOpens = time > 0.5f;
                    if (!afterIrisOpens)
                    {
                        // show atom rising behind iris
                        Editor.method_925(risingAtom, risingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                    }
                }
                renderer.method_529(irisAnimation[irisFrame], reflectionOutput, Vector2.Zero);
                renderer.method_528(irisRing, reflectionOutput, Vector2.Zero);
                if (pss.field_2743 && afterIrisOpens)
                {
                    // show atom rising infront of iris
                    Editor.method_925(risingAtom, risingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                }
            });

            QApi.RunAfterCycle(static (sim, first) =>
            {
                SolutionEditorBase seb = sim.field_3818;
                Dictionary<Part, PartSimState> pss = sim.field_3821;
                List<Part> parts = seb.method_502().field_3919;

                foreach (Part part in parts)
                {
                    PartType type = part.method_1159();
                    if (type == Reflection)
                    {
                        if (first)
                        {
                            if (sim.FindAtomRelative(part, reflectionOutput).method_1085())
                            {
                                continue;
                            }
                            Logger.Log("a");
                            if (!(sim.FindAtomRelative(part, reflectionBowl).method_99(out AtomReference mirror) && sim.FindAtomRelative(part, reflectionInput).method_99(out AtomReference subject)))
                            {
                                continue;
                            }

                            if (subject.field_2281 || subject.field_2282)
                            {
                                continue;
                            }

                            if (!(ReflectionChecker.isFano(subject.field_2280) && ReflectionChecker.isFano(mirror.field_2280)))
                            {
                                continue;
                            }

                            AtomType reflected = ReflectionChecker.GetReflectedAtom(subject.field_2280, mirror.field_2280);

                            if (reflected is null)
                            {
                                continue;
                            }

                            Brimstone.API.RemoveAtom(subject);
                            Brimstone.API.DrawFallingAtom(seb, subject);

                            Brimstone.API.AddSmallCollider(sim, part, reflectionOutput);
                            pss[part].field_2743 = true;
                            pss[part].field_2744 = new AtomType[] { reflected };
                        }
                        else if (pss[part].field_2743)
                        {
                            Brimstone.API.AddAtom(sim, part, reflectionOutput, pss[part].field_2744[0]);
                        }
                    } else if (type == class_191.field_1775)  //is triplex bonder
                    {
                        bool fullBonder = true;
                        List<AtomType> foundAtoms = new();
                        List<HexIndex> checkInputs = new();

                        foreach (class_222 bonder in type.field_1538)
                        {
                            HexIndex leftInput = part.method_1184(bonder.field_1920);
                            HexIndex rightInput = part.method_1184(bonder.field_1921);
                            if (!checkInputs.Contains(leftInput))
                            {
                                checkInputs.Add(leftInput);
                            }
                            if (!checkInputs.Contains(rightInput))
                            {
                                checkInputs.Add(rightInput);
                            }
                        }

                        foreach (HexIndex input in checkInputs)
                        {
                            
                            if (!sim.FindAtom(input).method_99(out AtomReference leftAtom))
                            {
                                fullBonder = false;
                                break;
                            }
                            if (!ReflectionChecker.isFano(leftAtom.field_2280))
                            {
                                fullBonder = false;
                                break;
                            }
                            foundAtoms.Add(leftAtom.field_2280);
                        }

                        if (!fullBonder)
                        {
                            continue;
                        }
                        Logger.Log("all atoms were found");

                        Logger.Log(foundAtoms[0]);
                        Logger.Log(foundAtoms[1]);
                        Logger.Log(foundAtoms[2]);

                        if (ReflectionChecker.GetReflectedAtom(foundAtoms[0], foundAtoms[1]) != foundAtoms[2])
                        {
                            Logger.Log(ReflectionChecker.GetReflectedAtom(foundAtoms[0], foundAtoms[1]));
                            continue;
                        }
                        Logger.Log("all atoms were fano");

                        foreach (class_222 bonder in type.field_1538)
                        {
                            HexIndex leftInput = part.method_1184(bonder.field_1920);
                            HexIndex rightInput = part.method_1184(bonder.field_1921);
                            Logger.Log("finding atoms to bond");
                            if (sim.FindAtom(leftInput).method_99(out AtomReference leftAtom) && sim.FindAtom(rightInput).method_99(out AtomReference rightAtom))
                            {
                                Logger.Log("the names bond, triplex bond");
                                Brimstone.API.JoinMolecules(sim, leftAtom.field_2277, rightAtom.field_2277, out Molecule joined);
                                Brimstone.API.AddBond(sim, joined, leftInput, rightInput, enum_126.Prisma0);
                                Brimstone.API.AddBond(sim, joined, leftInput, rightInput, enum_126.Prisma1);
                                Brimstone.API.AddBond(sim, joined, leftInput, rightInput, enum_126.Prisma2);
                            }
                        }
                    }
                }
            });
        }
    }
}
