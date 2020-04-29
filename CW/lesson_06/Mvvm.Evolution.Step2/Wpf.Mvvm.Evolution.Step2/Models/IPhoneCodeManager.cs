using System.Collections.Generic;

namespace Wpf.Mvvm.Evolution.Step2.Models
{
    internal interface IPhoneCodeManager
    {
        IEnumerable<string> PhoneCodes { get; }

        string GetAreaByPhoneCode(string phoneCode);
    }
}