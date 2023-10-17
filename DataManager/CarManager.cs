using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EsercizioCar.StoreData;
using System.Xml;
using System.Xml.Serialization;

namespace EsercizioCar.DataManager
{
    internal class CarManager
    {
        Utility utility = new Utility();
        bool close = false;
        static int idCarS;
        static int idCarC;
        public string idCar;

        public void CreateCar(List<Car> carList)
        {
            while (!close)
            {
                Console.WriteLine("");
                Console.WriteLine("== Aggiunta automobile ==");
                Console.WriteLine("");
                Console.WriteLine("Come vuoi inserire una nuova automobile?\n1) Aggiunta manuale\n2) Aggiunta automatica\n3) Aggiungi automaticamente un file con le caratteristiche");
                Console.WriteLine("Oppure inserisci f per tornare indietro");

                string userType = Console.ReadLine();

                if (userType.ToLower() == "f")
                {
                    Console.Clear();
                    break;
                }

                if (int.TryParse(userType, out int userChoose))
                {
                    if (userChoose == 1 || userChoose == 2 || userChoose == 3)
                    {
                        if (userChoose == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Inserisci il numero della matricola");
                            Console.WriteLine("Oppure inserisci f per annullare la creazione");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Es. ABC12345");
                            Console.ForegroundColor = ConsoleColor.White;

                            while (!close)
                            {
                                Console.WriteLine("Inserisci la prima parte della matricola composta da 3 lettere");
                                while (!close)
                                {
                                string firstPartId = Console.ReadLine();

                                if(firstPartId.ToLower() == "f")
                                    {
                                        Console.Clear();
                                        return;
                                    }

                                if (firstPartId.Length != 3)
                                {
                                        utility.errorMessage("La prima parte della matricola è composta da 3 lettere");
                                    }
                                else
                                {
                                    Console.WriteLine("Inserisci la seconda parte della matricola è composta da 5 numeri");
                                    while (!close)
                                    {
                                        if (int.TryParse(Console.ReadLine(), out int secondPartOfId))
                                            {
                                            if (secondPartOfId.ToString().Length == 5)
                                            {
                                                idCar = firstPartId + secondPartOfId;
                                                close = true;
                                            }
                                            else
                                            {
                                                utility.errorMessage("La seconda parte della matricola è composta da 5 numeri");
                                            }
                                        }
                                        else
                                        {
                                            utility.errorMessage("Seconda parte del telaio errata, riprova");
                                            }
                                        }

                                }
                                }
                                Car[] carCheck = carList.Where(r => r.Id.ToLower() == idCar).ToArray();
                                if (carCheck.Length != 0)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Attenzione! L'auto è già presente nel database");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.ReadLine();
                                    break;
                                }
                                close = false;
                                Console.WriteLine($"Inserisci la marca dell'auto{new string(' ', 40)}");

                                string companyCar = string.Empty;

                                while (!close)
                                {
                                    companyCar = Console.ReadLine();
                                    if(companyCar.Length > 2)
                                    {
                                        close = true;
                                    }
                                    else
                                    {
                                        utility.errorMessage("La marca non è valida");
                                    }
                                }
                                close = false;

                                Console.WriteLine($"Inserisci il modello dell'auto {new string(' ', 40)}");

                                string ModelCar = string.Empty;

                                while (!close)
                                {
                                    ModelCar = Console.ReadLine();   

                                    if (ModelCar.Length >= 1)
                                    {
                                        close = true;
                                    }
                                    else
                                    {
                                        utility.errorMessage("Il modello non è valido");
                                    }
                                }
                                close = false;

                                Console.WriteLine($"Inserisci l'anno dell'auto {new string(' ', 40)}");

                                int YearCar;

                                while (!close)
                                {
                                    string yearChoose = Console.ReadLine();
                                    if (int.TryParse(yearChoose, out int userYear))
                                    {
                                        if(userYear > 1950 && userYear < 2023)
                                        {

                                        YearCar = userYear;

                                        idCarC = idCarC + 1;

                                        int idChar = idCarC;

                                        Console.WriteLine($"Inserisci il tipo di alimentazione {new string(' ', 40)}");
                                        string Fuel = Console.ReadLine();

                                        Console.WriteLine($"Inserisci la cilindrata {new string(' ', 40)}");
                                        int Engine;

                                            close = false;

                                        while (!close)
                                        {
                                            if (int.TryParse(Console.ReadLine(), out int userEngine))
                                            {
                                                Engine = userEngine;

                                                CarCharateristic createCharateristic = new CarCharateristic(Fuel, Engine, idCar, idChar);
                                                Car createCar = new Car(idCar, companyCar, ModelCar, YearCar, carList, createCharateristic);
                                                close = true;

                                                Console.Clear();

                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Auto aggiunta con successo");
                                                Console.ForegroundColor = ConsoleColor.White;

                                            }

                                            else
                                            {
                                                utility.errorMessage("Inserisci una cilindrata valida");
                                            }
                                        }
                                        }
                                            else
                                            {
                                                utility.errorMessage("Inserisci una data valida");
                                            }


                                    }
                                    else
                                    {
                                        utility.errorMessage("Inserisci una data valida");
                                    }
                                }
                            }
                            close = false;
                        }
                        else if(userChoose == 2)
                        {
                            Console.Clear();
                            try
                            {
                            string[] carToImport = File.ReadAllLines("C:\\Users\\Betacom\\Documents\\Betacom\\Progetti\\EsercizioCar\\DataImport\\Cars.txt");

                            int importCarIndex = 1;

                                List<int> emptyRow = new List<int>();

                                List<int> wrongCar = new List<int>();

                                int carImported = 0;

                            if(carToImport.Length != 0)
                            {
                                foreach(string car in carToImport)
                                {
                                    if(car.Length != 0)
                                    {
                                    string[] carToAdd = car.Split(':');

                                        if(carToAdd.Length == 4)
                                        {
                                           var CarNeedAdd = carList.SingleOrDefault(r => r.Id.ToLower() == carToAdd[0]);

                                           if (CarNeedAdd != null)
                                           {
                                                    Console.Clear();
                                                    Console.WriteLine($"L'auto con matricola {carToAdd[0]} è già presente nel database");
                                                    Console.ReadLine();
                                           }
                                           else
                                           {
                                            string idCar = carToAdd[0];

                                            string CompanyCar = carToAdd[1];

                                            string ModelCar = carToAdd[2];

                                            if (int.TryParse(carToAdd[3], out int yearValue))
                                            {

                                                int YearCar = yearValue;

                                                Car createCar = new Car(idCar, CompanyCar, ModelCar, YearCar, carList);

                                                carImported++;

                                            }
                                           }

                                        }
                                        else
                                        {
                                                wrongCar.Add(importCarIndex);
                                        }
                                    }
                                    else
                                    {
                                            emptyRow.Add(importCarIndex);  
                                    }
                                    importCarIndex++;
                                }
                                    if(wrongCar.Count > 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;

                                        Console.WriteLine($"Non è stato possibile leggere la macchina a riga:");
                                        Console.SetCursorPosition(50, Console.CursorTop - 1);
                                        foreach (var car in wrongCar)
                                        {
                                            Console.WriteLine($"{car} ");
                                        }

                                        Console.ForegroundColor = ConsoleColor.White;

                                    }

                                    if (emptyRow.Count > 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;

                                        Console.WriteLine($"Sono state trovate delle righe vuote nelle posizioni:");

                                        Console.SetCursorPosition(54, Console.CursorTop - 1);

                                        foreach (var row in emptyRow)
                                        {
                                            Console.WriteLine($"{row}");
                                        }
                                        
                                        Console.ForegroundColor = ConsoleColor.White;

                                    }
                                    if(carImported == 0)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Non hai importato nessuna macchina");
                                    }
                                    else
                                    {

                                        Console.WriteLine("Hai a disposizione anche un file con le caratteristiche?\n1)Si\n2)No");

                                        while (!close)
                                        {
                                            if (int.TryParse(Console.ReadLine(), out int userRes))
                                            {
                                                if(userRes == 1)
                                                {
                                                        try
                                                        {
                                                            string[] characteristicsToImport = File.ReadAllLines("C:\\Users\\Betacom\\Documents\\Betacom\\Progetti\\EsercizioCar\\DataImport\\CarsCharacteristics.txt");
                                                            if(characteristicsToImport.Length != 0)
                                                            {
                                                                foreach(string characteristics in characteristicsToImport)
                                                                {
                                                                    if(characteristics.Length != 0)
                                                                    {
                                                                        string[] characteristicsToAdd = characteristics.Split(':');

                                                                        var CarNeedAddChar = carList.SingleOrDefault(r => r.Id.ToLower() == characteristicsToAdd[1].ToLower());
                                                                        if (CarNeedAddChar != null)
                                                                        {
                                                                            string Fuel = characteristicsToAdd[3];

                                                                            string originalCardId = characteristicsToAdd[1];

                                                                            if (int.TryParse(characteristicsToAdd[2], out int Engine))
                                                                            {
                                                                                if (int.TryParse(characteristicsToAdd[0], out int id))
                                                                                {

                                                                                    CarCharateristic importCharateristic = new CarCharateristic(characteristicsToAdd[3], Engine, characteristicsToAdd[1], id);

                                                                                    CarNeedAddChar.carCharateristics[0] = importCharateristic;

                                                                                    close = true;
                                                                                    Console.Clear();
                                                                            
                                                                                }

                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                                            Console.WriteLine("L'id inserito non corrisponde a nessuna auto");
                                                                            Console.ForegroundColor = ConsoleColor.White;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("Non è stato trovato alcun file con le caratteristiche");
                                                            Console.ForegroundColor = ConsoleColor.White;
                                                            Console.WriteLine("Premi invio per continuare");
                                                            Console.ReadLine();
                                                            close = true;
                                                        }
                                                    }
                                                else
                                                {
                                                    close = true;
                                                }
                                            }
                                        }
                                        close = false;
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"Sono state importate {carImported} auto correttamente");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }


                                }

                            }
                            catch(Exception ex)
                            {
                               Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Non è stato trovato alcun file contenente le auto da importare");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Premi invio per continuare");
                                Console.ReadLine();
                                Console.Clear();
                            }

                        }
                        else if (userChoose == 3)
                        {
                            Console.Clear();

                            try
                            {
                                int counChar = 0;
                                string[] characteristicsToImport = File.ReadAllLines("C:\\Users\\Betacom\\Documents\\Betacom\\Progetti\\EsercizioCar\\DataImport\\CarsCharacteristics.txt");
                                if (characteristicsToImport.Length != 0)
                                {
                                    List<int> NotAddChar = new List<int>();
                                    foreach (string characteristics in characteristicsToImport)
                                    {
                                        counChar++;
                                        if (characteristics.Length != 0)
                                        {
                                            string[] characteristicsToAdd = characteristics.Split(':');

                                            var CarNeedAddChar = carList.SingleOrDefault(r => r.Id.ToLower() == characteristicsToAdd[1].ToLower());
                                            if (CarNeedAddChar != null)
                                            {
                                                string Fuel = characteristicsToAdd[3];

                                                string originalCardId = characteristicsToAdd[1];

                                                if (int.TryParse(characteristicsToAdd[2], out int Engine))
                                                {
                                                    if (int.TryParse(characteristicsToAdd[0], out int id))
                                                    {

                                                        CarCharateristic importCharateristic = new CarCharateristic(characteristicsToAdd[3], Engine, characteristicsToAdd[1], id);

                                                        CarNeedAddChar.carCharateristics[0] = importCharateristic;

                                                        close = true;
                                                        Console.Clear();
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine("Caratteristiche aggiunte con successo");
                                                        Console.ForegroundColor = ConsoleColor.White;

                                                    }

                                                }
                                            }
                                            else
                                            {
                                                NotAddChar.Add(counChar);
                                            }
                                        }
                                    }
                                Console.ForegroundColor = ConsoleColor.Red;
                                    if(NotAddChar.Count > 0)
                                    {
                                    Console.WriteLine($"Non è stato possibile aggiungere le caratteristiche a riga: ");
                                    foreach(int notAdd in NotAddChar)
                                        {
                                            Console.WriteLine(notAdd);
                                        }

                                    }
                                Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Premi invio per continuare");
                                    Console.ReadLine();
                                    Console.Clear();
                                }

                            }
                            catch
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Non è stato trovato alcun file con le caratteristiche");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Premi invio per continuare");
                                Console.ReadLine();
                                close = true;
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Scegli un'opzione valida");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Scegli un'opzione valida");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            close = false;
        }

        public void DeleteCar(List<Car> carList)
        {
            while (!close)
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("== Eliminazione automobile ==");
                Console.WriteLine("");
                Console.WriteLine("Inserisci l'id dell'auto che vuoi eliminare");
                Console.WriteLine("Oppure inserisci f per uscire");
                string userChoose = Console.ReadLine().ToLower();
                if (userChoose == "f")
                {
                    close = true;
                    Console.Clear();
                }
                else
                {
                    var CarNeedRemove = carList.SingleOrDefault(r => r.Id.ToLower() == userChoose);
                    if (CarNeedRemove != null)
                    {
                        carList.Remove(CarNeedRemove);
                        Console.Clear();
                        close = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Veicolo rimosso con successo");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("L'id inserito non corrisponde a nessuna auto");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }

            }
            close = false;
        }

        public void SearchCar(List<Car> carList)
        {
            Console.Clear();
            while (!close)
            {
                Console.WriteLine("");
                Console.WriteLine("== Ricerca automobile ==");
                Console.WriteLine("");
                Console.WriteLine("Vuoi cercare un auto tramite:");
                Console.WriteLine("1) Marca di auto\n2) Anno di produzione\n oppure premi f per uscire");
                string userType = Console.ReadLine();
                if (int.TryParse(userType, out int userChoose))
                {
                    if(userChoose == 1)
                    {
                        Console.Clear();

                        Console.WriteLine("");
                        Console.WriteLine("== Ricerca automobile ==");
                        Console.WriteLine("");

                        Console.WriteLine("Inserisci la marca da ricercare");

                        userType = Console.ReadLine().ToLower();

                        Car[] carToPrint = carList.Where(r => r.Company.ToLower() == userType).ToArray();


                        if (carToPrint.Length != 0)
                        {
                            Console.Clear();
                            foreach (var car in carToPrint)
                            {
                                if(car.carCharateristics[0] != null)
                                {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Matricola: {car.Id}, Azienda: {car.Company}, Modello: {car.Model}, Anno di produzione: {car.ProductYear}, Alimentazione: {car.carCharateristics[0].CarFuel}, Cilindrata: {car.carCharateristics[0].CarEngine}");
                                Console.ForegroundColor = ConsoleColor.White;

                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Matricola: {car.Id}, Azienda: {car.Company}, Modello: {car.Model}, Anno di produzione: {car.ProductYear}, Alimentazione: N/D, Cilindrata: N/D");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                            }
                            Console.WriteLine("Premi invio per continuare");
                            Console.ReadLine();
                            Console.Clear();

                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nessuna auto disponibile di quell'azienda");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Premi invio per continuare");
                            Console.ReadLine();
                            Console.Clear();

                        }
                    }
                    else
                    {
                        Console.Clear();

                        Console.WriteLine("");
                        Console.WriteLine("== Ricerca automobile ==");
                        Console.WriteLine("");

                        Console.WriteLine("Inserisci l'anno da ricercare");

                        userType = Console.ReadLine();

                        if (int.TryParse(userType, out int yearSearch))
                        {

                            Car[] carToPrint = carList.Where(r => r.ProductYear == yearSearch).ToArray();


                                if (carToPrint.Length != 0)
                            {
                                Console.Clear();
                                foreach (var car in carToPrint)
                                {

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Matricola: {car.Id}, Azienda: {car.Company}, Modello: {car.Model}, Anno di produzione: {car.ProductYear}, Alimentazione: {car.carCharateristics[0].CarFuel}, Cilindrata: {car.carCharateristics[0].CarEngine}");
                                Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("Premi invio per continuare");
                                Console.ReadLine();
                                Console.Clear();

                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Nessuna auto disponibile in quell'anno");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Premi invio per continuare");
                                Console.ReadLine();
                                Console.Clear();

                            }
                        }

                    }
                }
                else
                {
                    if(userType.ToLower() == "f")
                    {
                        Console.Clear();
                        close = true;
                    }
                    else
                    {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Scegli un'opzione valide");
                    Console.ForegroundColor = ConsoleColor.White;
                    }
                }

            }
            close = false;

        }

        public void PrintCar(List<Car> carList)
        {
            Console.Clear();
            if (carList.Count > 0)
            {
                foreach (Car car in carList)
                {
                    if (car.carCharateristics[0] != null)
                    {

                    Console.WriteLine($"ID: {car.Id}, Azienda: {car.Company}, Modello: {car.Model}, Anno di produzione: {car.ProductYear}, Alimentazione: {car.carCharateristics[0].CarFuel}");
                    }
                    else
                    {
                        Console.WriteLine($"ID: {car.Id}, Azienda: {car.Company}, Modello: {car.Model}, Anno di produzione: {car.ProductYear}, Alimentazione: N/D");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nessuna automobile disponibile");
            }
            Console.WriteLine("Premi invio per continuare");
            Console.ReadLine();
            Console.Clear();
        }

        public void EditCar(List<Car> carList)
        {
            Console.Clear();
            while (!close)
            {
                Console.WriteLine("");
                Console.WriteLine("== Modifica automobile ==");
                Console.WriteLine("");

                Console.WriteLine("Inserisci la matricola dell'auto che vuoi modificare");
                Console.WriteLine("Oppure inserisci f per uscire");
                string userChoose = Console.ReadLine().ToLower();
                if (userChoose.ToLower() == "f")
                {
                    close = true;
                    Console.Clear();
                }
                else
                {
                    var CarNeedToUpdate = carList.SingleOrDefault(r => r.Id.ToLower() == userChoose);
                    if (CarNeedToUpdate != null)
                    {
                        while (!close)
                        {
                            Console.Clear();
                            Console.WriteLine("Inserisci la prima parte della matricola composta da 3 lettere");
                            while (!close)
                            {
                                string firstPartId = Console.ReadLine();

                                if (firstPartId.Length != 3 && firstPartId.Length != 0)
                                {
                                    utility.errorMessage("La prima parte della matricola è composta da 3 lettere");
                                }
                                else
                                {
                                    if (firstPartId.Length == 0)
                                    {
                                        firstPartId = CarNeedToUpdate.Id.Substring(0, 3);
                                    }
                                        Console.WriteLine($"Inserisci la seconda parte della matricola composta da 5 numeri {new string(' ', 40)}");
                                    while (!close)
                                    {
                                        string updateSecondString = Console.ReadLine();
                                        if (int.TryParse(updateSecondString, out int secondPartOfId) || updateSecondString == "")
                                        {
                                            if (secondPartOfId.ToString().Length == 5 || secondPartOfId.ToString().Length == 0)
                                            {
                                                if (secondPartOfId == 0)
                                                {
                                                    int oldValue = int.Parse(CarNeedToUpdate.Id.Substring(3));
                                                    secondPartOfId = oldValue;
                                                }
                                                else
                                                {
                                                    idCar = firstPartId + secondPartOfId;

                                                }
                                                close = true;
                                            }
                                            else
                                            {
                                                utility.errorMessage("La seconda parte della matricola deve essere di 5 caratteri");
                                            }
                                        }
                                        else
                                        {
                                            utility.errorMessage("La seconda parte della matricola deve essere di 5 caratteri");
                                        }
                                    }

                                }
                            }
                            close = false;

                            Console.WriteLine($"Inserisci la marca dell'auto{new string(' ', 40)}");

                            string CompanyCar = Console.ReadLine();

                            if (CompanyCar == "")
                            {
                                CompanyCar = CarNeedToUpdate.Company;
                            }


                            Console.WriteLine("Inserisci il modello dell'auto");
                            string ModelCar = Console.ReadLine();

                            if (ModelCar == "")
                            {
                                ModelCar = CarNeedToUpdate.Model;
                            }

                            Console.WriteLine($"Inserisci l'anno dell'auto {new string(' ', 40)}");
                            int YearCarUpdate;

                            while (!close)
                            {
                                string yearInp = Console.ReadLine();

                                if (int.TryParse(yearInp, out int userYear) || yearInp == "")
                                {
                                    if (yearInp == "")
                                    {
                                        YearCarUpdate = CarNeedToUpdate.ProductYear;
                                    }
                                    else
                                    {
                                        YearCarUpdate = userYear;

                                    }
                                    idCarC = idCarC + 1;
                                    int idChar = idCarC;

                                    Console.WriteLine("Inserisci il tipo di alimentazione");
                                    string Fuel = Console.ReadLine();

                                    Console.WriteLine("Inserisci la cilindrata");
                                    int Engine;

                                    while (!close)
                                    {
                                        if (int.TryParse(Console.ReadLine(), out int userEngine))
                                        {
                                            Engine = userEngine;
                                            carList.Remove(CarNeedToUpdate);
                                            CarCharateristic createCharateristic = new CarCharateristic(Fuel, Engine, idCar, idChar);
                                            Car createCar = new Car(idCar, CompanyCar, ModelCar, YearCarUpdate, carList, createCharateristic);
                                            close = true;

                                            Console.Clear();

                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Auto aggiornata con successo");
                                            Console.ForegroundColor = ConsoleColor.White;

                                        }
                                        else
                                        {
                                            utility.errorMessage("Inserisci una cilidrata valida");
                                        }
                                    }
                                }
                                else
                                {
                                    utility.errorMessage("Inserisci una data valida");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("La matricola inserita non corrisponde a nessuna auto");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                
            }
            close = false;
        }

        public void ExportCar(List<Car> carList)
        {
            Console.Clear();

            string exportPath = "C:\\Users\\Betacom\\Documents\\Betacom\\Progetti\\EsercizioCar\\DataSave\\CarUpdate.txt";

            List<string> carExported = new List<string>();

            try
            {
                if(carList.Count  > 0)
                {
                    foreach (Car car in carList)
                    {
                        if(car.carCharateristics[0] != null)
                        {
                            carExported.Add($"{car.Id},{car.Company},{car.Model},{car.ProductYear},{car.carCharateristics[0].CarFuel},{car.carCharateristics[0].CarEngine};");

                        }
                        else
                        {
                        carExported.Add( $"{car.Id},{car.Company},{car.Model},{car.ProductYear},N/D,N/D;");

                        }
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(exportPath)))
                        {
                            foreach (string line in carExported)
                                outputFile.WriteLine(line);
                        }
                    }
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine($"Sono state esportate correttamente {carExported.Count} auto");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Non sono presenti auto da esportare");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Errore riscontrato: {ex}");
            };

           


        }
    }
}
