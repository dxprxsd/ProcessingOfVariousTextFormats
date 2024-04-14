using System;
using System.Reflection;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public override string ToString()
    {
        return $"Name: {Name};\n" +
            $"Age: {Age}.";
    }

    public static Person CreatePersonFromConsoleInput()
    {
        Person person = new Person();

        Console.Write("Name: ");
        person.Name = Console.ReadLine();

        while (true)
        {
            Console.Write("Age: ");
            try
            {
                person.Age = Int32.Parse(Console.ReadLine());
                break;
            }
            catch (FormatException) { }
        }
        
        return person;
    }

    public object GetValueByFieldName(string fieldName)
    {
        PropertyInfo propertyInfo = this.GetType().GetProperty(fieldName);

        if (propertyInfo == null )
        {
            throw new ArgumentException($"Поля {fieldName} у модели Person не существует.");
        }

        return 3;
        //return propertyInfo.GetValue( this, null ); 
    }
}