using System.Reflection;

namespace Tests.ArchitectureTests.common
{
    [TestClass]
    [TestCategory("Architecture")]
    public class ProjectRulesTests
    {
        private Assembly _assembly;
        private readonly string _assemblyName = "stolenCars";
        private readonly string _commonNamespaceFrament = "common";
        private readonly string _applicationNamespace = "application";
        private readonly string _domainNamespace = "domain";
        private readonly string _infraStructureNamespace = "infrastructure";


        private string getModuleDomainName(string nnamespace)
        {
            IList<string> namespaces = nnamespace.Split('.').ToList();

            for (int position = 0; position < namespaces.Count; position++)
            {
                if (namespaces[position] == _assemblyName)
                {
                    return namespaces[position + 1];
                }
            }
            return null;
        }

        [TestInitialize]
        public void Setup() => _assembly = Assembly.Load(_assemblyName);

        [TestMethod]
        public void NamespaceAsemblyName_Should_BeUniqueAndInFirstPosition()
        {
            var types = _assembly.GetTypes()
                                 .Where(t => t.Namespace != null)
                                 .ToList();

            types.ForEach(type =>
            {
                var namespaceSplited = type.Namespace.Split('.').ToList();
                Assert.IsTrue(namespaceSplited[0].Equals(_assemblyName));
                Assert.IsTrue(namespaceSplited.Where(nsFragment => nsFragment.Equals(_assemblyName)).Count() == 1);
            });
        }

        [TestMethod]
        public void AModuleDomain_ShouldNot_RefferOtherModuleInDomainOrApplicationLayer()
        {
            var types = _assembly.GetTypes()
                                 .Where(t => t.Namespace != null)
                                 .ToList();

            types.ForEach(type =>
            {
                var namespaceModuleName = getModuleDomainName(type.Namespace);
                if (type.Namespace.Contains($".{_domainNamespace}.") || type.Namespace.Contains($".{_applicationNamespace}."))
                {
                    // Get all referenced types in the assembly
                    var referencedTypes = type.GetInterfaces()
                                              .Concat(type.GetFields().Select(f => f.FieldType))
                                              .Concat(type.GetProperties().Select(p => p.PropertyType))
                                              .Concat(type.GetMethods().SelectMany(m => m.GetParameters().Select(p => p.ParameterType)))
                                              .Distinct()
                                              .Where(t => t.Namespace != null);

                    foreach (var refType in referencedTypes) 
                    {
                        var referencedNamespace = refType.Namespace;
                        var referencedModuleName = getModuleDomainName(referencedNamespace);

                        // Check if the referenced type belongs to a different module in the same Domain or Application layer
                        if (!namespaceModuleName.Equals(referencedModuleName) || !referencedModuleName.Equals(_commonNamespaceFrament))
                        {
                            // TODO: Ignore microsoft & system namespaces
                            Assert.Fail($"Type {type.FullName} in module '{namespaceModuleName}' references type {refType.FullName} in module '{referencedModuleName}', which is not allowed.");
                        }
                    }
                }

            });
        }
    }
}