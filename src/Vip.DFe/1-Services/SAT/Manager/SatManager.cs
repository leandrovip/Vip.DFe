using System;
using System.Text;
using Vip.DFe.SAT.Configuration;
using Vip.DFe.SAT.Enum;
using Vip.DFe.SAT.Interfaces;

namespace Vip.DFe.SAT.Manager
{
    public static class SatManager
    {
        public static ISatLibrary GetLibrary(ModeloSat modelo, SatConfig config, string pathDll, Encoding encoding)
        {
            switch (modelo)
            {
                case ModeloSat.Cdecl: return new SatCdecl(config, pathDll, encoding);
                case ModeloSat.StdCall: return new SatStdCall(config, pathDll, encoding);
                default: throw new NotImplementedException("Modelo não implementado!");
            }
        }
    }
}