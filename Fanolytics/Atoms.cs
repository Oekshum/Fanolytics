using Quintessential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanolytics
{
    internal static class Atoms
    {
        public static AtomType FanoA, FanoB, FanoC, FanoD, FanoE, FanoF, FanoG;
        public static void AddAtomTypes ()
        {
            FanoA = Brimstone.API.CreateNormalAtom(21, "Fanolytics", "Carium", Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/carium_symbol"), Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/carium_diffuse"), class_238.field_1989.field_81.field_599, Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/carium_shade"));

            FanoB = Brimstone.API.CreateNormalAtom(22, "Fanolytics", "Horton", Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/horton_symbol"), Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/horton_diffuse"), class_238.field_1989.field_81.field_599, Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/horton_shade"));

            FanoC = Brimstone.API.CreateNormalAtom(23, "Fanolytics", "Knuth", Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/knuth_symbol"), Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/knuth_diffuse"), class_238.field_1989.field_81.field_599, Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/knuth_shade"));

            FanoD = Brimstone.API.CreateNormalAtom(24, "Fanolytics", "Coxeter", Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/coxeter_symbol"), Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/coxeter_diffuse"), class_238.field_1989.field_81.field_599, Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/coxeter_shade"));

            FanoE = Brimstone.API.CreateNormalAtom(25, "Fanolytics", "Fano", Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/fano_symbol"), Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/fano_diffuse"), class_238.field_1989.field_81.field_599, class_238.field_1989.field_81.field_597);

            FanoF = Brimstone.API.CreateNormalAtom(26, "Fanolytics", "Sanders", Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/sanders_symbol"), Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/sanders_diffuse"), class_238.field_1989.field_81.field_599, class_238.field_1989.field_81.field_597);

            FanoG = Brimstone.API.CreateNormalAtom(27, "Fanolytics", "Klein", Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/klein_symbol"), Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/klein_diffuse"), class_238.field_1989.field_81.field_599, Brimstone.API.GetTexture("textures/atoms/oekshum/fanolytics/klein_shade"));

            QApi.AddAtomType(FanoA);
            QApi.AddAtomType(FanoB);
            QApi.AddAtomType(FanoC);
            QApi.AddAtomType(FanoD);
            QApi.AddAtomType(FanoE);
            QApi.AddAtomType(FanoF);
            QApi.AddAtomType(FanoG);
        }
    }
}
