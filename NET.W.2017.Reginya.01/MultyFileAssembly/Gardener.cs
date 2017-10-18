using System;
using Orchard;

class Program
{
	public static void Main()
	{        
		AppleTree tree1 = new AppleTree();
		tree1.AppleTreeInfo();

		PearTree tree2 = new PearTree();
		tree2.PearTreeInfo();
		
		CherryTree tree3 = new CherryTree();
		tree3.CherryTreeInfo();
	
		Console.ReadLine();
	}	
}