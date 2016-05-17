using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace MyRoslynTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = CSharpSyntaxTree.ParseText(
            @"
            namespace NS1
            { 
                class Class1
                { 
                    public int ID { get; set; }
                    public string Name { get; set; }
                    public void Run(int id) { }        
                } 
            }");

            Console.WriteLine($@"tree : {tree}");

            // 루트노드
            var root = (CompilationUnitSyntax)tree.GetRoot();
            Console.WriteLine($@"root : {root}");

            // namespace 노드
            foreach (var ns in root.Members.OfType<NamespaceDeclarationSyntax>())
            {
                Console.WriteLine($@"ns.Name : {ns.Name}");
                var nsName = ns.Name.ToString();

                foreach (var nsMem in ns.Members)
                {
                    // class 노드
                    if (nsMem is ClassDeclarationSyntax)
                    {
                        var cls = (ClassDeclarationSyntax)nsMem;
                        Console.WriteLine($@"cls : {cls}");

                        // 클래스 멤버들
                        foreach (var clsChild in cls.ChildNodes())
                        {
                            Console.WriteLine($@"clsChild : {clsChild}");

                            if (clsChild is PropertyDeclarationSyntax) //속성
                            {
                                var prop = (PropertyDeclarationSyntax)clsChild;
                                Console.WriteLine($@"prop.Identifier.ValueText : {prop.Identifier.ValueText}");
                            }
                            else if (clsChild is MethodDeclarationSyntax) // 메서드
                            {
                                var mth = (MethodDeclarationSyntax)clsChild;
                                Console.WriteLine($@"mth.Identifier.ValueText : {mth.Identifier.ValueText}");
                            }
                        }
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
