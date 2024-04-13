using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Processing_of_various_text_formats_Kuzmin
{

    /*
     
    #todo абстракция - File Manager вместо File.Write / File.Read

    1) Models
        Person
        Car

    2) FileHandlers 
        interface FileHandler 

        CsvFileHandler
        JsonFileHandler
        XmlFileHandler
        csvFileHandler

    3) Main
    
     
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.OnStartUp();
            menu.DisplayMenu();
        }

        /*static void Main(string[] args)
        {
            //Person person = new Person { Id = 123, Name = "Da", Age = 23 };
            var persons = new List<Person>
            {
                new Person { Name = "Da0034", Age = 23 },
                new Person { Name = "Da1", Age = 24 },
                new Person { Name = "Da2", Age = 25 },
                new Person { Name = "Da3", Age = 26 }
            };


            var csvFileHandler = new CsvFileHandler();
            var xmlFileHandler = new XmlFileHandler();
            var jsonFileHandler = new JsonFileHandler();
            var yamlFileHandler = new YamlFileHandler();
            // Person person = new Person { Id = 122776, Name = "Yes", Age = 262 };
            //xmlFileHandler.WriteToFile("testpersons_new.xml", persons);
            //jsonFileHandler.WriteToFile("testpersons_new.json", persons);
            //yamlFileHandler.WriteToFile("testpersons_new.yaml", persons);
            //List<Person> newPersons = xmlFileHandler.ReadFromFile<Person>("testpersons_new.xml");
            //List<Person> newPersons = jsonFileHandler.ReadFromFile<Person>("testpersons_new.json");
            //List<Person> newPersons = yamlFileHandler.ReadFromFile<Person>("testpersons_new.yaml");

            //csvFileHandler.WriteToFile("testpersons.csv", persons);

            //List<Person> newPersons = csvFileHandler.ReadFromFile<Person>("testpersons.csv");


            foreach (Person person in newPersons)
            {
                Console.WriteLine(person.ToString());
            }

            Console.ReadKey();


            // команда
            // 2 - запись моделей в файл

        }*/
    }
}
