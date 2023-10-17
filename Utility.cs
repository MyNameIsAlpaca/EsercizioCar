using EsercizioCar.StoreData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EsercizioCar
{
    internal class Utility
    {
        public void errorMessage(string param)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(param);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, Console.CursorTop - 2);
        }

        public void ExportXml(List<Car> carList)
        {

            var serializer = new XmlSerializer(typeof(List<Car>));

            using (var writer = new StreamWriter("C:\\Users\\Betacom\\Documents\\Betacom\\Progetti\\EsercizioCar\\ExportCar.xml"))
            {
                serializer.Serialize(writer, carList);
            }

        }

    }
}
