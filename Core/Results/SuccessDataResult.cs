using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        // Veri + Başarı Mesajı Döndürmek İçin
        public SuccessDataResult(T data, string message) : base(data, message, true)
        {
        }

        // Sadece Veri Döndürmek İçin
        public SuccessDataResult(T data) : base(data, true)
        {
        }
    }
}
