using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.Barcode;

namespace KdsLibrary.Utils
{
    public static class ClBarcode
    {

        public static string GetUrlBarcode(string Key, int Height, int Weigh)
        {
            Barcode.wsBarCode Bc = new wsBarCode();
            return Bc.GenerateBarCodeNoTag(Key, Height, Weigh);

        }

    }
}
