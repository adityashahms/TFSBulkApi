namespace TFSBulkApi
{
    class Program
    {
        // Azure DevOps API base URL
        private static readonly string BaseUrl = "https://dev.azure.com/{organization}/{project}/_apis/wit/classificationnodes";
        private static readonly string ApiVersion = "7.0";
        private static string? Organization = "adityashahTest"; // Replace with your organization
        private static string? Project = "TestingAPI"; // Replace with your project
        private static string? PatToken = ""; // Replace with your Personal Access Token (PAT)

        static async Task Main(string[] args)
        {

            SetupParams();

            Console.WriteLine("Enter command to operation: 1. Create 2. Update 3. Delete 4. Exit");
            string? command = "";
            while (command != "4")
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        await CreateNodes();
                        break;
                    case "2":
                        await UpdateNodes();
                        break;
                    case "3":
                        await DeleteNodes();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }

        }

        private static void SetupParams()
        {
            Console.WriteLine("Enter Organization name: ");
            Organization = Console.ReadLine();
            Console.WriteLine("Enter Project name: ");
            Project = Console.ReadLine();
            Console.WriteLine("Enter Personal Access Token (PAT): ");
            PatToken = Console.ReadLine();
        }

        private static async Task DeleteNodes()
        {
            var deleteNodeList = generateDeleteNodeLIst();
            foreach (var node in deleteNodeList)
            {
                try
                {
                    var response = await ApiHelper.DeleteClassificationNodeAsync(node);
                    Console.WriteLine($"Node '{node.Name}' - Response: {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error for node '{node.Name}': {ex.Message}");
                }
            }
        }

        private static async Task UpdateNodes()
        {
            var updateNodesList = generateUpdateAreaAndIterationPath();
            foreach (var node in updateNodesList)
            {
                try
                {
                    var response = await ApiHelper.RenameClassificationNodeAsync(node);
                    Console.WriteLine($"Node '{node.Name}' - Response: {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error for node '{node.Name}': {ex.Message}");
                }
            }
        }

        private static async Task CreateNodes()
        {
            var createdNodesList = generatedRandomAreaAndIterationPath();
            foreach (var node in createdNodesList)
            {
                try
                {
                    var response = await ApiHelper.CreateOrUpdateClassificationNodeAsync(node);
                    Console.WriteLine($"Node '{node.Name}' - Response: {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error for node '{node.Name}': {ex.Message}");
                }
            }
        }

        private static List<ClassificationNode> generateDeleteNodeLIst()
        {
            List<ClassificationNode> nodes = new List<ClassificationNode>();
            for (int i = 0; i < 100; i++)
            {
                nodes.Add(new ClassificationNode { Name = "UpdateTestingArea " + i, StructureGroup = "Areas", Path = "UpdateTestingArea " + i });
                nodes.Add(new ClassificationNode { Name = "UpdateTestingIteration " + i, StructureGroup = "iterations", Path = "UpdateTestingIteration " + i });
                //nodes.Add(new ClassificationNode { Name = "CategoryTesting " + i, StructureGroup = "iterations", Path = "CategoryTesting " + i });
            }
            return nodes;
        }

        private static List<ClassificationNode> generateUpdateAreaAndIterationPath()
        {
            List<ClassificationNode> nodes = new List<ClassificationNode>();
            for (int i = 0; i < 100; i++)
            {
                nodes.Add(new ClassificationNode { Name = "UpdateTestingArea " + i, StructureGroup = "Areas", Path = "TestingArea " + i });
                nodes.Add(new ClassificationNode { Name = "UpdateTestingIteration " + i, StructureGroup = "iterations", Path = "TestingIteration " + i });
            }
            return nodes;
        }

        private static List<ClassificationNode> generatedRandomAreaAndIterationPath()
        {
            List<ClassificationNode> nodes = new List<ClassificationNode>();
            for (int i = 0; i < 100; i++)
            {
                nodes.Add(new ClassificationNode { Name = "TestingArea " + i, StructureGroup = "Areas", Path = "Area/Category" + i });
                nodes.Add(new ClassificationNode { Name = "TestingIteration " + i, StructureGroup = "iterations", Path = "Iteration/Category" + i });
            }
            return nodes;
        }
    }
}

public class ClassificationNode
{
    public string Name { get; set; }
    public string StructureGroup { get; set; } // "areas" or "iterations"
    public string Path { get; set; } // Path to the node (e.g., "Area/Category1" or "Iteration/Category2")
}

