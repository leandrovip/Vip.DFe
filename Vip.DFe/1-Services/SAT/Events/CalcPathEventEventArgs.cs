using System;

namespace Vip.DFe.SAT.Events
{
    public sealed class CalcPathEventEventArgs : EventArgs
    {
        public string Path { get; set; }
        public string CNPJ { get; set; }
        public DateTime Data { get; set; }
    }
}