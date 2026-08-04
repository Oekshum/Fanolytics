using Quintessential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanolytics
{
    internal static class ReflectionChecker
    {
        public static readonly AtomType[] FanoAtoms = { Atoms.FanoA, Atoms.FanoB, Atoms.FanoC, Atoms.FanoD, Atoms.FanoE, Atoms.FanoF, Atoms.FanoG };
        internal struct FanoLine
        {
            public AtomType Point1, Point2, Point3;
        }

        public static readonly FanoLine[] Lines =
        {
            new FanoLine() { Point1 = Atoms.FanoA, Point2 = Atoms.FanoB, Point3 = Atoms.FanoC },
            new FanoLine() { Point1 = Atoms.FanoA, Point2 = Atoms.FanoD, Point3 = Atoms.FanoG },
            new FanoLine() { Point1 = Atoms.FanoA, Point2 = Atoms.FanoE, Point3 = Atoms.FanoF },
            new FanoLine() { Point1 = Atoms.FanoB, Point2 = Atoms.FanoD, Point3 = Atoms.FanoF },
            new FanoLine() { Point1 = Atoms.FanoB, Point2 = Atoms.FanoE, Point3 = Atoms.FanoG },
            new FanoLine() { Point1 = Atoms.FanoC, Point2 = Atoms.FanoD, Point3 = Atoms.FanoE },
            new FanoLine() { Point1 = Atoms.FanoC, Point2 = Atoms.FanoF, Point3 = Atoms.FanoG },

        };

        public static AtomType GetReflectedAtom(AtomType subject, AtomType mirror)
        {
            if (subject == mirror) {
                return subject;
            }


            foreach (var line in Lines)
            {
                if ((subject == line.Point1 && mirror == line.Point2) || (subject == line.Point2 && mirror == line.Point1))
                {
                    return line.Point3;
                }
                if ((subject == line.Point1 && mirror == line.Point3) || (subject == line.Point3 && mirror == line.Point1))
                {
                    return line.Point2;
                }
                if ((subject == line.Point3 && mirror == line.Point2) || (subject == line.Point2 && mirror == line.Point3))
                {
                    return line.Point1;
                }
            }
            return null;
        }

        public static bool isFano(AtomType type)
        {
            foreach (var a in FanoAtoms)
            {
                if (type == a)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
