using System;
using System.Reflection;
using Entitas;
using Entitas.CodeGenerator;

class MainClass {
  public static void Maine(string[] args) {
    generate();
  }

  static void generate() {
    // All code generators that should be used
    var codeGenerators = new ICodeGenerator[] {
      new ComponentIndicesGenerator(),
      new ComponentExtensionsGenerator(),
      new PoolAttributesGenerator(),
      new PoolsGenerator(),
      new BlueprintsGenerator()
    };

    // Specify all pools
    var poolNames = new [] { "Core" };

    // Specify all blueprints
    var blueprintNames = new string[0];

    var assembly = Assembly.GetAssembly(typeof(Entity));
    var provider = new TypeReflectionProvider(assembly.GetTypes(), poolNames, blueprintNames);

    const string path = "../../Generated/";
    var files = CodeGenerator.Generate(provider, path, codeGenerators);

    foreach (var file in files) {
      Console.WriteLine(file.generatorName + ": " + file.fileName);
    }

    Console.WriteLine("Done. Press any key...");
    Console.Read();
  }
}