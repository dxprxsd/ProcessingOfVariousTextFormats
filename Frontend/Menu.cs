using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Text;

public class Menu
{
    private bool _isRunning;
    private readonly List<string> _helpLines = new List<string>
    {
        "[HELP] Поддерживаемые команды:",
        "show <model> - показывает текущий список для каждой модели.",
        "clear <model> - очищает список для указанной модели.",
        "read <model> <file_name> - добавляет данные из файла в коллекцию указанной модели.",
        "write <model> <file_name> - сохраняет данные коллекции указанной модели в файл.",
        "add <model> - создать элемент указанной модели и добавить его в коллекцию.",
        "sort <model> <up/down> - отсортировать коллекцию указанной модели в порядке возрастания (up) или убывания (down)." +
        "\n\t\t\t Параметр сортировки: Person -> Age, Building -> Owner, Car -> Horsepower",
        "search <model> <value> - поиск значения в коллекции указанной модели.\n" +
        "\t\t\t Параметр поиска: Person -> Name, Building -> Owner, Car -> Manufacturer",
        "\n\n"
    };
    private readonly List<string> _allowedModels = new List<string>
    {
        "person", "building", "car"
    };

    public List<Person> personsCollection;
    public List<Building> buildingsCollection;
    public List<Car> carsCollection;
    // todo check field access
    private FileHandlersManager fileHandlersManager;

    public Menu()
    {
        _isRunning = true;
        this.personsCollection = new List<Person>();
        this.buildingsCollection = new List<Building>();
        this.carsCollection = new List<Car>();
        this.fileHandlersManager = new FileHandlersManager();
    }

    public void OnStartUp()
    {
        Console.WriteLine("//-- ОБРАБОТКА РАЗЛИЧНЫХ ТЕКСТОВЫХ ФОРМАТОВ --\\\n");
        Console.WriteLine("Поддерживаемые форматы для хранения моделей: JSON, YAML, XML, CSV.\n");
        Console.WriteLine("Существующие модели:");
        Console.WriteLine("Person {\nName: string;\nAge: int\n}\n");
        Console.WriteLine("Car {\nManufacturer: string;\nModel: string;\nYear: int;\nHorsepower: int;\nMaxSpeed: int\n}\n");
        Console.WriteLine("Building {\nOwner: string;\nAddress: string;\nPositionX: double;\nPositionY: double;\nYear: int\n}\n\n\n");

        HandleHelpCommand();

        Console.WriteLine(new string('*', 50));


        // Persons: person1, person2...

    }

    public void DisplayMenu()
    {
        while (_isRunning)
        {
            string command = Console.ReadLine();
            HandleCommand(command);
        }
    }

    public void HandleCommand(string fullCommand)
    {
        string[] commandPieces = fullCommand.Split();
        string cmd = commandPieces[0];

        switch (cmd)
        {
            case "help":
                HandleHelpCommand();
                break;
            case "show":
                HandleShowCommand(commandPieces);
                break;
            case "clear":
                HandleClearCommand(commandPieces);
                break;
            case "read":
                HandleReadCommand(commandPieces);
                break;
            case "write":
                HandleWriteCommand(commandPieces);
                break;
            case "add":
                HandleAddElementCommand(commandPieces);
                break;
            case "sort":
                HandleSortCommand(commandPieces);
                break;
            case "search":
                HandleSearchCommand(commandPieces);
                break;
            default:
                HandleUnsupportedCommand();
                break;
        }
    }

    public void HandleUnsupportedCommand()
    {
        Console.WriteLine("Ошибка. Неподдерживаемая команда.");
    }

    public void HandleHelpCommand()
    {
        for (int i = 0; i < _helpLines.Count(); i++)
        {
            Console.WriteLine(_helpLines[i]);
        }
    }

    public void HandleShowCommand(string[] commandPieces)
    {
        if (commandPieces.Length != 2)
        {
            Console.WriteLine("Ошибка. Неверное количество параметров к команде.\n");
            return;
        }

        string model = commandPieces[1].ToLower();

        if (!_allowedModels.Contains(model))
        {
            Console.WriteLine("Ошибка. Указана неподдерживаемая модель.\n");
            return;
        }


        IEnumerable<object> collection = null;

        switch (model)
        {
            case "person":
                collection = personsCollection.Cast<object>();
                break;
            case "building":
                collection = buildingsCollection.Cast<object>();
                break;
            case "car":
                collection = carsCollection.Cast<object>();
                break;
        }

        if (collection != null && collection.Any())
        {
            Console.WriteLine($"Элементы коллекции {model}:");
            for (int i = 0; i < collection.Count(); i++)
            {
                Console.WriteLine($"[{i}]");
                Console.WriteLine(collection.ElementAt(i).ToString() + "\n");

            }
        }
        else
        {
            Console.WriteLine($"Коллекция {model} не содержит элементов.\n");
        }

    }

    public void HandleClearCommand(string[] commandPieces)
    {
        if (commandPieces.Length != 2)
        {
            Console.WriteLine("Ошибка. Неверное количество параметров к команде.\n");
            return;
        }

        string model = commandPieces[1].ToLower();

        if (!_allowedModels.Contains(model))
        {
            Console.WriteLine("Ошибка. Указана неподдерживаемая модель.\n");
            return;
        }

        switch (model)
        {
            case "person":
                personsCollection.Clear();
                break;
            case "building":
                buildingsCollection.Clear();
                break;
            case "car":
                carsCollection.Clear();
                break;
        }

        Console.WriteLine($"Коллекция {model} была успешно очищена.\n");
    }

    public void HandleReadCommand(string[] commandPieces)
    {
        //  "read <model> <file_name> - добавляет данные из файла в коллекцию указанной модели.",

        if (commandPieces.Length != 3)
        {
            Console.WriteLine("Ошибка. Неверное количество параметров к команде.\n");
            return;
        }

        string model = commandPieces[1].ToLower();
        string filePath = commandPieces[2];

        if (!_allowedModels.Contains(model))
        {
            Console.WriteLine("Ошибка. Указана неподдерживаемая модель.\n");
            return;
        }

        IEnumerable<object> addedCollection = null;

        try
        {
            switch (model)
            {
                case "person":
                    List<Person> newPersonsCollection = fileHandlersManager.ReadFromFile<Person>(filePath);
                    addedCollection = newPersonsCollection;
                    personsCollection.AddRange(newPersonsCollection);
                    break;
                case "building":
                    List<Building> newBuildingsCollection = fileHandlersManager.ReadFromFile<Building>(filePath);
                    addedCollection = newBuildingsCollection;
                    buildingsCollection.AddRange(newBuildingsCollection);
                    break;
                case "car":
                    List<Car> newCarsCollection = fileHandlersManager.ReadFromFile<Car>(filePath);
                    addedCollection = newCarsCollection;
                    carsCollection.AddRange(newCarsCollection);
                    break;
            }
        }
        catch (FileHandlersManagerException ex)
        {
            Console.WriteLine($"[FAIL] Возникла ошибка при выполнении команды: {ex.Message}\n");
            return;
        }


        if (addedCollection != null && addedCollection.Any())
        {
            Console.WriteLine($"[SUCCESS] Из файла {filePath} были считаны и добавлены в коллекцию {model} следующие элементы:");
            for (int i = 0; i < addedCollection.Count(); i++)
            {
                Console.WriteLine($"[{i}]");
                Console.WriteLine(addedCollection.ElementAt(i).ToString() + "\n");

            }
        }
        else
        {
            Console.WriteLine($"[SUCCESS] Файл {filePath} был успешно прочитан, но он не содержит элементов.\n");
        }
    }

    public void HandleWriteCommand(string[] commandPieces)
    {
        // "write <model> <file_name> - сохраняет данные коллекции указанной модели в файл.",

        if (commandPieces.Length != 3)
        {
            Console.WriteLine("[FAIL] Ошибка. Неверное количество параметров к команде.\n");
            return;
        }

        string model = commandPieces[1].ToLower();
        string filePath = commandPieces[2];

        if (!_allowedModels.Contains(model))
        {
            Console.WriteLine("[FAIL] Ошибка. Указана неподдерживаемая модель.\n");
            return;
        }

        try
        {
            switch (model)
            {
                case "person":
                    fileHandlersManager.WriteToFile(filePath, personsCollection);
                    break;
                case "building":
                    fileHandlersManager.WriteToFile(filePath, buildingsCollection);
                    break;
                case "car":
                    fileHandlersManager.WriteToFile(filePath, carsCollection);
                    break;
            }
        }
        catch (FileHandlersManagerException ex)

        {
            Console.WriteLine("[FAIL] Возникла ошибка при выполнении команды.");
            Console.WriteLine(ex.Message + "\n");
            return;
        }
        /*catch (Exception ex)
        {

        }*/

        Console.WriteLine($"[SUCCESS] Коллекция {model} была сохранена в файл {filePath}.\n");
    }

    public void HandleAddElementCommand(string[] commandPieces)
    {
        // "add <model> - создать элемент указанной модели и добавить его в коллекцию."

        if (commandPieces.Length != 2)
        {
            Console.WriteLine("[FAIL] Ошибка. Неверное количество параметров к команде.\n");
            return;
        }

        string model = commandPieces[1].ToLower();

        if (!_allowedModels.Contains(model))
        {
            Console.WriteLine("[FAIL] Ошибка. Указана неподдерживаемая модель.\n");
            return;
        }

        switch (model)
        {
            case "person":
                Person newPerson = Person.CreatePersonFromConsoleInput();
                personsCollection.Add(newPerson);
                break;
            case "building":
                Building newBuilding = Building.CreateBuildingFromConsoleInput();
                buildingsCollection.Add(newBuilding);
                break;
            case "car":
                Car newCar = Car.CreateCarFromConsoleInput();
                carsCollection.Add(newCar);
                break;
        }

        Console.WriteLine($"[SUCCESS] Элемент был добавлен в коллекцию {model}.\n");
    }

    public void HandleSortCommand(string[] commandPieces)
    {
        // "sort <model> <up/down> - отсортировать коллекцию указанной модели в порядке возрастания (up) или убывания (down)." +
        //                           Параметр сортировки: Person -> Age, Building -> Owner, Car -> Horsepower",

        if (commandPieces.Length != 3)
        {
            Console.WriteLine("[FAIL] Ошибка. Неверное количество параметров к команде.\n");
            return;
        }

        string model = commandPieces[1].ToLower(); 

        if (!_allowedModels.Contains(model))
        {
            Console.WriteLine("[FAIL] Ошибка. Указана неподдерживаемая модель.\n");
            return;
        }

        string order = commandPieces[2]; 

        if (order != "up" && order != "down")
        {
            Console.WriteLine("[FAIL] Ошибка. Указан неподдерживаемый порядок сортировки.\n");
            return;
        }

        switch (model)
        {
            case "person":
                if (order == "up")
                {
                    personsCollection = personsCollection.OrderBy(p => p.Age).ToList();
                }
                else
                {
                    personsCollection = personsCollection.OrderByDescending(p => p.Age).ToList();
                }

                break;
            case "building":
                if (order == "up")
                {
                    buildingsCollection = buildingsCollection.OrderBy(p => p.Owner).ToList();
                }
                else
                {
                    buildingsCollection = buildingsCollection.OrderByDescending(p => p.Owner).ToList();
                }

                break;
            case "car":
                if (order == "up")
                {
                    carsCollection = carsCollection.OrderBy(p => p.Horsepower).ToList();
                }
                else
                {
                    carsCollection = carsCollection.OrderByDescending(p => p.Horsepower).ToList();
                }

                break;
        }
        
        if (order == "up")
        {
            Console.WriteLine($"[SUCCESS] Коллекция {model} была отсортирована по возрастанию.\n");
        }
        else
        {
            Console.WriteLine($"[SUCCESS] Коллекция {model} была отсортирована по убыванию.\n");
        }

    }

    public void HandleSearchCommand(string[] commandPieces)
    {
        // "search <model> <value> - поиск #TODO",

        if (commandPieces.Length != 3)
        {
            Console.WriteLine("[FAIL] Ошибка. Неверное количество параметров к команде.\n");
            return;
        }

        string model = commandPieces[1].ToLower();

        if (!_allowedModels.Contains(model))
        {
            Console.WriteLine("[FAIL] Ошибка. Указана неподдерживаемая модель.\n");
            return;
        }

        string value = commandPieces[2];

        List<object> foundOccurences = new List<object>();

        switch (model)
        {
            case "person":
                for (int i = 0; i < personsCollection.Count(); i++)
                {
                    if (personsCollection[i].Name.ToLower().Contains(value.ToLower()))
                    {
                        foundOccurences.Add(personsCollection[i]);
                    }
                }

                break;
            case "building":
                for (int i = 0; i < buildingsCollection.Count(); i++)
                {
                    if (buildingsCollection[i].Owner.ToLower().Contains(value.ToLower()))
                    {
                        foundOccurences.Add(buildingsCollection[i]);
                    }
                }

                break;
            case "car":
                for (int i = 0; i < carsCollection.Count(); i++)
                {
                    if (carsCollection[i].Manufacturer.ToLower().Contains(value.ToLower()))
                    {
                        foundOccurences.Add(carsCollection[i]);
                    }
                }

                break;
        }

        if (foundOccurences != null && foundOccurences.Any())
        {
            Console.WriteLine($"[SUCCESS] По значению {value} в коллекции {model} было найдено {foundOccurences.Count()} совпадений:");
            for (int i = 0; i < foundOccurences.Count(); i++)
            {
                Console.WriteLine($"[{i}]");
                Console.WriteLine(foundOccurences.ElementAt(i).ToString() + "\n");

            }
        }
        else
        {
            Console.WriteLine($"[SUCCESS] Совпадений по значению {value} в коллекции {model} не было найдено.\n");
        }
    }
}
