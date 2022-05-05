// Si on cherchait à être efficaces et rendre un résultat rapide, DataTable (de System.Data)
// a une méthode "Compute" qui fait exactement ce qui est demandé dans cet exercice!

using DefiCode;

Calculator calculator = new Calculator();
string expression = "(5+2)*3";

Console.WriteLine(calculator.Calculate(expression));

Console.WriteLine("Hello");
