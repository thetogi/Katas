using Katas.Interface_Casting;




var exampleClass = new InterfaceCastingDemo();
exampleClass.myInt64 = 6;
myInterface test = exampleClass as myInterface;

var reCast = test as InterfaceCastingDemo;
reCast.myInt = 1;
Console.WriteLine("Hello, World!");