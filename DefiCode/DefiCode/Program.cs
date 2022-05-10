// Si on cherchait à être efficaces et rendre un résultat rapide, DataTable (de System.Data)
// a une méthode "Compute" qui fait exactement ce qui est demandé dans cet exercice!

using DefiCode;

Calculator calculator = new Calculator();
CalculatorUI ui = new CalculatorUI(calculator);

ui.Start();
