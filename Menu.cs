﻿using EsercizioCar.DataManager;
using EsercizioCar.StoreData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsercizioCar
{
    internal class Menu
    {
        public bool close = false;

        CarList carList = new CarList();

        CarManager carManager = new CarManager();
        public void openMenu()
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine("======== Benvenuto ========");
                Console.WriteLine("");
                while (!close)
                {
                    Console.WriteLine("Scegli cosa vuoi fare");
                    Console.WriteLine("1) Inserisci automobile\n2) Modifica automobile\n3) Elimina automobile\n4) Cerca automobile\n5) Visualizza tutti le automobile disponibili\n6) Esporta auto inserite\n7) Esci\n8) Export xml\n9) Export Json");
                    string userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out int userChoose))
                    {
                        if (userChoose <= 9 && userChoose > 0)
                        {
                            if (userChoose == 1)
                            {
                                Console.Clear();
                                carManager.CreateCar(carList.carList);
                            }
                            else if (userChoose == 2)
                            {
                                carManager.EditCar(carList.carList);
                            }
                            else if (userChoose == 3)
                            {
                                carManager.DeleteCar(carList.carList);
                            }
                            else if (userChoose == 4)
                            {
                                carManager.SearchCar(carList.carList);
                            }
                            else if (userChoose == 5)
                            {
                                carManager.PrintCar(carList.carList);
                            }
                            else if (userChoose == 6)
                            {
                                carManager.ExportCar(carList.carList);
                            }
                            else if (userChoose == 7)
                            {
                                close = true;
                            }
                            else if (userChoose == 8)
                            {
                                carManager.ExportXml(carList.carList);
                            }
                            else if (userChoose == 9)
                            {
                                carManager.ExportJson(carList.carList);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
