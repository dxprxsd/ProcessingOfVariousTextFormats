using System;
using System.Collections.Generic;
using System.Linq;

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
        "\n\n"
    };
    private readonly List<string> _allowedModels = new List<string>
    {
        "person", "building", "car"
    };

    public List<Person> personsCollection;
    public List<Building> buildingsCollection;
    public List<Car> carsCollection;

    public Menu()
    {
        _isRunning = true;
        this.personsCollection = new List<Person>();
        this.buildingsCollection = new List<Building>();
        this.carsCollection = new List<Car>();
    }

    public void OnStartUp()
    {
        Console.WriteLine("//-- ОБРАБОТКА РАЗЛИЧНЫХ ТЕКСТОВЫХ ФОРМАТОВ --\\\n");
        Console.WriteLine("Поддерживаемые форматы для хранения моделей: JSON, YAML, XML, CSV.\n");
        Console.WriteLine("Существующие модели:");
        Console.WriteLine("Person {\nName: string;\nAge: int\n}\n");
        Console.WriteLine("Car {\nManufacturer: string;\nModel: string;\nYear: int;\nHorsepower: int;\nMaxSpeed: int\n}\n");
        Console.WriteLine("Building {\nOwner: string;\nAddress: string;\nPositionX: double;\nPositionY: double;\nYear: int\n}\n\n\n");

        Console.WriteLine(_helpLines);

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
                HandleReadCommand();
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
        Console.WriteLine(_helpLines);
    }

    public void HandleShowCommand(string[] commandPieces)
    {
        if (commandPieces.Length != 2)
        {
            Console.WriteLine("Ошибка. Неверное количество параметров к команде.");
            return;
        }
        
        string model = commandPieces[1].ToLower();

        if (!_allowedModels.Contains(model))
        {
            Console.WriteLine("Ошибка. Указана неподдерживаемая модель.");
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
            default:
                Console.WriteLine("Ошибка. Введена неподдерживаемая модель.");
                return;
        }

        if (collection != null && collection.Any())
        {
            Console.WriteLine($"Элементы коллекции {model}:");
            foreach (var item in collection)
            {
                Console.WriteLine(item.ToString());  
            }
        }
        else
        {
            Console.WriteLine($"Коллекция {model} не содержит элементов.");
        }

    }

    public void HandleClearCommand(string[] commandPieces)
    {
        if (commandPieces.Length != 2)
        {
            Console.WriteLine("Ошибка. Неверное количество параметров к команде.");
            return;
        }

        string model = commandPieces[1].ToLower();

        if (!_allowedModels.Contains(model))
        {
            Console.WriteLine("Ошибка. Указана неподдерживаемая модель.");
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
            default:
                Console.WriteLine("Ошибка. Введена неподдерживаемая модель.");
                return;
        }

        Console.WriteLine($"Коллекция {model} была успешно очищена.");
    }

    public void HandleReadCommand(string[] commandPieces)
    {
        if (commandPieces.Length != 3)
        {
            Console.WriteLine("Ошибка. Неверное количество параметров к команде.");
            return;
        }

        string model = commandPieces[1].ToLower();

        if (!_allowedModels.Contains(model))
        {
            Console.WriteLine("Ошибка. Указана неподдерживаемая модель.");
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
            default:
                Console.WriteLine("Ошибка. Введена неподдерживаемая модель.");
                return;
        }

        Console.WriteLine($"Коллекция {model} была успешно очищена.");
    }
}